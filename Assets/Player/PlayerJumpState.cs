using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : State
{
    public float speed = 5f;
    private int targetXaxis = 0; // 0 = center, 1 = right, -1 = left
    private bool canParry = true;


    private enum Direction
    {
        Left = -1,
        Right = 1
    }
    private Direction direction = Direction.Right;

    public PlayerJumpState(PlayerStateManager context): base(context){
    }
  
    public override void Enter()
    {
        context.animator.SetBool("isGrounded", false);
        context.rigidbody2D.velocity = new Vector2(context.rigidbody2D.velocity.x, context.jumpForce);
        context.isGrounded = false;
        canParry = true;
        context.audioSource.PlayOneShot(context.jumpSound);
    }

    public override void UpdateState()
    {
        if(!context.isDashing){
            context.rigidbody2D.velocity = new Vector2(context.move * speed, context.rigidbody2D.velocity.y);
        }
        
        if(context.isGrounded){
            context.animator.SetBool("isGrounded", true);
            context.rigidbody2D.velocity = new Vector2(0, 0);
            context.ChangeState(context.groundState);
        }
    }

    public override void Exit()
    {
        context.animator.SetBool("isGrounded", false);
        context.audioSource.PlayOneShot(context.landSound);
        context.LandGroundFX();
    }

    public override void OnJump(){
        if(canParry){
            Parry();
        }
    }

    void Parry(){
        context.animator.SetTrigger("parry");
        canParry = false;
        Vector2 size = new Vector2(1, 1);
        RaycastHit2D hit = Physics2D.BoxCast(context.transform.position, size, 0, Vector2.right * (int)direction, 1f, LayerMask.GetMask("Parryable"));
        if(hit.collider != null){
            context.audioSource.PlayOneShot(context.parrySound);
            //Add impulse force to top for player
            context.rigidbody2D.velocity = new Vector2(context.rigidbody2D.velocity.x, context.jumpForce / 1.2f);
            canParry = true;
        }
    }
    public override Quaternion getTargetRotation(){
        return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 0);
    }
}
