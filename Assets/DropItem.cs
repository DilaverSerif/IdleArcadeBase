using System;
using System.Collections.Generic;
using DG.Tweening;
using Lean.Pool;
using Lean.Touch;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class DropItem
{
    public List<DropItemData> dropItems;
    
    public void Drop(Vector3 centerPosition)
    {
        foreach (var dropItemData in dropItems)
        {
            if (Random.Range(0, 100) < dropItemData.dropChance)
            {
                var itemPrefab = GameObjectRepository.Instance.GetWorldItem(dropItemData.item);

                for (int i = 0; i < dropItemData.dropAmount; i++)
                {
                    var spawned = LeanPool.Spawn(itemPrefab, centerPosition, Quaternion.identity);

                    var distance = Random.Range(3, 8f);
                    var dropPoint = centerPosition + Random.insideUnitSphere * distance;
                    
                    var jumpCount = distance.Remap(3,7,2,4);
                    var duration = distance.Remap(3,7,0.35f,1f);
                    
                    dropPoint.y = 0;    
                    spawned.transform.DOJump(dropPoint, 1, (int)jumpCount,duration).SetEase(Ease.InOutQuad);
                }
            }
        }
    }
    

}