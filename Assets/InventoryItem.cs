using System;
[Serializable]
public class InventoryItem
{
    public Enum_Item item;
    public int amount;
    
    public InventoryItem(Enum_Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}