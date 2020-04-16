using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainExitMenu : MonoBehaviour
{
    private GUIStyle prevButton = new GUIStyle();
    private GUIStyle exitButton = new GUIStyle();
    private Rect prevBttnRect;
    private Rect exitBttnRect;
    private Rect screenRect;
    
    private MainMenuManager MainMgr;

    private Texture alphaBkg;
    private AudioSource exitBttnSound;
    private AudioSource prevBttnSound;

    private List<Texture2D> texture2Ds;

    void Awake()
    {
        MainMgr = gameObject.GetComponent<MainMenuManager>();    
        prevBttnRect = new Rect(Screen.width * 0.3773f, Screen.height * 0.5583f, Screen.width * 0.075f, Screen.height * 0.13f);
        exitBttnRect = new Rect(Screen.width * 0.55f, Screen.height * 0.5583f, Screen.width * 0.075f, Screen.height * 0.13f);
        screenRect = new Rect(0, 0, Screen.width, Screen.height);

        exitBttnSound = AudioSetter.SetEffect(gameObject, "Sound/MainMenu/exitBttn");
        prevBttnSound = AudioSetter.SetEffect(gameObject, "Sound/MainMenu/ClickSound");
    }
    
    void Start()
    {
        texture2Ds = Utils.Sprites2Textures2D(Resources.LoadAll<Sprite>("MainMenu/MainMenu"));
        
        prevButton.normal.background = texture2Ds[6];
        prevButton.active.background = texture2Ds[5];
        exitButton.normal.background = texture2Ds[3];
        exitButton.active.background = texture2Ds[4];
        alphaBkg = texture2Ds[19];
    }

    void OnGUI()
    {
        if (!MainMgr.IsExitMenuOpen) return;

        GUI.depth = 0;
        GUI.DrawTexture(screenRect, alphaBkg);

        if (GUI.Button(exitBttnRect, "", exitButton))
        {
            exitBttnSound.Play();
            Application.Quit();
        }

        if(GUI.Button(prevBttnRect,"",prevButton))
        {
            prevBttnSound.Play();
            MainMgr.IsExitMenuOpen = false;
        }
    }
}
