using UnityEngine;
using System.Collections;

public class FinalScore : MonoBehaviour //Final Score Part And Movement
{
    //Global Trigger Variable
    static public bool FinalScoreVictory;
    static public bool FinalScoreLose;
    static public float Result;
    public float Result_Colone;

    //Variable For Timer Number
    private bool TimeStart;
    private string TimeString;
    private int Minute;
    private int Second;
    private int Milisecond;

    //Control Boolean
    private bool MovementEnd;

    //buttons
    public GUIStyle GoToMainMenu;
    public GUIStyle Restart;

    //static string Timer;
    public GUIStyle FontStyle;
    public float Result_Rise;
    // Use this for initialization

    //For Board
    public Texture2D ScoreBoard;
    public Texture2D ScoreBoard_Victory;
    public Texture2D ScoreBoard_Lose;

    //for Background
    private Texture ScoreAlphaBG;
    public Texture ScoreAlphaBG_Victory;
    public Texture ScorAlphaBG_Lose;

    //Movement Part
    public Texture ButtonAlpha;
    public Texture ButtonAlpha_After;   

    //For Movement (Buttons)
    public float MovementYUP;
    public float MovementYDOWN;
    public float Acceleration_UP;
    public float Acceleration_DOWN;
    private float SpeedUP;
    private float SpeedDOWN;
    private float Standard;
    private bool Up;
    private bool Down;
    private float UpDownCount;
    private bool ScoreMenu;


    //boolean Control
    public bool TimerEnd;
    private bool TimeDisplay;

    //if Acheievement Activate
    public Texture Acheive;
    public bool Success;
    public float time;

