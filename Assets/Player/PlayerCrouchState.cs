using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : State
{

    public PlayerCrouchState(PlayerStateManager context): base(context){
    }

    public override void Enter()
    {
        context.animator.SetBool("isCrouching", true);
        context.rigidbody2D.velocity = new Vector2(0, 0);
    }

    public override void UpdateState()
    {
        if(!Input.GetKey(KeyCode.S)){
            context.ChangeState(context.groundState);
        }
    }

    public override void Exit()
    {
        context.animator.SetBool("isCrouching", false);
    }

    public override void OnJump(){
        if(OneWayPlatformCheck()){
            return;
        }
        context.ChangeState(context.groundState);
    }


    public override Quaternion getTargetRotation(){
        return Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 0);
    }
    bool OneWayPlatformCheck(){
        Vector3 start = context.transform.position;
        Vector3 dir = Vector3.down;
        Debug.DrawRay(start, dir, Color.red, 2f);

        RaycastHit2D linecast = Physics2D.Linecast(start, start + dir, 1 << LayerMask.NameToLayer("Ground"));
        //Check if hast the one way platform tag 
        if(linecast.collider != null && linecast.collider.CompareTag("OneWayPlatform")){
            //Check if player is above the platform
            Debug.Log(linecast.collider);
            context.FallOneWayPlatform(linecast.collider);
            if(linecast.point.y < start.y){
                //Disable collider
                context.collider2D.enabled = false;
                //Wait for 0.5 seconds
                //StartCoroutine(EnableColliderAfter(0.5f));
                return true;
            }
        }
        return false;
    }
}
