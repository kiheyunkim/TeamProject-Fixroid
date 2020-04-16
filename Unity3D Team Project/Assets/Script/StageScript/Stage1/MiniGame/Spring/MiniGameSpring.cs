using UnityEngine;
using System.Collections;

public class MiniGameSpring : MonoBehaviour
{
    public AudioSource OneStepEndSound;

    static public bool SpringGameStart;
    static public bool Success;
    static public bool Fail;
    static public bool Ready;

    public GameObject Integral;
    public GameObject Target_Spring;
    public GameObject Background;
    public GameObject Cameras;

    //Spring Image
    public Sprite Spring_Step_First;
    public Sprite Spring_Step_First_Sel;
    public Sprite Spring_Step_Second;
    public Sprite Spring_Step_Second_Sel;
    public Sprite Spring_Step_Third;
    public Sprite Spring_Step_Third_Sel;
    public Sprite Spring_Step_Final;

    public GameObject Step1_Darg;
    public GameObject Square1;
    public GameObject Square2;
    public GameObject Square3;

    //First Control Boolean
    public bool Initialization;
    public bool CameraTrace;

    public bool GameReady;
    public bool FirstStep;
    public bool FirstStep_End;
    public bool SecondStep;
    public bool SecondStep_End;
    public bool ThirdStep;
    public bool ThirdStep_End;
    public bool FourthStep;



    //Small Game Control
    private bool TouchStart;
    private bool GoUp;
    private bool GoDown;
    private int Count;


    Touch touch;

    // Use this for initialization
    void Start ()
    {
        Initialization = true;
        Ready = false;
        GameReady = false;
        FirstStep = false;
        SecondStep = false;
        ThirdStep = false;
        FirstStep_End = false;
        SecondStep_End = false;
        ThirdStep_End = false;
        TouchStart = false;
        Count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (CameraTrace)
        {
            this.transform.position = Cameras.transform.position;
        }
        if (Fail)
        {
            SpringGameStart = false;
            MinigameSuccess.Fail = true;
            Square1.GetComponent<Animator>().SetTrigger("Fail");
            Square2.GetComponent<Animator>().SetTrigger("Fail");
            Square3.GetComponent<Animator>().SetTrigger("Fail");
            Square1.GetComponent<SpriteRenderer>().sprite = null;
            Square2.GetComponent<SpriteRenderer>().sprite = null;
            Square3.GetComponent<SpriteRenderer>().sprite = null;
            Initialization = true;
            Ready = false;
            GameReady = false;
            FirstStep = false;
            SecondStep = false;
            ThirdStep = false;
            FirstStep_End = false;
            SecondStep_End = false;
            ThirdStep_End = false;
            TouchStart = false;
            Count = 0;
            Integral.SetActive(false);
        }
        
        if(Success)
        {
            HandScript.EquippedItem = Stage1UI.Item.Basic;
            SpringGameStart = false;
            Initialization = true;
            GameReady = false;
            FirstStep = false;
            SecondStep = false;
            ThirdStep = false;
            FirstStep_End = false;
            SecondStep_End = false;
            ThirdStep_End = false;
            TouchStart = false;
            Count = 0;
            Integral.SetActive(false);
        }
        if (SpringGameStart)
        {

            if (Initialization)
            {
                Initialization = false;
                CameraTrace = true;
                Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_First;
                Square1.GetComponent<Animator>().SetTrigger("DragLine");
                SpringGameStart = false;
                GameReady = true;
            }

            if (GameReady)
            {
                Step1_Darg.GetComponent<Animator>().SetTrigger("DragStart");
                FirstStep = true;
                GameReady = false;
            }


            //////////////////////////////////// First Step
            if (FirstStep)
            {
                if (Ready)
                {
                    //Drag Motion
                    if (Input.touchCount > 0)
                    {
                        Debug.Log(Input.touches[0].position);
                        touch = Input.touches[0];
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                if (!TouchStart)
                                {
                                    if (touch.position.y > Screen.height * 0.7f)
                                    {
                                        TouchStart = true;
                                    }
                                    else
                                    {
                                        Fail = true;
                                    }
                                }
                                break;

                            case TouchPhase.Ended:
                                if (Count != 6 && FirstStep)
                                {
                                    Fail = true;
                                }
                                if (Count == 6)
                                {
                                    FirstStep_End = true;
                                }
                                if (Count > 6)
                                {
                                    Fail = true;
                                }
                                break;

                            case TouchPhase.Moved:
                                Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_First_Sel;
                                //Fail Control
                                if (touch.position.y > Screen.height * 1.0f && touch.position.x < Screen.width * 0.3783783783783784f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y > Screen.height * 1.0f && touch.position.x > Screen.width * 0.6486486486486486f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y < Screen.height * 0.6553398058252427f && touch.position.x < Screen.width * 0.3783783783783784f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y < Screen.height * 0.6553398058252427f && touch.position.x > Screen.width * 0.6486486486486486f)
                                {
                                    Fail = true;
                                }
                                else
                                {
                                    Fail = false;
                                }


                                //Success Control
                                if (touch.position.y > Screen.height * 0.72815533980582524271844660194175f)
                                {
                                    if (GoUp)
                                        Count++;
                                    GoDown = true;
                                    GoUp = false;
                                }
                                if (touch.position.y < Screen.height * 0.72815533980582524271844660194175f)
                                {
                                    if (GoDown)
                                        Count++;
                                    GoDown = false;
                                    GoUp = true;
                                }
                                break;
                        }
                    }

                    if (FirstStep_End)//For Next 2Step
                    {
                        if (MainMenu.EffectValue)
                            OneStepEndSound.Play();
                        Ready = false;
                        Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_Second;
                        Square1.GetComponent<Animator>().SetTrigger("DragSuccess");
                        FirstStep = false;
                        SecondStep = true;
                        FirstStep_End = false;
                        Fail = false;
                        Count = 0;
                        GoDown = false;
                        GoUp = false;
                        TouchStart = false;
                        Square2.GetComponent<Animator>().SetTrigger("DragLine");
                    }
                }
            }
                    //////////////////////////////////////////Second Step

            if (SecondStep)
            {
                if (Ready)
                {
                    //Drag Motion
                    if (Input.touchCount > 0)
                    {
                        Debug.Log(Input.touches[0].position);
                        touch = Input.touches[0];
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:

                                if (!TouchStart)
                                {
                                    if (touch.position.y > Screen.height * 0.4004854368932039f)
                                    {
                                        TouchStart = true;
                                    }
                                    else
                                    {
                                        Fail = true;
                                    }
                                }
                                break;
                            case TouchPhase.Ended:
                                if (Count != 6 && SecondStep)
                                {
                                    Fail = true;
                                }
                                if (Count == 6)
                                {
                                    SecondStep_End = true;
                                }
                                if (Count > 6)
                                {
                                    Fail = true;
                                }
                                break;
                            case TouchPhase.Moved:
                                Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_Second_Sel;
                                //Fail Control

                                if (touch.position.y > Screen.height * 0.4611650485436893f && touch.position.x < Screen.width * 0.3783783783783784f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y > Screen.height * 0.4611650485436893f && touch.position.x > Screen.width * 0.6486486486486486f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y < Screen.height * 0.3398058252427184f && touch.position.x < Screen.width * 0.3783783783783784f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y < Screen.height * 0.3398058252427184f && touch.position.x > Screen.width * 0.6486486486486486f)
                                {
                                    Fail = true;
                                }
                                else
                                {
                                    Fail = false;
                                }

                                //Success Control
                                if (touch.position.y > Screen.height * 0.4004854368932039f)
                                {
                                    if (GoUp)
                                        Count++;
                                    GoDown = true;
                                    GoUp = false;
                                }
                                if (touch.position.y < Screen.height * 0.4004854368932039f)
                                {
                                    if (GoDown)
                                        Count++;
                                    GoDown = false;
                                    GoUp = true;
                                }
                                break;
                        }
                    }


                    if (SecondStep_End)
                    {
                        if (MainMenu.EffectValue)
                            OneStepEndSound.Play();
                        Ready = false;
                        Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_Third;
                        Square2.GetComponent<Animator>().SetTrigger("DragSuccess");
                        SecondStep = false;
                        ThirdStep = true;
                        SecondStep_End = false;
                        Fail = false;
                        Count = 0;
                        GoDown = false;
                        GoUp = false;
                        TouchStart = false;
                        Square3.GetComponent<Animator>().SetTrigger("DragLine");
                    }
                }
            }

