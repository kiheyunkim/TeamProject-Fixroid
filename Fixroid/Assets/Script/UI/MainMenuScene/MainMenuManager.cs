using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public bool IsExitMenuOpen { get; set; }

    private float timeAValue;

    //private Sprite[] stageLockedCard = new Sprite[3];
    private GameObject[] pivots = new GameObject[4];
    private GameObject[] cards = new GameObject[4];

    private GUIStyle buttonOptionStyle = new GUIStyle();
    private GUIStyle buttonAcheiveStyle = new GUIStyle();
    private GUIStyle buttonNextStyle = new GUIStyle();
    private GUIStyle buttonPrevStyle = new GUIStyle();

    private Rect bttnOptRect;
    private Rect bttnAcheiveRect;
    private Rect bttnPrevRect;
    private Rect bttnNextRect;

    private UnityEngine.UI.Text timeText;
    private MainScrollMgr menuScrollMgr;
    private Animator miniAnimation;

    private AudioSource mainMenuBGM;
    private AudioSource robotAppear;
    private AudioSource buttonClick;

    private SaveManager saveManager;
    private SettingManager settingManager;

    private List<Texture2D> texture2Ds;

    void Awake()
    {
        timeAValue = 0;
        IsExitMenuOpen = false;

        bttnOptRect = new Rect(Screen.width * 0.739f, Screen.height * 0.028f, Screen.width * 0.086f, Screen.height * 0.153f);
        bttnAcheiveRect = new Rect(Screen.width * 0.175f, Screen.height * 0.820f, Screen.width * 0.086f, Screen.height * 0.153f);
        bttnPrevRect = new Rect(Screen.width * 0.095f, Screen.height * 0.424f, Screen.width * 0.086f, Screen.height * 0.153f);
        bttnNextRect = new Rect(Screen.width * 0.819f, Screen.height * 0.424f, Screen.width * 0.086f, Screen.height * 0.153f);

        mainMenuBGM = AudioSetter.SetBgm(gameObject, "Sound/MainMenu/MainMenu BGM");
        robotAppear = AudioSetter.SetEffect(gameObject, "Sound/MainMenu/RobotAppear");
        buttonClick = AudioSetter.SetEffect(gameObject, "Sound/MainMenu/ClickSound");
        
        texture2Ds = Utils.Sprites2Textures2D(Resources.LoadAll<Sprite>("MainMenu/MainMenu"));
        buttonOptionStyle.normal.background = texture2Ds[13];
        buttonOptionStyle.active.background = texture2Ds[12];
        buttonAcheiveStyle.normal.background = texture2Ds[17];
        buttonAcheiveStyle.active.background = texture2Ds[16];
        buttonNextStyle.normal.background = texture2Ds[15];
        buttonNextStyle.active.background = texture2Ds[14];
        buttonPrevStyle.normal.background = texture2Ds[11];
        buttonPrevStyle.active.background = texture2Ds[10];

        mainMenuBGM.Play();
        robotAppear.Play();
    }

    void Start()
    {
        for (int i = 0; i < pivots.Length; i++)
            pivots[i] = GameObject.Find("Pivot" + (i + 1));
        for (int i = 0; i < pivots.Length; i++)
            cards[i] = GameObject.Find("Card" + (i + 1));

        menuScrollMgr = GameObject.Find("Cards").GetComponentInChildren<MainScrollMgr>();
        timeText = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
        miniAnimation = Camera.main.GetComponentInChildren<Animator>();

        saveManager = SaveManager.GetInstance;
        settingManager = SettingManager.GetInstance;
        settingManager.NotWarning();


        for (int i = 0; i < 4; i++)
        {
            if (!saveManager.SaveTile.stageOpen[i + 1])
            {

            }
            else
            {
                if (saveManager.SaveTile.stageOpen[i + 1])
                    cards[i].GetComponentInChildren<Animator>().SetTrigger((i + 1) + "NoEnd");
                else
                    cards[i].GetComponentInChildren<Animator>().SetTrigger((i + 1) + "End");
            }
        }

        CalculateTime();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !IsExitMenuOpen)
            IsExitMenuOpen = true;

        timeText.color = new Color(0.457f, 0.457f, 0.457f, timeAValue);
        if (pivots[menuScrollMgr.StdIndex].transform.position.x.ToString("N1") == gameObject.transform.position.x.ToString("N1") && saveManager.SaveTile.stageOpen[menuScrollMgr.StdIndex+1])
            if (timeAValue != 1) timeAValue += 0.1f;
    }

    void OnGUI()
    {
        if (IsExitMenuOpen) return;

        GUI.depth = 3;

        if (GUI.Button(bttnOptRect, "", buttonOptionStyle))
        {
            buttonClick.Play();
            SceneManager.NextSceneNumber = 5;
            miniAnimation.SetTrigger("Continue");

        }

        if (GUI.Button(bttnAcheiveRect, "", buttonAcheiveStyle))
        {
            buttonClick.Play();
            SceneManager.NextSceneNumber = 6;
            miniAnimation.SetTrigger("Continue");
        }

        if (GUI.Button(bttnNextRect, "", buttonNextStyle))
        {
            buttonClick.Play();
            if (menuScrollMgr.StdIndex == 3) menuScrollMgr.StdIndex = 0;
            else menuScrollMgr.StdIndex += 1;
            CalculateTime();
        }

        if (GUI.Button(bttnPrevRect, "", buttonPrevStyle))
        {
            buttonClick.Play();
            if (menuScrollMgr.StdIndex == 0) menuScrollMgr.StdIndex = 3;
            else menuScrollMgr.StdIndex -= 1;
            CalculateTime();
        }
    }

    void CalculateTime()
    {
        timeAValue = 0;
        int minute, second, millsecond;
        float result = 0;

        result = saveManager.SaveTile.stageTime[menuScrollMgr.StdIndex + 1]; //-> stdIndex는 0부터
        
        minute = (int)result / 60;
        second = (int)result / 1;
        millsecond = (int)((result * 100) % 100f);
        timeText.text = minute.ToString("D2") + ":" + second.ToString("D2") + ":" + millsecond.ToString("D2");
    }
}