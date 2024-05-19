using UnityEngine;
public class EnemyAnimationEventTrigger : MonoBehaviour
{
    public EnemyBrain EnemyBrain;
    
    public void AttackEvent()
    {
        EnemyBrain.attackSystem.Attack();
    }
}