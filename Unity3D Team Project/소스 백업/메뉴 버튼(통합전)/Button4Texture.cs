using UnityEngine;
using System.Collections;

public class Button4Texture : MonoBehaviour
{
    public UnityEngine.UI.Button Button4;
    public Sprite Stage4;
    public Sprite Stage4Locked;

    void OnGUI()
    {
        if (MainMenu.StageEnd[3])
        {
            Stage4 = Resources.Load<Sprite>("7");
            Button4.image.sprite = Stage4;
        }
        else
        {
            Stage4Locked = Resources.Load<Sprite>("8");
            Button4.image.sprite = Stage4Locked;
            //pop up - NO Click
        }
    }
    public void Click()
    {
        if (MainMenu.StageEnd[3])
            UnityEngine.SceneManagement.SceneManager.LoadScene("Stage4");
        else
        { }
        //pop up - NO! Click
    }
}
