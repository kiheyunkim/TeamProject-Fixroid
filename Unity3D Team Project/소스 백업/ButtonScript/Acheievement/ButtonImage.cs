using UnityEngine;
using System.Collections;

public class ButtonImage : MonoBehaviour
{

    public UnityEngine.UI.Button[] Bttn;
    public Sprite[] UnLcoked;       //Imaged for UnLocked
    public Sprite[] Locked;         //Images for Locked

    private int ButtonLength;

    void Start()
    {
        ButtonLength = Bttn.Length;

    }
    void OnGUI()
    {
        for (int i = 0; i <ButtonLength; i++)
        {
            if (MainMenu.StageEnd[i])
                Bttn[i].image.sprite = UnLcoked[i];
            else
                Bttn[i].image.sprite = Locked[i];
        }
    }
    
}
