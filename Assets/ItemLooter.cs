using System;
using UnityEditor;
using UnityEngine;

[Serializable]
public class ItemLooter
{
    public float lootRange = 3;
    public LayerMask layerMask;
    
    private Transform Transform => inventory.transform;
    private Inventory inventory;
    
    public void Initialize(Inventory inventoryParent)
    {
        inventory = inventoryParent;
    }
    
    public void OnUpdate()
    {
        Loot();   
    }
    
    private void Loot()
    {
        var targets = AttackSystemExtensions.GetObjectsWithLayerInCircularCast<WorldItem>(Transform.position, layerMask, lootRange);

        if (targets.Count > 0)
        {
           targets[0].Loot(inventory);
        }
    }

    public void OnDrawGizmosSelected()
    {
        #if UNITY_EDITOR
        
        Handles.color = Color.green;
        Handles.DrawWireDisc(Transform.position, Vector3.up, lootRange);
        Handles.Label(Transform.position + lootRange * Transform.forward, "Loot Range");
        #endif
    }
}
