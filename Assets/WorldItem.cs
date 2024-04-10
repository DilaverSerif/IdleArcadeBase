using UnityEngine;

public class WorldItem : MonoBehaviour
{
    private Collider baseCollider;
    public Enum_Item itemType;
    public int amount = 1;
    void Awake()
    {
        baseCollider = GetComponent<Collider>();
    }

    void OnEnable()
    {
        baseCollider.enabled = true;
    }

    public void Loot(Inventory inventory)
    {
        baseCollider.enabled = false;
        
        inventory.AddItem(new ItemTransferData(itemType, amount));
        Destroy(gameObject);
    }
}
