using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Sirenix.OdinInspector;
using UnityEngine;

[Serializable]
public class StatUser
{
    public Action<AddedStat> OnAddedStat;
    public List<StatUserData> stats;
    public List<Buff> buffs;
    
    private UniTask updateBuffsTask;
    
    public void AddBuff(Buff buff)
    {
        buffs.Add(buff);
        
        if(updateBuffsTask.Status == UniTaskStatus.Pending) return;
        updateBuffsTask = UpdateBuffs();
        
        OnAddedStat?.Invoke(new AddedStat());
    }
    
    public void RemoveBuff(Buff buff)
    {
        buffs.Remove(buff);
    }
    
    private async UniTask UpdateBuffs()
    {
        while (buffs.Count > 0)
        {
            buffs.ForEach(buff =>
            {
                if (buff.isPermanent) return;
                buff.timeLeft -= Time.deltaTime;
                if (buff.timeLeft <= 0)
                {
                    buffs.Remove(buff);
                }
            });

            await UniTask.WaitForFixedUpdate();
        }
    }
    public float GetStat(StatType moveSpeed)
    {
        var result = stats.FirstOrDefault(stat => stat.statType == moveSpeed);
        return result?.CurrentValue ?? 0;
    }

    #if UNITY_EDITOR
    
    [Button]
    public void AddTestStats(StatType statUserData,int level)
    {
        var stat = new StatUserData
        {
            statType = statUserData,
            level = level
        };
        
        stats.Add(stat);
        OnAddedStat?.Invoke(new AddedStat());
        //
        // if(updateBuffsTask.Status == UniTaskStatus.Pending) return;
        // updateBuffsTask = UpdateBuffs();
    }
    
    [Button]
    public void RemoveTestStats(StatUserData statUserData)
    {
        stats.Remove(statUserData);
    }
    
    [Button]
    public void AddTestBuff(Buff buff)
    {
        buffs.Add(buff);
        
        if(updateBuffsTask.Status == UniTaskStatus.Pending) return;
        updateBuffsTask = UpdateBuffs();
        
        OnAddedStat?.Invoke(new AddedStat());
    }
    
  #endif
}