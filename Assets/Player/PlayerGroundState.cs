using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : State
{
    public float speed = 5f;


    public PlayerGroundState(PlayerStateManager context): base(context){
    }

  
    public override void Enter()
    {
        context.animator.SetBool("isGrounded", true);
    }

    public override void UpdateState()
    {
        if(context.targetYaxis < 0){
            context.ChangeState(context.crouchState);
        }

        if(!context.isDashing){
            context.rigidbody2D.velocity = new Vector2(context.targetXaxis * speed, context.rigidbody2D.velocity.y);
        }
    }

    public override void Exit()
    {
        context.animator.SetBool("isGrounded", false);
    }

    public override Quaternion getTargetRotation(){
        float angle = Vector2.Angle(context.transform.right, new Vector2(context.targetXaxis, context.targetYaxis));
        return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, angle);
    }


    public override void OnJump(){
        if(context.isDashing){
            return;
        }
        context.ChangeState(context.jumpState);
    }
}