    //For Sound
    public AudioSource FinalScoreButtonSound;
    public AudioSource FinalScoreRaise;
    public AudioSource SuccessSound;
    public AudioSource FailSound;
    void Start()
    {
        Success = false;
        time = 2.0f;

        FinalScoreLose = false;
        FinalScoreVictory = false;

        Standard = Screen.height * 0.347222222222f + Screen.height * SpeedUP + Screen.height * SpeedDOWN;
        FontStyle.fontSize = Screen.height / 24;
        Result_Rise = 0f;
        TimeString = "00:00:00";                                                                                                            //First String
                                                                                                                                            //Result
        
        //Adjust Value
        Acceleration_UP = 0;
        Acceleration_DOWN = 0;
        SpeedDOWN = 0;
        SpeedUP = 0;
        
        //Value for Count of Bounce
        UpDownCount = 1;           
                                                                                                                 
        //boolean for justice
        TimeDisplay = false;
        TimerEnd = false;

    }
    void Update()
    {
        Result_Colone = Result;
        if (FinalScoreLose)                                                                                                                  //If Lose, No Timer Activate
        {
            //if (MainMenu.EffectValue)
            //    FailSound.Play();
            Stage1UI.BGMStop = true;
            ScoreBoard = ScoreBoard_Lose;
            ScoreAlphaBG = ScorAlphaBG_Lose;
            ScoreMenu = true;
            TimerEnd = true;    
            Up = true;
            Down = false;
            FinalScoreLose = false;
        }

        if (FinalScoreVictory)                                                                                                               //If Victory, First Timer Activate
        {
            //if (MainMenu.EffectValue)
            //    FailSound.Play();
            Stage1UI.BGMStop = true;
            ScoreBoard = ScoreBoard_Victory;
            ScoreAlphaBG = ScoreAlphaBG_Victory;
            TimeStart = true;
            ScoreMenu = true;
            if (!TimeDisplay)
            {
                Result_Rise = 0;
                TimeDisplay = true;
            }
            FinalScoreVictory = false;

            //For Victory!! And Stage Adjustment
            MainMenu.StageNoEnd[0] = false;
            MainMenu.StageEnd[0] = true;
            if (MainMenu.EffectValue)
                FinalScoreRaise.Play();
        }

        if (TimeStart)
        {
            if(MainMenu.Stage1Result==0)
            {
                MainMenu.Stage1Result = 50.0f - Result;
            }
            else
            {
                if ( MainMenu.Stage1Result > 50.0f - Result)
                {
                    MainMenu.Stage1Result = 50.0f - Result;//Excellent Record
                }
            }
            if (!Acheievement.isGet[0])//if Time below 1Seconds, Achieve 1 is activated
            {
                Acheievement.isGet[0] = true;
                Success = true;
            }

            if (Result <= 1 && Result > 0)
            {
                if (!Acheievement.isGet[1])//if Time below 1Seconds, Achieve 1 is activated
                {
                    Acheievement.isGet[1] = true;
                    Success = true;
                }

            }
            Result_Rise += 0.14f;

            

            if ((int)Result_Rise > 60)
            {
                Minute = (int)Result_Rise / 60 - (int)Result_Rise % 60;
            }
            else
            {
                Minute = (int)Result_Rise / 60;
            }
            Second = (int)Result_Rise / 1;
            Milisecond = (int)((Result_Rise * 100) % 100f);
            TimeString = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2");

            if (Result_Rise > Result)
            {
                if (!FinalScoreRaise.isPlaying)
                    FinalScoreRaise.Stop();
                Minute = (int)Result / 60;
                Second = (int)Result / 1;
                Milisecond = (int)((Result * 100) % 100f);
                TimeString = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2");
                TimeStart = false;
                TimerEnd = true;
                Up = true;
                Down = false;

            }
            else
            {
                //Score Raise Sound
                if (!FinalScoreRaise.isPlaying)
                    FinalScoreRaise.Play();
            }
        }
        if (TimerEnd)
        {
            Standard = Screen.height * 0.347222222222f + Screen.height * SpeedUP + Screen.height * SpeedDOWN;
            if (UpDownCount == 3)
            {
                Up = false;
                Down = false;
                TimerEnd = false;
                MovementEnd = true;
                ButtonAlpha = ButtonAlpha_After;
                Standard = Screen.height * (0.62638888888888888888888888888888889f);
            }

            if (Up)//Screen Down
            {
                MovementYUP += 0.02f;
                Acceleration_UP += 0.001f;
                SpeedUP = MovementYUP + Acceleration_UP;


                if (SpeedUP + SpeedDOWN > 0.2691666666666666666666667f)
                {
                    Standard = Screen.height * (0.62638888888888888888888888888888889f);
                    UpDownCount += 1;
                    Up = false;
                    Down = true;
                }
            }
            if (Down)//Screen Up
            {
                MovementYDOWN -= 0.002f;
                Acceleration_DOWN -= 0.0005f;
                SpeedDOWN = MovementYDOWN + Acceleration_DOWN;


                if (SpeedUP + SpeedDOWN < 0.2791666666666666666666667f * (0.87f + (UpDownCount / 100)))
                {
                    Up = true;
                    Down = false;
                }
            }

        }
        
    }
    void OnGUI()
    {
        if (ScoreMenu)
        {
            Stage1UI.AlphaOn = false;
            GUI.depth = 0;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ScoreAlphaBG);

            //Movement Part
            GUI.DrawTexture(new Rect(Screen.width * 0.33203125f, Standard, Screen.width * 0.3359375f, Screen.height * 0.21805555555556f), ButtonAlpha);

            //Score And Timer
            GUI.DrawTexture(new Rect(Screen.width * 0.2599609375f, Screen.height * 0.15555555555556f, Screen.width * 0.480078125f, Screen.height * 0.47083333333333333333f), ScoreBoard);
            if (TimeDisplay)
            {
                GUI.TextArea(new Rect(Screen.width * 0.35703125f, Screen.height * 0.4402777777778f, Screen.width * 0.2859375f, Screen.height * 0.08819444444444444f), TimeString, FontStyle);
                FontStyle.fontSize = (int)(Screen.height * 0.165f * 0.75f);
                //ScoreText.enabled = true;
                //ScoreText.text = TimeString;
            }
        }

        if (MovementEnd)//When Time Calculation End, 
        {
            if (GUI.Button(new Rect(Screen.width * 0.33359375f, Screen.height * 0.6625f, Screen.width * 0.10234375f, Screen.height * 0.181944444444444444444f), "", Restart))
            {
                if (MainMenu.EffectValue)
                    FinalScoreButtonSound.Play();
                Loading.NextSceneNumber = 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            }
            if (GUI.Button(new Rect(Screen.width * 0.565625f, Screen.height * 0.6625f, Screen.width * 0.10234375f, Screen.height * 0.18194444444444444444444f), "", GoToMainMenu))
            {
                if (MainMenu.EffectValue)
                    FinalScoreButtonSound.Play();
                Loading.NextSceneNumber = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            }
        }

        if(Success)
        {
            if (time > 0)
            {
                time -= Time.deltaTime;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Acheive);
            }
            else
            {
                Success = false;
            }
        }
    }

}
