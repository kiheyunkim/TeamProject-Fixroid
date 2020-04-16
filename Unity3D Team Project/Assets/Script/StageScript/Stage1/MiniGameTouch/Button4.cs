using UnityEngine;
using System.Collections;

public class Button4 : MonoBehaviour
{
    public AudioSource ButtonClick;
    static public bool Clicked;

    void OnMouseDown()
    {
        if (!Clicked)
        {
            if (MainMenu.EffectValue)
                ButtonClick.Play();

            if (TouchMiniGame.Logic)
            {
                TouchMiniGame.Array[TouchMiniGame.Clicked_Num] = 3;
                TouchMiniGame.Clicked_Num += 1;
                transform.GetComponent<Animator>().SetTrigger("Disappear");
            }
            Clicked = true;
        }
    }
}
