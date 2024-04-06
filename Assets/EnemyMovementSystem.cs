using UnityEngine;
public abstract class EnemyMovementSystem<TBrain,TComponent> where TComponent : Component where TBrain : MonoBehaviour
{
    public TComponent component;
    public TBrain brain;
    
    public EnemyMovementSystem(TBrain brain, TComponent component)
    {
        this.brain = brain;
        this.component = component;
    }

    public abstract void Move(Vector3 direction);
    public abstract void Stop();
}