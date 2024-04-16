using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    public static bool MusicOn
    {
        get => ES3.Load("MusicSave", true);
        set => ES3.Save("MusicSave", MusicOn);
    }
    public static bool EffectOn
    {
        get => ES3.Load("EffectSave", true);
        set => ES3.Save("EffectSave", EffectOn);
    }

    [TableList]
    public AudioClip[] audioClip;
    public AudioClip gameMusic;

    private AudioSource soundFxSource;

    AudioSource SoundFxSource
    {
        get
        {
            if (soundFxSource != null) return soundFxSource;

            soundFxSource = new GameObject("SoundFxSource").AddComponent<AudioSource>();
            soundFxSource.transform.SetParent(transform);

            return soundFxSource;
        }
    }

    private AudioSource musicSource;

    AudioSource MusicSource
    {
        get
        {
            if (musicSource == null)
            {
                musicSource = new GameObject("MusicSource").AddComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.transform.SetParent(transform);
            }

            return musicSource;
        }
    }

    private readonly List<AudioSource> loopSoundFxSources = new List<AudioSource>();

    // ReSharper disable once Unity.RedundantEventFunction
    void Awake()
    {

    }

    void Start()
    {
        MusicSource.clip = gameMusic;

        if (MusicOn)
            MusicSource.Play();
        else
            MusicSource.Stop();
    }
        
    public void CloseAllSounds(bool fade = true)
    {
        var volume = 0;
        if (!fade) volume = 1;

        foreach (var loopSoundFxSource in loopSoundFxSources)
        {
            loopSoundFxSource.DOFade(volume, 0.5f).OnComplete(() =>
            {
                loopSoundFxSources.Remove(loopSoundFxSource);
                Destroy(loopSoundFxSource.gameObject);
            });
        }

        MusicSource.DOFade(volume, 0.5f).OnComplete(() =>
        {
            if (volume > 0)
                MusicSource.Play();
            else MusicSource.Stop();
        });

        SoundFxSource.DOFade(volume, 0.5f).OnComplete(() =>
        {
            if (volume > 0)
                SoundFxSource.Play();
            else SoundFxSource.Stop();
        });
    }

    public void PlaySoundFx(string clipID, float pitch = 1f, float volume = 1f)
    {
        if (!EffectOn) return;
        SoundFxSource.pitch = pitch;
        SoundFxSource.volume = volume;
        SoundFxSource.PlayOneShot(GetClip(clipID));
    }

    public void PlayMusic(string clipID, float pitch = 1f, float volume = 1f)
    {
        if (!MusicOn) return;
        SoundFxSource.pitch = pitch;
        SoundFxSource.volume = volume;
        MusicSource.loop = true;
        MusicSource.clip = GetClip(clipID);
    }

    public void PlayLoopSoundFx(string clipID, float pitch = 1f, float volume = 1f)
    {
        if (!EffectOn) return;
        var loopSoundFxSource = new GameObject("loop" + clipID).AddComponent<AudioSource>();
        loopSoundFxSource.transform.SetParent(SoundFxSource.transform);
        loopSoundFxSource.volume = 0;
        loopSoundFxSource.clip = GetClip(clipID);
        loopSoundFxSource.loop = true;
        loopSoundFxSources.Add(loopSoundFxSource);
        loopSoundFxSource.DOFade(volume, 0.3f);
        loopSoundFxSource.pitch = pitch;
        loopSoundFxSource.Play();
    }

    public void StopLoopSoundFx(string clipID)
    {
        if (!EffectOn) return;
        var loopSoundFxSource = loopSoundFxSources.Find(x => x.gameObject.name == "loop" + clipID.ToString());
        if (loopSoundFxSource == null) return;
        loopSoundFxSources.Remove(loopSoundFxSource);
        loopSoundFxSource.DOFade(0, 0.3f).OnComplete(() => Destroy(loopSoundFxSource.gameObject));
    }

    public void StopMusic(bool stop)
    {
        if (stop)
            MusicSource.Stop();
        else
            MusicSource.Play();
    }

    AudioClip GetClip(string clipID)
    {
        foreach (var clip in audioClip)
        {
            if (clip.name != clipID) continue;
            return clip;
        }

        Debug.LogError("Clip not found!");
        return null;
    }

    public void MuteSound(bool b)
    {
        foreach (var audioSource in loopSoundFxSources.Where(audioSource => audioSource != musicSource))
            audioSource.mute = b;

        if (SoundFxSource != musicSource)
            SoundFxSource.mute = b;
    }
}