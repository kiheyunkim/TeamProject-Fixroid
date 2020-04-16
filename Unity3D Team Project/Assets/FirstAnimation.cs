using UnityEngine;
using System.Collections;

public class FirstAnimation : MonoBehaviour
{
    //Global Trigger Boolean
    public bool StartAnimation;
    static public bool AnimationEnd;
    
    public GameObject Background;
    public GameObject Animation;

    private float Alpha_Value;
	// Use this for initialization
	void Start ()
    {
        StartAnimation = true;
        AnimationEnd = false;
        Alpha_Value = 0;
        Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        Animation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!AnimationEnd)
        {
            if (StartAnimation)
            {
                Alpha_Value += 0.14f;
                if (Alpha_Value > 0.95f)
                {
                    Alpha_Value = 1;
                    StartAnimation = false;
                    Animation.GetComponent<Animator>().SetBool("Stage", true);
                }
                Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha_Value);
                Animation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha_Value);
            }
        }
        else
        {
            if(Alpha_Value>0)
            {
                Alpha_Value -= 0.14f;
                if (Alpha_Value < 0.1f)
                {
                    Alpha_Value = 0;
                }
                Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha_Value);
                Animation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha_Value);
            }
            if(Alpha_Value==0)
            {
                Stage1UI.Destoryed_anime = true;
                Destroy(Background);
                Destroy(Animation);
                Destroy(this);
            }
        }
    }
}
