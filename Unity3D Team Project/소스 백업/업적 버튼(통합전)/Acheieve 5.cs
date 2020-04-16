using UnityEngine;
using System.Collections;

public class Acheieve5 : MonoBehaviour
{

    public UnityEngine.UI.Button Button5;
    public Sprite Acheive5;
    public Sprite Acheive5Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive5 = Resources.Load<Sprite>("1");       //Unlocked image
            Button5.image.sprite = Acheive5;
        }
        else
        {
            Acheive5Locked = Resources.Load<Sprite>("2"); //locked image
            Button5.image.sprite = Acheive5Locked;
        }
    }
}
