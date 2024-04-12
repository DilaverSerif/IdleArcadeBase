using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour
{
    public EnemyInGameData inGameData;
    public EnemyDefaultData defaultData;

    public EnemyStateMachine enemyStateMachine;
    public EnemyNavMeshMoveSystem enemyMovement;
    public EnemyHealth healthSystem;
    public EnemyAttackSystem attackSystem;

    void Awake()
    {
        attackSystem = GetComponent<EnemyAttackSystem>();
        healthSystem = GetComponent<EnemyHealth>();
        healthSystem.InitializeHealthSystem(this,defaultData.maxHealth);
        
        enemyStateMachine = new EnemyStateMachine(this);
        enemyMovement = new EnemyNavMeshMoveSystem(this, GetComponent<NavMeshAgent>());
    }

    void FixedUpdate()
    {
        enemyStateMachine.StateMachine.OnLogic();
    }

    void OnDrawGizmosSelected()
    {
        enemyStateMachine.routeSystem.OnGizmos();
    }
}