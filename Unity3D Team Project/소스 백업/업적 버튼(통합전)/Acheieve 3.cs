using UnityEngine;
using System.Collections;

public class Acheieve3 : MonoBehaviour
{
    public UnityEngine.UI.Button Button3;
    public Sprite Acheive3;
    public Sprite Acheive3Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive3 = Resources.Load<Sprite>("1");       //Unlocked image
            Button3.image.sprite = Acheive3;
        }
        else
        {
            Acheive3Locked = Resources.Load<Sprite>("2"); //locked image
            Button3.image.sprite = Acheive3Locked;
        }
    }
}
