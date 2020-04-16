//Option에서
using UnityEngine;
using System.Collections;

public class Credit : MonoBehaviour
{

	// Use this for initialization
    public Texture BackgroundTexture;
    //for sound
    public AudioSource ClickSound;

    public GUIStyle ButtonExit;
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackgroundTexture);//Display our Background Texture

        if (GUI.Button(new Rect(Screen.width * 0.8974609375f, Screen.height * 0.817708333333333f, Screen.width * 0.0869140625f, Screen.height * 0.154513888888889f), "", ButtonExit))//modify button's position
        {
            if (MainMenu.EffectValue)
              ClickSound.Play();
            Loading.NextSceneNumber = 5;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }
}
