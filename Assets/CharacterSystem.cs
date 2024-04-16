using UnityEngine;
public abstract class CharacterSystem<T> where T: Component
{
    public bool debug = false;
    [HideInInspector]
    public T brain;
    protected Transform transform => brain.transform;

    public CharacterSystem(T brain)
    {
        this.brain = brain;
    }
    
    public virtual void OnStart()
    {
        
    }
    
    public virtual void OnUpdate()
    {
        
    }
    
    public virtual void OnFixedUpdate()
    {
        
    }
    
    public virtual void OnLateUpdate()
    {
        
    }
    
    public virtual void OnEnable()
    {
        
    }
    
    public virtual void OnDisable()
    {
        
    }
    
    public virtual void OnDestroy()
    {
        
    }
    
    public virtual void OnApplicationQuit()
    {
        
    }
    
    public virtual void OnApplicationPause()
    {
        
    }
    
    public virtual void OnApplicationFocus()
    {
        
    }
    
    public virtual void OnDrawGizmos()
    {
        
    }
    
    public virtual void OnDrawGizmosSelected()
    {
        
    }
    
    public virtual void OnValidate()
    {
        
    }
    
    public virtual void OnGUI()
    {
        
    }
    
    public virtual void OnRenderObject()
    {
        
    }
    
    
    
}