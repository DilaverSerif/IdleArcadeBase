public struct ItemTransferData //itemleri envanterden envantere aktarırken kullanılan veri yapısı
{
    public Enum_Item item;
    public int amount;
    
    public ItemTransferData(Enum_Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}