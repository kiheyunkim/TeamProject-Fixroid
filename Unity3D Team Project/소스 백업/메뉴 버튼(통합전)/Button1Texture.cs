using UnityEngine;
using System.Collections;

public class Button1Texture : MonoBehaviour
{

    public UnityEngine.UI.Button Button1;
    public Sprite Stage1;
    public Sprite Stage1Locked;

    void OnGUI()
    {
        if (MainMenu.StageEnd[0])
        {
            Stage1 = Resources.Load<Sprite>("1");
            Button1.image.sprite = Stage1;
        }
        else
        {
            Stage1Locked = Resources.Load<Sprite>("2");
            Button1.image.sprite = Stage1Locked;
            //pop up - NO Click
        }
    }
    public void Click()
    {
        if (MainMenu.StageEnd[0])
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1");
        else
        { }
        //pop up - NO! Click
    }
}
