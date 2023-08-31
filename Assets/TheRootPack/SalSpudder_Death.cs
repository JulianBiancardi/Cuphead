using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalSpudder_Death : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Shake the camera 
        CameraShake.Instance.ShakeCamera(1f, 0.2f);   
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Move the enemy to the bottom center of the level
        animator.gameObject.transform.position += new Vector3(-5, 0, 0);
       
    }
}
