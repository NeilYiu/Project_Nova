using UnityEngine;
using System.Collections;
using UnityEditor;

public class AttackBehaviour : StateMachineBehaviour {

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Player.Instance.isGrounded)
        {
            //bug: didn't work
            //Debug.Log(Player.Instance.GetComponent<Rigidbody2D>().velocity);
            //Player.Instance.GetComponent<Rigidbody2D>().AddForce(new Vector2(100f,0),ForceMode2D.Impulse);
            //Player.Instance.GetComponent<Rigidbody2D>().velocity = new Vector2(100f,0);
            //Debug.Log(Player.Instance.isAttacking);
            Player.Instance.isAttacking = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player.Instance.isAttacking = false;
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
