using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcheiveManager : MonoBehaviour
{
    public bool IsPopUpOpen { get; set; }

    //BackGround Texture
    private Texture bkgTexture;

    //UI Buttons
    private GUIStyle nextBttn = new GUIStyle();
    private GUIStyle prevBttn = new GUIStyle();
    private GUIStyle exitBttn = new GUIStyle();

    //For Sound
    private AudioSource acheiveBGM;
    private AudioSource bttnClick;
    private AudioSource xBttnClick;

    private Rect screenRect;
    private Rect prevRect;
    private Rect nextRect;
    private Rect exitRect;

    private List<Texture2D> texture2Ds;

    private PopupControl popControl;
    private ScrollControl scrollMgr;

    private bool isUpdated;

    void Awake()
    {
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        prevRect = new Rect(Screen.width * 0.175f, Screen.height * 0.42361f, Screen.width * 0.085975f, Screen.height * 0.1527f);
        nextRect = new Rect(Screen.width * 0.7390625f, Screen.height * 0.42361f, Screen.width * 0.085975f, Screen.height * 0.1527f);
        exitRect = new Rect(Screen.width * 0.7390625f, Screen.height * 0.027f, Screen.width * 0.0859375f, Screen.height * 0.1527f);
    }

	void Start ()
    {
        acheiveBGM = AudioSetter.SetBgm(gameObject, "Sound/Acheievement/AcheieveBGM");
        bttnClick = AudioSetter.SetEffect(gameObject, "Sound/Acheievement/AcheieveBttn");
        xBttnClick = AudioSetter.SetEffect(gameObject, "Sound/Acheievement/AcheieveXBttn");

        texture2Ds = Utils.Sprites2Textures2D(Resources.LoadAll<Sprite>("Acheivement/AceiveIntegrate"));
        nextBttn.normal.background = texture2Ds[5];
        nextBttn.active.background = texture2Ds[4];
        prevBttn.normal.background = texture2Ds[3];
        prevBttn.active.background = texture2Ds[2];
        exitBttn.normal.background = texture2Ds[8];
        exitBttn.active.background = texture2Ds[7];
        bkgTexture = texture2Ds[29];

        popControl = gameObject.GetComponentInChildren<PopupControl>();
        scrollMgr = gameObject.GetComponentInChildren<ScrollControl>();
        isUpdated = false;

        acheiveBGM.Play();
    }
	
    void Update()
    {
        if (!isUpdated) return;
        popControl.switchTexture(scrollMgr.StandardIndex);
        isUpdated = false;
    }

    // Update is called once per frame
    void OnGUI()
    {
        if (IsPopUpOpen) return;

        GUI.depth = 2;
        GUI.DrawTexture(screenRect, bkgTexture);

        if (GUI.Button(prevRect, " ", prevBttn))
        {
            bttnClick.Play();
            if (scrollMgr.StandardIndex > 0)
                scrollMgr.StandardIndex -= 1;
            isUpdated = true;
        }

        if (GUI.Button(nextRect, " ", nextBttn))
        {
            bttnClick.Play();
            if (scrollMgr.StandardIndex < 5)
                scrollMgr.StandardIndex += 1;
            isUpdated = true;
        }

        if (GUI.Button(exitRect, " ", exitBttn))
        {
            xBttnClick.Play();
            SceneManager.NextSceneNumber = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }

    }

    public void StartPopup()
    {
        IsPopUpOpen = true;
        popControl.Activated = true;
    }

    public void StopPopup()
    {
        IsPopUpOpen = false;
        popControl.Activated = false;
    }
}
