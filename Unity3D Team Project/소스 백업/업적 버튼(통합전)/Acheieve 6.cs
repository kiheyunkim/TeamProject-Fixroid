using UnityEngine;
using System.Collections;

public class Acheieve6 : MonoBehaviour
{

    public UnityEngine.UI.Button Button6;
    public Sprite Acheive6;
    public Sprite Acheive6Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive6 = Resources.Load<Sprite>("1");       //Unlocked image
            Button6.image.sprite = Acheive6;
        }
        else
        {
            Acheive6Locked = Resources.Load<Sprite>("2"); //locked image
            Button6.image.sprite = Acheive6Locked;
        }
    }
}
