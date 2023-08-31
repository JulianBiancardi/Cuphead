using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion_Death : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        //Shake the camera
        CameraShake.Instance.ShakeCamera(1f, 0.2f);

        //Play death sound that is inside the enemy game object
        animator.GetComponent<OllieBulb>().playDeathSound();
        
        //Deactivate the tear spawns
        animator.GetComponent<OllieBulb>().stopthrowTears();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
