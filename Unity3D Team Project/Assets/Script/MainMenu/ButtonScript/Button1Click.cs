using UnityEngine;
using System.Collections;

public class Button1Click : MonoBehaviour
{
    //For Click Sound
    public AudioSource ClickSound;

    //For Button Sprite
    public Sprite Clicked;
    public Sprite Uped;

    //For Clicked 
    public float time;
    public bool clicked = false;

    void Update()
    {
        if(clicked)
        {
            if (time >= 0)//wait for time (sec)
            {
                time -= Time.deltaTime;
                return;
            }
            else
            { 
                gameObject.GetComponent<SpriteRenderer>().sprite = Uped;
                MainMenu.StageCardClick = true;
                MainMenu.ClickedStageNum = 1;
                clicked = false;
            }
        }
    }
    void OnMouseDown()
    {
        if (!MainMenuExit.ExitMenu)
        {
            if (MainMenu.EffectValue)
                ClickSound.Play();
            clicked = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = Clicked;
        }
    }

}
