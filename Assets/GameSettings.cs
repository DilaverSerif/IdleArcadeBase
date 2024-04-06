using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "Create GameSettings", fileName = "GameSettings", order = 0)]
public class GameSettings : SingletonScriptableObject<GameSettings>
{
    [BoxGroup("Floating Text Settings")]
    public float floatingTextLifeTime = 1;
    [BoxGroup("Floating Text Settings")]
    public float floatingTextScaleTime = 1;
}