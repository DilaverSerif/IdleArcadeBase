using UnityEngine;
public abstract class PlayerSystem
{
    [HideInInspector]
    public PlayerBrain playerBrain;
    protected Transform transform => playerBrain.transform;

    public PlayerSystem(PlayerBrain playerBrain)
    {
        this.playerBrain = playerBrain;
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