using UnityEngine;
using System.Collections;

public class Acheieve1 : MonoBehaviour
{
    public UnityEngine.UI.Button[] Bttn;
    public Sprite[] UnLcoked;       //Imaged for UnLocked
    public Sprite[] Locked;         //Images for Locked

    private int UnLockedLength;     //count for Unlocked
    private int LockedLength;       //count for locked

    void Start()
    {
        UnLockedLength = UnLcoked.Length;
        LockedLength = Locked.Length;

        for (int i = 0; i < LockedLength; i++)
            UnLcoked[i] = Resources.Load<Sprite>("");
        for (int i = 0; i < UnLockedLength; i++)
            Locked[i] = Resources.Load<Sprite>("");
    }
    void OnGUI()
    {

    }
    public UnityEngine.UI.Button Button1;
    public Sprite Acheive1;
    public Sprite Acheive1Locked;
    void OdnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Acheive1 = Resources.Load<Sprite>("1");       //Unlocked image
            Button1.image.sprite = Acheive1;
        }
        else
        {
            Acheive1Locked = Resources.Load<Sprite>("2"); //locked image
            Button1.image.sprite = Acheive1Locked;
        }
    }
}   
