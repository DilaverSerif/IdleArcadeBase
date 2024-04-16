using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class TopRight
{
    [Serializable]
    public struct ItemUI
    {
        public Enum_Item Item;
        public Image GoldIcon;
        public TextMeshProUGUI GoldTextMeshProUGUI;
    }

    public ItemUI[] items;

    public void OnInitialize()
    {
        PlayerInventory.OnUpdateInventory += OnUpdateInventory;
    }

    public void OnDisable()
    {
        PlayerInventory.OnUpdateInventory -= OnUpdateInventory;
    }
    private void OnUpdateInventory(List<InventoryItem> itemList)
    {
        if(itemList.Count == 0) return;
        
        foreach (var iInventoryItem in itemList)
        {
            GetTextMeshByItemType(iInventoryItem.item).text = iInventoryItem.amount.ToString();
        }
    }
    
    public Vector3 GetIconPointByItemType(Enum_Item enumItem)
    {
        foreach (var item in items)
        {
            if (item.Item == enumItem)
                return item.GoldIcon.transform.position;
        }
        
        Debug.LogError("Error Not Found item");
        
        return Vector3.back;
    }

    public TextMeshProUGUI GetTextMeshByItemType(Enum_Item enumItem)
    {
        foreach (var item in items)
        {
            if (item.Item == enumItem)
                return item.GoldTextMeshProUGUI;
        }
        
        Debug.LogError("Error Not Found item");

        return null;
    }

}