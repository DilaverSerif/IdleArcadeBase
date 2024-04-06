public class PlayerHealth : HealthSystem
{
    public override void Die()
    {
        throw new System.NotImplementedException();
    }
    public override void Revive()
    {
        throw new System.NotImplementedException();
    }
    public override void Hit()
    {
        throw new System.NotImplementedException();
    }
    public override WarSide GetWarSide()
    {
        return WarSide.PlayerSide;
    }
}