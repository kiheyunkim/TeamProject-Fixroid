using UnityEngine;
using System.Collections;

public class Button3 : MonoBehaviour
{
    public AudioSource ClickSound;
    static public bool Clicked;

    void OnMouseDown()
    {
        if (!Clicked)
        {
            if (TouchMiniGame.Logic)
            {
                if (MainMenu.EffectValue)
                    ClickSound.Play();
                TouchMiniGame.Array[TouchMiniGame.Clicked_Num] = 2;
                TouchMiniGame.Clicked_Num += 1;
                transform.GetComponent<Animator>().SetTrigger("Disappear");
            }
            Clicked = true;
        }
    }
}
