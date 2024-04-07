public class EnemyHealth : HealthSystem<EnemyBrain>
{
    public override void Die()
    {
        brain.enemyStateMachine.ChangeState(Enum_EnemyState.Dead);
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