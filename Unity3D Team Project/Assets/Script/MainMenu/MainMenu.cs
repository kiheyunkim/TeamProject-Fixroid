//Script for MainMenu
using UnityEngine;
using System.Collections;

//BackgournTexure is in the Canvas
public class MainMenu : MonoBehaviour
{
    //for Card Animation and Image
    public Animator[] Card;
    public Animator[] Card_Clear;
    public Sprite[] StageLockedCard;

    //Setting Load Option
    static public int isFirst;                                     //is First Excution?
                                                                   
    //Global Value for Stage                                       
    static public bool[] StageNoEnd = new bool[4];                 //is Non-Cleared Stage?
    static public bool[] StageEnd = new bool[4];                   //is Cleared Stage?
    static public bool[] StageLocked = new bool[4];                //is Opened Stage?
                                                                   
    //Global Value for Achievement                                 
    static public bool[] Acheive = new bool[10];                   
                                                                   
    //Global Value for Option                                      
    static public bool BGMValue = true;                            
    static public bool EffectValue = true;
    static public bool VibrateValue = true;
    static public bool LeftOrRight;                               // false = Right
                                                                   
    //Button GUIStyle And Texture                                             
    public GUIStyle ButtonOption;                                   //for Texture Without GUI OutLine, With Texture
    public GUIStyle ButtonAcheievement;                             //for Texture Without GUI OutLine, With Texture
    public GUIStyle ButtonNext;
    public GUIStyle ButtonPrev;
    public Texture2D ButtonOptionActive;                            //When Exit Menu Activate
    public Texture2D ButtonAcheievementActive;                      //When Exit Menu Activate
    public Texture2D ButtonNextActive;                              //When Exit Menu Activate
    public Texture2D ButtonPrevActive;                              //When Exit Menu Activate

    //for sound
    public AudioSource MainMenu_BGM;
    public AudioSource RobotAppear;
    public AudioSource ButtonClick;
    public AudioSource RobotDisappear;
    public AudioSource ExitMenuOpen;

    //for Stage Button Click
    static public bool StageCardClick;
    static public int ClickedStageNum;
    public GameObject SmallAnimation;

    //TimeAttack Times
    public GameObject[] Pivot = new GameObject[4];
    public UnityEngine.UI.Text TimeText;
    private int Minute;
    private int Second;
    private int Milisecond;
    static public float Stage1Result = 0;
    static public float Stage2Result = 0;
    static public float Stage3Result = 0;
    static public float Stage4Result = 0;
    private float TimeTextAlpha;

    void Click_Sound()
    {
        if (EffectValue)
            ButtonClick.Play();
    }

