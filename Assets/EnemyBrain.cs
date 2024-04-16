using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

public class EnemyBrain : MonoBehaviour
{
    public EnemyInGameData inGameData;
    public EnemyDefaultData defaultData;

    public EnemyStateMachine enemyStateMachine;
    public EnemyNavMeshMoveSystem enemyMovement;
    public EnemyHealth healthSystem;
    public EnemyAttackSystem attackSystem;
    public EnemyAnimationSystem enemyAnimationSystem;

    void Awake()
    {
        attackSystem = GetComponent<EnemyAttackSystem>();
        healthSystem = GetComponent<EnemyHealth>();
        healthSystem.InitializeHealthSystem(this, defaultData.maxHealth);

        enemyStateMachine = new EnemyStateMachine(this);
        enemyMovement = new EnemyNavMeshMoveSystem(this, GetComponent<NavMeshAgent>());
        enemyAnimationSystem = new EnemyAnimationSystem(this);
    }

    void FixedUpdate()
    {
        enemyStateMachine.StateMachine.OnLogic();
        enemyAnimationSystem.OnUpdate();
    }

    void OnDrawGizmosSelected()
    {
        enemyStateMachine.routeSystem.OnGizmos();
    }
}