using UnityEngine;
using UnityEngine.AI;
public class EnemyNavMeshMoveSystem : EnemyMovementSystem<EnemyBrain, NavMeshAgent>
{
    public EnemyNavMeshMoveSystem(EnemyBrain brain, NavMeshAgent component) : base(brain, component)
    {
    }

    public override void Move(Vector3 direction)
    {
        component.SetDestination(direction);
    }

    public override void Stop()
    {
        component.SetDestination(brain.transform.position);
        component.ResetPath();
    }
}