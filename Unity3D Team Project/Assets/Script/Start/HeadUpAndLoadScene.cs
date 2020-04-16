using UnityEngine;
using System.Collections;

public class HeadUpAndLoadScene : StateMachineBehaviour//When Animation Ends, Load Scene / this Script For Load Next Scene
{
	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Loading.NextSceneNumber = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        animator.SetBool("Exit", true);
    }
}
