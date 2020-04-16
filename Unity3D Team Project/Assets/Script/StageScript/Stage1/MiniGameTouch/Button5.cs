using UnityEngine;
using System.Collections;

public class Button5 : MonoBehaviour
{
    public AudioSource ButtonClick;
    static public bool Clicked;

    void OnMouseDown()
    {
        if (!Clicked)
        {
            if (TouchMiniGame.Logic)
            {
                if (MainMenu.EffectValue)
                    ButtonClick.Play();
                TouchMiniGame.Array[TouchMiniGame.Clicked_Num] = 4;
                TouchMiniGame.Clicked_Num += 1;
                transform.GetComponent<Animator>().SetTrigger("Disappear");
            }
            Clicked = true;
        }
    }
}
