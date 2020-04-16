using UnityEngine;
using System.Collections;

public class Button2Texture : MonoBehaviour
{
    public UnityEngine.UI.Button Button2;
    public Sprite Stage2;
    public Sprite Stage2Locked;

    void OnGUI()
    {
        if (MainMenu.StageEnd[1])
        {
            Stage2 = Resources.Load<Sprite>("3");
            Button2.image.sprite = Stage2;
        }
        else
        {
            Stage2Locked = Resources.Load<Sprite>("4");
            Button2.image.sprite = Stage2Locked;
            //pop up - NO Click
        }
    }
    public void Click()
    {
        if (MainMenu.StageEnd[1])
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage2");
        else
        { }
        //pop up - NO! Click
    }
}
