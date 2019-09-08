using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingEvent : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject car;
        GameObject costumer;
        GameObject cp = animator.gameObject;
        car = cp.transform.GetChild(0).gameObject;
        car.transform.eulerAngles = new Vector3(0, 0, 0);
        costumer = cp.transform.GetChild(1).gameObject;

        Rigidbody carRb = car.GetComponent<Rigidbody>();
        carRb.velocity = carRb.velocity = (new Vector3(0, 0, 0));
        carRb.angularVelocity = new Vector3(0, 0, 0);

        cp.transform.position = new Vector3(0, 0, -48);
        car.transform.position = new Vector3(59.2f, 0, -61.3f);
        costumer.transform.position = new Vector3(66.4f, 0, -54.6f);
        Debug.Log("coming start position:" + car.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}


    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GameObject costumer;
        GameObject car;
        CarPeopleController cpController;

        GameObject cp = animator.gameObject;
        //car = cp.transform.GetChild(0).gameObject;
        //car.transform.GetComponent<Collider>().enabled = false;
        //costumer = cp.transform.GetChild(1).gameObject;
        cpController =
                        (CarPeopleController)cp.GetComponent(typeof(CarPeopleController));
        cpController.carWaiting();

    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
