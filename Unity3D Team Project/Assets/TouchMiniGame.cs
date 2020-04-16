using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TouchMiniGame : MonoBehaviour
{
    //For Sound
    public AudioSource ButtonSound;

    //Animation
    public GameObject SuccessAnimation;
    public GameObject BackGround;

    //Global Trigger Boolean
    static public bool FinalAnimationEnd;
    static public bool Success;
    static public bool Fail;
    static public bool RandomButtonGameStart;

    public struct ButtonsStruct
    {
        public bool Active;
        public float PosX;
        public float PosY;
    }
    public GameObject Camera;
    public GameObject Background;
    public List<int> ListX;

    public ButtonsStruct[] ButtonClick = new ButtonsStruct[5];
    public GameObject[] ButtonObject = new GameObject[5];

    //For Initialization
    private bool PositionSet;
    public int MaxNumberX;
    private int TempX;

    //For First Showing
    private bool Gamestart;
    private int CurrentPos;
    private float ActiveTime;

    //For Playing Logic
    static public bool Logic;
    static public int Clicked_Num;
    static public int[] Array = new int[5];

    //Final
    public bool Final;

    //For Time
    public bool TimerStart;
    public GameObject Timer;
    public GameObject TimerBack;
    public float LimitTime;

    void Start()
    {
        LimitTime = 7;

        Button1.Clicked = false;
        Button2.Clicked = false;
        Button3.Clicked = false;
        Button4.Clicked = false;
        Button5.Clicked = false;
        Success = false;
        Fail = false;
        Final = false;
        Logic = false;
        PositionSet = true;
        MaxNumberX = 5;
        CurrentPos = 0;
        Clicked_Num = 0;
        ActiveTime = 1.0f;
        RandomButtonGameStart = false;
        Gamestart = false;
        for (int i = 1; i <= 5; i++)
            ListX.Add(i);

    }

	void Update ()
    {
        if(TimerStart)
        {
            if(LimitTime<=0)
            {
                Fail = true;
                Debug.Log("Fail");
                RandomButtonGameStart = false;
                MinigameSuccess.Fail = true;
                Final = true;
                Button1.Clicked = false;
                Button2.Clicked = false;
                Button3.Clicked = false;
                Button4.Clicked = false;
                Button5.Clicked = false;
            }
            else
            {
                Timer.GetComponent<UnityEngine.UI.Image>().fillAmount = LimitTime / 7;
                LimitTime -= Time.deltaTime;
            }
        }
        Background.transform.position= Camera.transform.position;
        if(RandomButtonGameStart)
        {
            BackGround.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Stage1UI.UI_OFF = true;
            Stage1UI.AlphaOn = false;
            if(PositionSet)         //Setting Coord
            {
                Position_Setting();
                Gamestart = true;
                PositionSet = false;
                Timer.SetActive(true);
                TimerBack.SetActive(true);
            }
            if(Gamestart)           //Showing
            {
                if(ActiveTime>0)
                {
                    ActiveTime -= Time.deltaTime;
                }
                else
                {
                    ButtonObject[CurrentPos].transform.position = new Vector3(ButtonClick[CurrentPos].PosX, ButtonClick[CurrentPos].PosY, 0);
                    ButtonObject[CurrentPos].SetActive(true);
                    ButtonObject[CurrentPos].GetComponent<Animator>().SetTrigger("Appear");
                    CurrentPos += 1;
                    ActiveTime = 1.0f;
                    if (MainMenu.EffectValue)
                        ButtonSound.Play();
                    if(CurrentPos>=5)
                    {
                        Gamestart = false;
                        FinalAnimationEnd = true;
                    }
                }
            }
            //Game Main Logic
            if(FinalAnimationEnd)
            {
                Logic = true;
                TimerStart = true;
                FinalAnimationEnd = false;
            }
            if(Logic)
            {
                Check();
            }
            //GameStart()
        }
        else
        {
            if(Final)
            {
                Timer.SetActive(false);
                TimerBack.SetActive(false);
                Logic = false;
                PositionSet = true;
                MaxNumberX = 5;
                CurrentPos = 0;
                Clicked_Num = 0;
                ActiveTime = 1.5f;
                RandomButtonGameStart = false;
                Gamestart = false;
                TimerStart = false;
                LimitTime = 7;
                Timer.GetComponent<UnityEngine.UI.Image>().fillAmount = 1;
                ListX.Clear();
                for (int j = 1; j <= 5; j++)
                {
                    ButtonObject[j - 1].GetComponent<SpriteRenderer>().sprite = null;
                    ListX.Add(j);
                    Array[j - 1] = 5;
                    ButtonObject[j - 1].SetActive(false);   
                }
                //Stage1UI.UI_OFF = false;
                //Stage1UI.AlphaOn = true;
                Final = false;
                BackGround.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            }
        }
    }   


    void Position_Setting()//Function for Position Setting
    {
        for (int i = 0; i < 5; i++)
        {
            TempX = Random.Range(0, MaxNumberX);
            ButtonClick[i].PosX = Background.transform.position.x - 6 + ListX[TempX] * 2;       //For  X Coord
            if (ListX[TempX] == 5)
                ButtonClick[i].PosY = Background.transform.position.y - 2.3f + Random.Range(0, 2) * 1.5f;    //For Y Coord
            else
                ButtonClick[i].PosY = Background.transform.position.y - 2.3f + Random.Range(0, 2) * 1.5f;    //For Y Coord
            ListX.RemoveAt(TempX);
            MaxNumberX -= 1;
        }
        for (int i = 0; i < 5; i++)
        {
            ButtonClick[i].Active = true;
        }
    }

    void Check()
    {
        for (int i = 0; i < Clicked_Num; i++)
        {
            if (Array[i] == 5)
                return;
            if (i == Array[i])
            {
                if (Clicked_Num == 5)
                {
                    if(RandomTouchFinalButton.AnimationEnd)
                    {
                        TimerStart = false;
                        LimitTime = 7;
                        Success = true;
                        Debug.Log("Success");
                        SuccessAnimation.GetComponent<Animator>().SetTrigger("TriggerVictory");
                        MinigameSuccess.Success = true;
                        RandomButtonGameStart = false;
                        Final = true;
                        Button1.Clicked = false;
                        Button2.Clicked = false;
                        Button3.Clicked = false;
                        Button4.Clicked = false;
                        Button5.Clicked = false;
                    }
                }
            }
            else
            {
                TimerStart = false;
                LimitTime = 7;
                Fail = true;
                Debug.Log("Fail");
                RandomButtonGameStart = false;
                MinigameSuccess.Fail = true;
                Final = true;
                Button1.Clicked = false;
                Button2.Clicked = false;
                Button3.Clicked = false; 
                Button4.Clicked = false;
                Button5.Clicked = false;
            }
        }
    }
}