using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private float LifeTime => GameSettings.Instance.floatingTextLifeTime;
    private float ScaleTime => GameSettings.Instance.floatingTextScaleTime;
    private TextMeshPro textMeshPro;

    void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    public void Initialize(FloatingTextData floatingTextData)
    {
        textMeshPro.text = floatingTextData.text;
        textMeshPro.color = floatingTextData.color;
        
        Animate().Forget();
    }
    
    void Update()
    {
        transform.LookAt(transform.position - Camera.main.transform.position, Vector3.up);
    }

    async UniTaskVoid Animate()
    {
        transform.localScale = Vector3.zero;
        await transform.DOScale(Vector3.one, ScaleTime).SetEase(Ease.OutBack);
        
        await UniTask.Delay(TimeSpan.FromSeconds(LifeTime));
        
        await transform.DOScale(Vector3.zero, ScaleTime).SetEase(Ease.InBack);
        
        Destroy(gameObject);
    }

    #if UNITY_EDITOR
    [Button]
    public void TestText(string text)
    {
        FloatingTextData floatingTextData = new FloatingTextData
        {
            text = text,
            color = Color.white
        };
        Initialize(floatingTextData);
    }
  #endif
}