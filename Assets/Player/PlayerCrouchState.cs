using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchState : State
{

    private enum Direction
    {
        Left = -1,
        Right = 1
    }
    private Direction direction = Direction.Right;

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
        } else if(Input.GetKeyDown(KeyCode.Space)){
            OneWayPlatformCheck();
        }

        float horizontal = Input.GetAxis("Horizontal");
        Flip(horizontal);
        Shoot();
    }

    public override void Exit()
    {
        context.animator.SetBool("isCrouching", false);
    }

    void Flip(float horizontal)
    {
        if(horizontal > 0){
            direction = Direction.Right;
            context.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(horizontal < 0){
            direction = Direction.Left;
            context.transform.rotation = Quaternion.Euler(0, -180, 0);
        }
    }

    void Shoot(){
        if(Input.GetKey(KeyCode.E)){
            context.animator.SetBool("isShooting", true);
            context.playerWeapon.Shoot(Quaternion.Euler(0, context.transform.rotation.eulerAngles.y, 0));
        } else if(Input.GetKeyUp(KeyCode.E)){
            context.animator.SetBool("isShooting", false);
            context.playerWeapon.StopShooting();
        }
    }

    void OneWayPlatformCheck(){
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
            }
        }
    }
}
