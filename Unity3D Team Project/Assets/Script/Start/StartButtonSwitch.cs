using UnityEngine;
using System.Collections;

public class StartButtonSwitch : MonoBehaviour//Script For Button Switching
{
    //For Button Texture
    public float time = 0.5f;
    public GameObject Button;
    public Sprite ClickedImage;
    public Sprite UnClickedImage;

    //For Button Clicked
    public static bool Clicked = false; //true : clicked, false : Unclicked

    //For Button Switching End
    private bool StageMove = false; //true : End, false : Not End

    //For Next Animation
    public GameObject RobotAnimation;
    public Sprite Head_First;
    public Sprite Head_Final;


	void Update ()
    {
        if (StartScript.isTouched&&StartScript.isEnd)//button clicked
        {
            RobotAnimation.GetComponent<SpriteRenderer>().sprite = Head_First;
            if (Clicked)
                Button.GetComponent<SpriteRenderer>().sprite = ClickedImage;//UnClickedImage;
            else
            {
                Button.GetComponent<SpriteRenderer>().sprite = ClickedImage;
                if (time >= 0)//wait for time (sec)
                {
                    time -= Time.deltaTime;
                    return;
                }
                else
                {
                    Clicked = true;
                    StageMove = true;
                }
            }
        }
        if(StageMove)
        {
            RobotAnimation.GetComponent<Animator>().SetTrigger("HeadUp");
        }          
   }
}