    void Awake()
    {
        for (int i = 0; i < Card.Length; i++)
        {
            if (StageLocked[i])
                GameObject.Find(Card[i].name).GetComponent<SpriteRenderer>().sprite = StageLockedCard[i];//For LockedStage Texture
            else
            {
                if (StageNoEnd[i])//For unlocked but No Clear case Animation
                {
                    Card[i].SetBool("Stage" + i + "NoEnd", true);
                    Card[i].SetBool("Stage" + i + "End", false);
                }
                if (StageEnd[i])//For unlocked & Cleared Case Animation
                {
                    Card[i].SetBool("Stage" + i + "NoEnd", false);
                    Card[i].SetBool("Stage" + i + "End", true);
                }
            }
        }
    }
    void Start()
    {
        Screen.SetResolution(1280, 720, true);
        //Debug.Log(Card[0].GetBool("Stage0NoEnd"));
        //Debug.Log(StageLocked[0]);
        //Debug.Log(StageNoEnd[0]);
        //Debug.Log(StageLocked[1]);
        //Debug.Log(StageLocked[2]);
        //Debug.Log(StageLocked[3]);
        TimeTextAlpha = 0;
        //ExitAlpha.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);//

        StageCardClick = false;
        ClickedStageNum = -1;
        
        if (BGMValue)
            MainMenu_BGM.Play();

        //for (int i = 0; i < Card.Length; i++)
        //    Card[i].GetComponent<Animator>().Play("");
    }
    void Update()
    {
        //No Click when ExitMenu Open
        if (MainMenuExit.ExitMenu)
        {
            ButtonOption.active.background = null;
            ButtonAcheievement.active.background = null;
            ButtonPrev.active.background = null;
            ButtonNext.active.background = null;
        }
        else
        {
            ButtonOption.active.background = ButtonOptionActive;
            ButtonAcheievement.active.background = ButtonAcheievementActive;
            ButtonPrev.active.background = ButtonPrevActive;
            ButtonNext.active.background = ButtonNextActive;
        }
        TimeText.color = new Color(0.45703125f, 0.45703125f, 0.45703125f, TimeTextAlpha);

        if (StageCardClick)
        {
            if(!RobotDisappear.isPlaying)
                RobotDisappear.Play();
            SmallAnimation.GetComponent<Animator>().SetTrigger("Disappear");
            StageCardClick = false;
        }


        //for Timers
        Minute = 0;
        Second = 0;
        Milisecond = 0;
        TimeText.text = "    ";

        for (int i = 0; i < 4; i++)
        {
            if (Pivot[i].transform.position.x.ToString("N2") == gameObject.transform.position.x.ToString("N2"))
            {
                if (TimeTextAlpha !=1)
                    TimeTextAlpha += 0.1f;
                if (i == 0)
                    if (Stage1Result != 0)
                    {
                        Minute = (int)Stage1Result / 60;
                        Second = (int)Stage1Result / 1;
                        Milisecond = (int)((Stage1Result * 100) % 100f);
                        TimeText.GetComponent<UnityEngine.UI.Text>().text = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2");
                    }
                if (i == 1)
                    if (Stage2Result != 0)
                    {
                        Minute = (int)Stage2Result / 60;
                        Second = (int)Stage2Result / 1;
                        Milisecond = (int)((Stage2Result * 100) % 100f);
                        TimeText.GetComponent<UnityEngine.UI.Text>().text = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2");
                    }
                if (i == 2)
                    if (Stage3Result != 0)
                    {
                        Minute = (int)Stage3Result / 60;
                        Second = (int)Stage3Result / 1;
                        Milisecond = (int)((Stage3Result * 100) % 100f);
                        TimeText.GetComponent<UnityEngine.UI.Text>().text = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2");
                    }
                if (i == 3)
                    if (Stage4Result != 0)
                    {
                        Minute = (int)Stage4Result / 60;
                        Second = (int)Stage4Result / 1;
                        Milisecond = (int)((Stage4Result * 100) % 100f);
                        TimeText.GetComponent<UnityEngine.UI.Text>().text = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2");
                    }
            }
        }
    }
    void OnGUI()
    {
        GUI.depth = 1;

        if (GUI.Button(new Rect(Screen.width * 0.7390625f, Screen.height * 0.02777777778f, Screen.width * 0.0859375f, Screen.height * 0.1527777777778f), "", ButtonOption))//For Option Button
        {
            if (!MainMenuExit.ExitMenu)
            {
                Click_Sound();
                ClickedStageNum = 5;
                SmallAnimation.GetComponent<Animator>().SetTrigger("Disappear");
            }
        }
        if (GUI.Button(new Rect(Screen.width * 0.175f, Screen.height * 0.8194444444444f, Screen.width * 0.0859375f, Screen.height * 0.1527777777778f), "", ButtonAcheievement))//For Achievement ButtonRobotappear
        {
            if (!MainMenuExit.ExitMenu)
            {
                Click_Sound();
                ClickedStageNum = 6;
                SmallAnimation.GetComponent<Animator>().SetTrigger("Disappear");
            }
        }
        if (GUI.Button(new Rect(Screen.width * 0.0953125f, Screen.height * 0.4236111111111f, Screen.width * 0.0859375f, Screen.height * 0.15277777777777778f), "", ButtonPrev))//For Prev Button
        {
            if (!MainMenuExit.ExitMenu)
            {
                if (MainMenu_SCroll.MidCard > 0)
                {
                    Click_Sound();
                    TimeTextAlpha = 0;
                    MainMenu_SCroll.MidCard -= 1;
                }
            }
        }
        if (GUI.Button(new Rect(Screen.width * 0.81875f, Screen.height * 0.4236111111111f, Screen.width * 0.0859375f, Screen.height * 0.15277777777777778f), "", ButtonNext))//For Nex Button
        {
            if (!MainMenuExit.ExitMenu)
            {
                if (MainMenu_SCroll.MidCard < 3)
                {
                    Click_Sound();
                    TimeTextAlpha = 0;
                    MainMenu_SCroll.MidCard += 1;
                }
            }
        }
        //For Escape Click (Exit Menu)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!MainMenuExit.ExitMenu)
            {
                if(EffectValue)
                    ExitMenuOpen.Play();
                MainMenuExit.ExitMenu = true;
            }
        }
    }
}

