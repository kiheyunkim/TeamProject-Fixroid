using UnityEngine;
using System.Collections;

public class HandleTrigger : MonoBehaviour
{
    public bool Clicked = false;
    public GameObject MiniGameHandleObject;
    public GameObject UI;

    void Update()
    {
        if(MiniGameHandle.Success)
        {
            Clicked = true;
            ThridSectorEnter.ThirdSector = true;
            MiniGameHandle.Success = false;
        }
    }
    void OnMouseDown()
    {
        if (!Stage1UI.ItemOpen && !Stage1UI.PauseOpen)
        {
            if (!Clicked)
            {
                if (HandScript.EquippedItem != Stage1UI.Item.Spaner)
                {
                    HandScript.isNotMatch = true;
                    HandScript.Blink = true;
                }
                else
                {
                    Stage1UI.AlphaOn = false;
                    Stage1UI.UI_OFF = true;
                    MiniGameHandleObject.SetActive(true);
                    MiniGameHandle.Success = false;
                    MiniGameHandle.Fail = false;
                    MiniGameHandle.Gamestart = true;
                }
            }
        }
    }
}
