using UnityEngine;
using System.Collections;

public class Acheive1 : MonoBehaviour
{ 

    //For Button Sprite
    public Sprite Clicked_lcoked;
    public Sprite Uped_locked;
    public Sprite Clicked_Unlcok;
    public Sprite Uped_Unlock;
    public Sprite Clicked;
    public Sprite Uped;

    //For Clicked 
    public float time;
    public bool clicked = false;

    //For Click Sound
    public AudioSource ClickSound;

    void Start()
    {
        time = 0.5f;
        if (Acheievement.isGet[0])
        {
            Clicked = Clicked_Unlcok;
            Uped = Uped_Unlock;
        }
        else
        {
            Clicked = Clicked_lcoked;
            Uped = Uped_locked;
        }
    }
    void Update()
    {
        

        if (clicked)
        {
            if (time >= 0)//wait for time (sec)
            {
                time -= Time.deltaTime;
                return;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = Uped;
                time = 0.5f;
                Acheievement.PopupOpen = true;
                clicked = false;
            }
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = Uped;
        }
    }
    void OnMouseDown()
    {
        if (!Acheievement.PopupOpen&& Acheievement.isGet[0])
        {
            if (MainMenu.EffectValue)
                ClickSound.Play();
            clicked = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = Clicked;
        }
    }
}
