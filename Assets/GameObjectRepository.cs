using UnityEngine;

[CreateAssetMenu(fileName = "GameObjectRepository", menuName = "ScriptableObjects/GameObjectRepository", order = 1)]
public class GameObjectRepository : SingletonScriptableObject<GameObjectRepository>
{
    public FloatingText floatingTextPrefab;
    public WorldItem[] worldItems;
    
    public WorldItem GetWorldItem(Enum_Item itemType)
    {
        foreach (var worldItem in worldItems)
        {
            if (worldItem.itemType == itemType)
            {
                return worldItem;
            }
        }

        Debug.LogError("WorldItem not found for " + itemType);
        return null;
    }
}