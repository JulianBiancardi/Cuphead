using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : State
{
    public float speed = 5f;
    private int targetXaxis = 0; // 0 = center, 1 = right, -1 = left


    public PlayerGroundState(PlayerStateManager context): base(context){
    }

  
    public override void Enter()
    {
        context.animator.SetBool("isGrounded", true);
    }

    public override void UpdateState()
    {
        if(Input.GetKey(KeyCode.S)){
            context.ChangeState(context.crouchState);
        } else if(Input.GetKeyDown(KeyCode.LeftShift)){
            context.Dash();
        } else if(Input.GetKey(KeyCode.Space)){
            context.ChangeState(context.jumpState);
        } 

        float horizontal = Input.GetAxis("Horizontal");
        if(!context.isDashing){
            context.rigidbody2D.velocity = new Vector2(horizontal * speed, context.rigidbody2D.velocity.y);
            context.animator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        Flip(horizontal);
        Shoot();
    }

    public override void Exit()
    {
        context.animator.SetBool("isGrounded", false);
    }

    void Flip(float horizontal)
    {
        if(horizontal > 0){
            context.direction = PlayerStateManager.Direction.Right;
            context.transform.rotation = Quaternion.Euler(0, 0, 0);
            targetXaxis = 1;
        } else if(horizontal < 0){
            context.direction = PlayerStateManager.Direction.Left;
            context.transform.rotation = Quaternion.Euler(0, -180, 0);
            targetXaxis = -1;
        } else {
            targetXaxis = 0;
        }
    }

    void Shoot(){
        if(Input.GetKeyDown(KeyCode.F) && context.superCount > 0){
            context.superCount--;
            context.playerWeapon.StopShooting();
            context.animator.SetBool("isShooting", false);
            context.animator.SetTrigger("superAttack");
        } else{
            if(Input.GetKey(KeyCode.E)){
                context.animator.SetBool("isShooting", true);
                context.playerWeapon.Shoot(getTargetRotation());
            } else if(Input.GetKeyUp(KeyCode.E)){
                context.animator.SetBool("isShooting", false);
                context.playerWeapon.StopShooting();
            }
        }
    }

    private Quaternion getTargetRotation(){
        if(Input.GetKey(KeyCode.W)){
            context.animator.SetFloat("targetYaxis", 1);
            if(targetXaxis != 0)
                return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 45);
            else
                return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 90);
        } else {
            context.animator.SetFloat("targetYaxis", 0);
            return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 0);
        }
    }

}
