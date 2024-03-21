using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TargetFinder
{
    public string targetTag;
    public float enterRadius;
    public float exitRadius;
    
    public virtual void OnLogic()
    {
        
    }
}