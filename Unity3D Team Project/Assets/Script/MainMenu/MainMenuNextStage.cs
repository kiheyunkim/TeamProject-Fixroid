using UnityEngine;
using System.Collections;

public class MainMenuNextStage : StateMachineBehaviour      //Scrpit for Loading when mini-Animation End
{

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	//override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	// override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (MainMenu.ClickedStageNum == 1)
        {
            Loading.NextSceneNumber = 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
        if (MainMenu.ClickedStageNum == 2)
        {
            Loading.NextSceneNumber = 2;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
        if (MainMenu.ClickedStageNum == 3)
        {
            Loading.NextSceneNumber = 3;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
        if (MainMenu.ClickedStageNum == 4)
        {
            Loading.NextSceneNumber = 4;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
       if (MainMenu.ClickedStageNum == 5)
       {
           Loading.NextSceneNumber = 5;
           UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
       }
       if (MainMenu.ClickedStageNum == 6)
       {
           Loading.NextSceneNumber = 6;
           UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
       }
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
