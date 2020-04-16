using UnityEngine;
using System.Collections;

public class Acheieve4 : MonoBehaviour
{

    public UnityEngine.UI.Button Button4;
    public Sprite Acheive4;
    public Sprite Acheive4Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive4 = Resources.Load<Sprite>("1");       //Unlocked image
            Button4.image.sprite = Acheive4;
        }
        else
        {
            Acheive4Locked = Resources.Load<Sprite>("2"); //locked image
            Button4.image.sprite = Acheive4Locked;
        }
    }
}
