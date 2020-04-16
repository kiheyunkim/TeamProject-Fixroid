using UnityEngine;
using System.Collections;

public class Acheieve9 : MonoBehaviour
{
    public UnityEngine.UI.Button Button9;
    public Sprite Acheive9;
    public Sprite Acheive9Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive9 = Resources.Load<Sprite>("1");       //Unlocked image
            Button9.image.sprite = Acheive9;
        }
        else
        {
            Acheive9Locked = Resources.Load<Sprite>("2"); //locked image
            Button9.image.sprite = Acheive9Locked;
        }
    }
}