            if (ThirdStep)
            {
                if (Ready)
                {
                    //Drag Motion
                    if (Input.touchCount > 0)
                    {
                        Debug.Log(Input.touches[0].position);
                        touch = Input.touches[0];
                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                if (!TouchStart)
                                {
                                    if (touch.position.y > Screen.height * 0.0f)
                                    {
                                        TouchStart = true;
                                    }
                                    else
                                    {
                                        Fail = true;
                                    }
                                }
                                break;
                            case TouchPhase.Ended:
                                if (Count != 6 && ThirdStep)
                                {
                                    Fail = true;
                                }
                                break;
                            case TouchPhase.Moved:
                                Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_Third_Sel;
                                //Fail Control
                                if (touch.position.y > Screen.height * 0.0f && touch.position.x < Screen.width * 0.3783783783783784f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y > Screen.height * 0.0f && touch.position.x > Screen.width * 0.6486486486486486f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y < Screen.height * 0.1941747572815534f && touch.position.x < Screen.width * 0.3783783783783784f)
                                {
                                    Fail = true;
                                }
                                else
                                if (touch.position.y < Screen.height * 0.1941747572815534f && touch.position.x > Screen.width * 0.6486486486486486f)
                                {
                                    Fail = true;
                                }
                                else
                                {
                                    Fail = false;
                                }


                                //Success Control
                                if (touch.position.y > Screen.height * 0.0970873786407767f)
                                {
                                    if (GoUp)
                                        Count++;
                                    GoDown = true;
                                    GoUp = false;
                                }
                                if (touch.position.y < Screen.height * 0.0970873786407767f)
                                {
                                    if (GoDown)
                                        Count++;
                                    GoDown = false;
                                    GoUp = true;
                                }
                                break;
                        }
                        if (Count == 6)
                        {
                            ThirdStep_End = true;
                        }

                    }
                    if (ThirdStep_End)
                    {
                        if (MainMenu.EffectValue)
                            OneStepEndSound.Play();
                        Target_Spring.GetComponent<SpriteRenderer>().sprite = Spring_Step_Final;
                        Square3.GetComponent<Animator>().SetTrigger("DragSuccess");
                        FourthStep = true;
                        ThirdStep_End = false;
                        Fail = false;
                        Count = 0;
                        GoDown = false;
                        GoUp = false;
                        Ready = false;
                        TouchStart = false;
                    }

                }
            }
            if(FourthStep)
            {
                if (MainMenu.EffectValue)
                    OneStepEndSound.Play();
                Success = true;
            }
        }
    }
}
