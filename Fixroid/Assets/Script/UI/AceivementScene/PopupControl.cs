using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupControl : MonoBehaviour
{
    //Button
    private Texture2D leftBttn;
    private Texture2D leftBttnClk;
    private Texture2D rightBttn;
    private Texture2D rightBttnClk;
    private Texture2D exit;
    private Texture2D exitClk;

    //Cut Scene
    private List<Texture> cutScene1 = new List<Texture>();
    private List<Texture> cutScene2 = new List<Texture>();

    //popup UI
    private GUIStyle leftBttnStyle = new GUIStyle();
    private GUIStyle rightBttnStyle = new GUIStyle();
    private List<Texture> targetScene = new List<Texture>();

    private int maxIndex;
    private int currentPos;
    private bool isUpdated;

    //Sound
    private AudioSource lrClkBttn;
    private AudioSource xButton;

    //Rects
    private Rect leftBttnRect;
    private Rect rightBttnRect;
    private Rect screenRect;

    //AcheiveMgr
    private AcheiveManager AcheiveMgr;

    //Activate
    public bool Activated { get; set; }

    private List<Texture2D> texture2Ds;

    void Awake()
    {
        currentPos = 0;
        screenRect = new Rect(0, 0, Screen.width, Screen.height);
        leftBttnRect = new Rect(Screen.width * 0.0156f, Screen.height * 0.83f, Screen.width * 0.0781f, Screen.height * 0.1389f);
        rightBttnRect = new Rect(Screen.width * 0.9063f, Screen.height * 0.83f, Screen.width * 0.0781f, Screen.height * 0.1389f);

        isUpdated = false;
    }

    void Start()
    {
        lrClkBttn = AudioSetter.SetEffect(gameObject, "Sound/Acheievement/AcheieveBttn");
        xButton = AudioSetter.SetEffect(gameObject, "Sound/Acheievement/AcheieveXBttn");

        texture2Ds = Utils.Sprites2Textures2D(Resources.LoadAll<Sprite>("Acheivement/AceiveIntegrate"));

        leftBttn = texture2Ds[6];
        leftBttnClk = texture2Ds[9];
        rightBttn = texture2Ds[11];
        rightBttnClk = texture2Ds[12];
        exit = texture2Ds[13];
        exitClk = texture2Ds[15];

        cutScene1.Add(texture2Ds[0]);
        cutScene1.Add(texture2Ds[16]);
        cutScene1.Add(texture2Ds[21]);
        cutScene2.Add(texture2Ds[1]);
        cutScene2.Add(texture2Ds[17]);
        cutScene2.Add(texture2Ds[22]);
        cutScene2.Add(texture2Ds[25]);

        AcheiveMgr = Camera.main.GetComponentInChildren<AcheiveManager>();

        initializeButtons();
        targetScene.AddRange(cutScene1);
        maxIndex = 3;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isUpdated) return;

        if (currentPos == 0)
        {
            leftBttnStyle.normal.background = null;
            leftBttnStyle.active.background = null;
            isUpdated = false;
            return;
        }
        else
        if (currentPos == maxIndex - 1)
        {
            rightBttnStyle.normal.background = exit;
            rightBttnStyle.active.background = exitClk;
            isUpdated = false;
            return;
        }
        else
        {
            leftBttnStyle.normal.background = leftBttn;
            leftBttnStyle.active.background = leftBttnClk;
            rightBttnStyle.normal.background = rightBttn;
            rightBttnStyle.active.background = rightBttnClk;
            isUpdated = false;
            return;
        }
    }

    void OnGUI()
    {
        if (!Activated) return;

        GUI.depth = 1;

        if (currentPos >= 0 && currentPos <= maxIndex)
            GUI.DrawTexture(screenRect, targetScene[currentPos]);

        if(GUI.Button(leftBttnRect,"",leftBttnStyle))
        {
            lrClkBttn.Play();
            if (currentPos > 0)
                currentPos -= 1;
            isUpdated = true;
            return;
        }

        if (GUI.Button(rightBttnRect, "", rightBttnStyle))
        {
            if (currentPos < maxIndex - 1)
            {
                lrClkBttn.Play();
                currentPos += 1;
                isUpdated = true;
                return;
            }
            if (currentPos == maxIndex - 1)
            {
                Activated = false;
                xButton.Play();
                initializeButtons();
                isUpdated = false;
                AcheiveMgr.StopPopup();
                return;
            }
        }
    }

    public void switchTexture(int cutNumber)
    {
        switch(cutNumber)
        {
            case 0:
                targetScene.Clear();
                targetScene.AddRange(cutScene1);
                break;
            case 1:
                targetScene.Clear();
                targetScene.AddRange(cutScene2);
                break;
            
        }
        maxIndex = targetScene.Count;
        currentPos = 0;
    }

    void initializeButtons()
    {
        currentPos = 0;
        leftBttnStyle.normal.background = null;
        leftBttnStyle.active.background = null;
        rightBttnStyle.normal.background = rightBttn;
        rightBttnStyle.active.background = rightBttnClk;
    }
}
