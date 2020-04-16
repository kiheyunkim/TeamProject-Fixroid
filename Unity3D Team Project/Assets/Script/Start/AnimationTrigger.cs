using UnityEngine;
using System.Collections;

public class AnimationTrigger : MonoBehaviour
{
    public Animator Title;
    public Animator Button;
    static public bool TitleEnd = false;
	void Update ()
    {
        if (StartScript.isEnd)
            Title.GetComponent<Animator>().SetBool("StartTitle", true);
        if (TitleEnd)
            Button.GetComponent<Animator>().SetBool("StartButton", true);
        if (StartScript.isTouched)
            Destroy(this);
    }
}
