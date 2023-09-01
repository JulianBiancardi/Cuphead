using UnityEngine;
public abstract class State
{
    protected PlayerStateManager context;

    public State(PlayerStateManager context){
        this.context = context;
    }

    public abstract void Enter();
    public abstract void UpdateState();
    public abstract void Exit();
    public abstract void OnJump();

    public abstract Quaternion getTargetRotation();

}
