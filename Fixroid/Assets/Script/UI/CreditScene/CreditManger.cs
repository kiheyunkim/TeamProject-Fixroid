using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditManger : MonoBehaviour
{
    private Texture bgkTexture;
    private GUIStyle buttonExit = new GUIStyle();

    private Rect screenRect;
    private Rect buttonRect;

    private AudioSource clickSound;
    private List<Texture2D> texture2Ds;
    void Awake()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        buttonRect = new Rect(Screen.width * 0.8975f, Screen.height * 0.8177f, Screen.width * 0.0869f, Screen.height * 0.1545f);
    }

    void Start()
    {
        texture2Ds = Utils.Sprites2Textures2D(Resources.LoadAll<Sprite>("Credit/CreditUI"));

        buttonExit.normal.background = texture2Ds[2];
        buttonExit.active.background = texture2Ds[1];
        bgkTexture = texture2Ds[0];

        clickSound = AudioSetter.SetEffect(gameObject, "Sound/Credit/CreditBttn");
    }


    void OnGUI()
    {
        GUI.DrawTexture(screenRect, bgkTexture);
        if (GUI.Button(buttonRect, "", buttonExit))
        {
            clickSound.Play();
            SceneManager.NextSceneNumber = 5;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }
}
