using UnityEngine;
using System.Collections;

public class Acheive10 : MonoBehaviour
{
    public UnityEngine.UI.Button Button10;
    public Sprite Acheive_10;
    public Sprite Acheive10Locked;
    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive_10 = Resources.Load<Sprite>("1");       //Unlocked image
            Button10.image.sprite = Acheive_10;
        }
        else
        {
            Acheive10Locked = Resources.Load<Sprite>("2"); //locked image
            Button10.image.sprite = Acheive10Locked;
        }
    }
}
