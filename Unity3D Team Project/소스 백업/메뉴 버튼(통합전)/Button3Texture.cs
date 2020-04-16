using UnityEngine;
using System.Collections;

public class Button3Texture : MonoBehaviour
{
    public UnityEngine.UI.Button Button3;
    public Sprite Stage3;
    public Sprite Stage3Locked;

    void OnGUI()
    {
        if (MainMenu.StageEnd[2])
        {
            Stage3 = Resources.Load<Sprite>("5");
            Button3.image.sprite = Stage3;
        }
        else
        {
            Stage3Locked = Resources.Load<Sprite>("6");
            Button3.image.sprite = Stage3Locked;
            //pop up - NO Click
        }
    }
    public void Click()
    {
        if (MainMenu.StageEnd[2])
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage3");
        else
        { }
        //pop up - NO! Click
    }
}
