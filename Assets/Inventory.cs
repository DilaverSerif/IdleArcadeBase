using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    [TableList]
    public List<InventoryItem> items = new List<InventoryItem>();

    public void AddItem(ItemTransferData itemTransferData)
    {
        foreach (var inventoryItem in items.Where(inventoryItem => inventoryItem.item == itemTransferData.item))
        {
            inventoryItem.amount += itemTransferData.amount;
            Debug.Log("Added " + itemTransferData.amount + " " + itemTransferData.item + " to inventory");
            return;
        }
        
        items.Add(new InventoryItem(itemTransferData.item, itemTransferData.amount));
        Debug.Log("Added New Item " + itemTransferData.item + " to inventory");
    }
    
    public void RemoveItem(ItemTransferData itemTransferData)
    {
        foreach (var inventoryItem in items.Where(inventoryItem => inventoryItem.item == itemTransferData.item))
        {
            inventoryItem.amount -= itemTransferData.amount;
            Debug.Log("Removed " + itemTransferData.amount + " " + itemTransferData.item + " from inventory");
            return;
        }
        
        Debug.Log("Item " + itemTransferData.item + " not found in inventory");
    }
    
    public bool HasItem(Enum_Item item, int amount)
    {
        return items.Any(inventoryItem => inventoryItem.item == item && inventoryItem.amount >= amount);
    }
}