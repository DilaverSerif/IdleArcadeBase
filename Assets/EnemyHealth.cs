public class EnemyHealth : HealthSystem<EnemyBrain>
{
    public DropItem dropItem;
    public override void Die()
    {
        brain.enemyStateMachine.ChangeState(Enum_EnemyState.Dead);
        dropItem.Drop(transform.position);
    }
    public override void Revive()
    {
       
    }
    public override void Hit()
    {
        brain.enemyStateMachine.ChangeState(Enum_EnemyState.Hurt);
    }
    public override WarSide GetWarSide()
    {
        return WarSide.AgainstPlayer;
    }
}