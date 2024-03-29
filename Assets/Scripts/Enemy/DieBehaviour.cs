﻿using UnityEngine;
using System.Collections;

public class DieBehaviour : StateMachineBehaviour
{
    private float resurrectCoolDown=10;
    private float deathTimer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.GetComponent<Enemy>().meleeCollider.enabled = false;
        deathTimer = 0;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deathTimer += Time.deltaTime;
        if (deathTimer >= resurrectCoolDown)
        {
            //animator.gameObject.SetActive(true);
            animator.GetComponent<Enemy>().isDying = false;
            animator.GetComponent<Enemy>().healthBarUI.fillAmount = 1;
            animator.SetTrigger("isResurrected");
            animator.GetComponent<Enemy>().Start();
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("isResurrected");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
