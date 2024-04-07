using System;
using Sirenix.OdinInspector;
using UnityHFSM;
public abstract class BaseStateMachine<TEnum, TEventData, TBrain> where TEnum : Enum
    where TEventData : struct
    where TBrain : class
{
    protected TBrain brain;

    protected StateMachine<TEnum, TEventData> stateMachine;
    public StateMachine<TEnum, TEventData> StateMachine
    {
        get
        {
            return stateMachine;
        }
    }

    [ShowInInspector]
    public TEnum CurrentState
    {
        get
        {
            return stateMachine != null ? stateMachine.ActiveStateName : default;
        }
    }

    protected BaseStateMachine(TBrain brain)
    {
        this.brain = brain;
        stateMachine = new StateMachine<TEnum, TEventData>();
        
        OnCreateStates();
        OnSetStates();
        OnSetTransitions();
        
        stateMachine.SetStartState(GetStartingState());
        stateMachine.Init();
    }
    
    public virtual void ChangeState(TEnum state,bool force = true)
    {
        stateMachine.RequestStateChange(state,force);
    }
    
    protected abstract void OnCreateStates();
    protected abstract void OnSetTransitions();
    protected abstract void OnSetStates();
    public abstract TEnum GetStartingState();
}