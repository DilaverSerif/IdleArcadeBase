using System;
using System.Collections.Generic;
public class PlayerInventory: Inventory
{
    public static Action<List<InventoryItem>> OnUpdateInventory;
    public static Action<ItemTransferData> OnAddedItem;
    public static Action<ItemTransferData> OnRemoveItem;
    
    public ItemLooter itemLooter;

    void Awake()
    {
        itemLooter.Initialize(this);
    }

    public override void AddItem(ItemTransferData itemTransferData)
    {
        base.AddItem(itemTransferData);
        OnAddedItem?.Invoke(itemTransferData);
    }

    public override void RemoveItem(ItemTransferData itemTransferData)
    {
        base.RemoveItem(itemTransferData);
        OnRemoveItem?.Invoke(itemTransferData);
    }

    void OnValidate()
    {
        itemLooter.Initialize(this);
    }

    void Update()
    {
        itemLooter.OnUpdate();
    }

    void OnDrawGizmosSelected()
    {
        itemLooter.OnDrawGizmosSelected();
    }
}