using UnityEngine;
using System.Collections;

public class Acheieve7 : MonoBehaviour
{

    public UnityEngine.UI.Button Button7;
    public Sprite Acheive7;
    public Sprite Acheive7Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive7 = Resources.Load<Sprite>("1");       //Unlocked image
            Button7.image.sprite = Acheive7;
        }
        else
        {
            Acheive7Locked = Resources.Load<Sprite>("2"); //locked image
            Button7.image.sprite = Acheive7Locked;
        }
    }
}
