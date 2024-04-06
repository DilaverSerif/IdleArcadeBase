using Sirenix.OdinInspector;
using UnityEngine;
[CreateAssetMenu(menuName = "Player System/Create Player Default Data", fileName = "PlayerDefaultData", order = 0)]
public class PlayerDefaultData: ScriptableObject, IPlayerData
{
    [field: SerializeField,BoxGroup("Movement")]
    public AnimationCurve movementCurve { get; set; }
    [field: SerializeField,BoxGroup("Movement")]
    public AnimationCurve jumpCurve { get; set; }
    [field: SerializeField,BoxGroup("Movement")]
    public float acceleration { get; set; }
    [field: SerializeField,BoxGroup("Rotation")]
    public AnimationCurve rotationTurnSpeedCurve { get; set; }
    [field: SerializeField,BoxGroup("Rotation")]
    public PlayerInGameData.RotationType rotationType { get; set; }
    
    [field: SerializeField,BoxGroup("Health")]
    public int maxHealth { get; set; }
}