using UnityEngine;
using System.Collections;

public class MinigameSuccess : MonoBehaviour
{
    //Global Trigger boolean
    static public bool Success;
    static public bool Fail;
    static public bool AlphaOff;

    //For Background
    public float Alpha;

    //For Sound
    public AudioSource SuccessSound;
    public AudioSource FailSound;

    public GameObject Animation;
    public GameObject Background;
	// Use this for initialization
	void Start ()
    {
        AlphaOff = true;
        Success = false;
        Fail = false;
        Alpha = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(!AlphaOff)
        {
            Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            Stage1UI.AlphaOn = true;
            Stage1UI.UI_OFF = false;
            AlphaOff = true;
        }
        if(!Success&&!Fail)
        {
            Alpha = 0;
        }

        if (Success || Fail)
        {
            Stage1UI.AlphaOn = false;
            Stage1UI.UI_OFF = true;
        }

        if (Success)
        {
            if(Alpha>0.97f)
            {
                Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                Animation.GetComponent<Animator>().SetTrigger("Success");
                if (MainMenu.EffectValue)
                    SuccessSound.Play();
            }
            else
            {
                Alpha += 0.03f;
                Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha);
            }
        }

        if (Fail)
        {
            if (Alpha > 0.97f)
            {
                Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                Animation.GetComponent<Animator>().SetTrigger("Fail");
                if (MainMenu.EffectValue)
                    FailSound.Play();
            }
            else
            {
                Alpha += 0.03f;
                Background.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, Alpha);
            }
        }
	}
}
