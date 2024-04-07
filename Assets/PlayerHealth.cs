using ComboSystem.Player;
public class PlayerHealth : HealthSystem<PlayerBrain>
{
    public override void Die()
    {
        brain.playerStateMachine.ChangeState(Enum_PlayerState.Die);
    }
    public override void Revive()
    {
        
    }
    public override void Hit()
    {
        brain.playerStateMachine.ChangeState(Enum_PlayerState.Hurt);
    }
    public override WarSide GetWarSide()
    {
        return WarSide.PlayerSide;
    }
}