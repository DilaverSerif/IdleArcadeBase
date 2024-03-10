using Sirenix.OdinInspector;
using UnityEngine;
public interface IPlayerData
{
    [BoxGroup("Movement")]
    public AnimationCurve movementCurve { get; set; }
    public AnimationCurve jumpCurve { get; set; }
    
    [BoxGroup("Movement")]
    public float acceleration  { get; set; }
    
    [BoxGroup("Rotation")] 
    public AnimationCurve rotationTurnSpeedCurve  { get; set; }
    [BoxGroup("Rotation")] 
    public PlayerInGameData.RotationType rotationType { get; set; }
}