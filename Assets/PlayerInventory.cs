using System;
public class PlayerInventory: Inventory
{
    public ItemLooter itemLooter;

    void Awake()
    {
        itemLooter.Initialize(this);
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