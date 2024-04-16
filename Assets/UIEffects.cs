using DG.Tweening;
using Lean.Pool;
using UnityEngine;
public static class UIEffects
{
    public struct UiEffectData
    {
        public GameObject targetObject;
        public Vector3 spawnPos;
        public Quaternion spawnRotation;
        
        public Vector3 targetUIPos;
        public float moveTime;
    }
    
    public static Tween WorldToUIPos(UiEffectData effectData)
    {
        var mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        var toScreenPoint = Camera.main.WorldToScreenPoint(effectData.spawnPos);
        var spawnedObject = LeanPool.Spawn(effectData.targetObject, toScreenPoint, effectData.spawnRotation,mainCanvas.transform);
        
        return spawnedObject.transform.DOMove(effectData.targetUIPos, effectData.moveTime);
    }
    
    public static Tween UIToUIPos(UiEffectData effectData)
    {
        var mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        var spawnedObject = LeanPool.Spawn(effectData.targetObject, effectData.spawnPos, effectData.spawnRotation,mainCanvas.transform);
        
        return spawnedObject.transform.DOMove(effectData.targetUIPos, effectData.moveTime);
    }
}