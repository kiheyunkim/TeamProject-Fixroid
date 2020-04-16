using UnityEngine;
using System.Collections;

public class Acheieve8 : MonoBehaviour
{
    public UnityEngine.UI.Button Button8;
    public Sprite Acheive8;
    public Sprite Acheive8Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive8 = Resources.Load<Sprite>("1");       //Unlocked image
            Button8.image.sprite = Acheive8;
        }
        else
        {
            Acheive8Locked = Resources.Load<Sprite>("2"); //locked image
            Button8.image.sprite = Acheive8Locked;
        }
    }
}
