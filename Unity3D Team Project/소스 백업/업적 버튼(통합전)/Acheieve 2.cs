using UnityEngine;
using System.Collections;

public class Acheieve2 : MonoBehaviour {

    public UnityEngine.UI.Button Button2;
    public Sprite Acheive2;
    public Sprite Acheive2Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive2 = Resources.Load<Sprite>("1");       //Unlocked image
            Button2.image.sprite = Acheive2;
        }
        else
        {
            Acheive2Locked = Resources.Load<Sprite>("2"); //locked image
            Button2.image.sprite = Acheive2Locked;
        }
    }
}
