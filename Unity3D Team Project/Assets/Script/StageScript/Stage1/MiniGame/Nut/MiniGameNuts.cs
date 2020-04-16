using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MiniGameNuts : MonoBehaviour
{
    public bool Final;
    public bool Already_End;

    //Global  Trigger Boolean
    public bool GameStart;//true :
    static public bool Success;
    static public bool Fail;
    public GameObject Character;

    //Texture
    public GameObject Background;
    public GameObject Camera;
    public GameObject Animation;
    public GameObject Nut;
    public Sprite Nut_Noraml;
    public Sprite Nut_Clicked;

    //is Mouse In Area?
    public bool InArea;

    //Animation Trig
    public bool AnimeStart;
    //Start Option
    static public bool GameReady;//true : Rotation, False : No Rotation

    //Start Rotation;
    public int CurrentStep;
    public bool rotate;
    public bool Rotation;
    public float Angle;
    public float minSwipeDistY;
    private Vector2 startPos;

    //Change Nut
    public GameObject Target_Nut;
    public Sprite Change_Nut;

    //For Sound
    public AudioSource RotationSound;
    public bool soundControl;

    void Start()
    {
        AnimeStart = true;
        Final = true;
        Already_End = false;
        GameReady = false;
        CurrentStep = 1;
        rotate = false;
        InArea = false;
        GameStart = false;
        soundControl = true;
    }
    void Update()
    {
        if(Already_End&&Final)
        {
            Character.GetComponent<Animator>().SetTrigger("TriggerVictory");
            HandScript.EquippedItem = Stage1UI.Item.Basic;
            Choo.StartChoo = true;
            Already_End = true;
            Final = false;
        }
        if(GameStart)
        {
            Success = false;
            Fail = false;
            Animation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Nut.transform.position = Camera.transform.position;
            Background.transform.position = Camera.transform.position;
            Nut.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            Background.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            if(AnimeStart)
            {
                Animation.GetComponent<Animator>().SetTrigger("NutStart");
                AnimeStart = false;
            }
        }
        else
        {
            Animation.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            Nut.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            Background.transform.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
        }
        if(CurrentStep>4)
        {
            if(CurrentStep==5)                              //Victory
            {
                Stage1UI.DeleteItem(Stage1UI.Item.Nut);
                MinigameSuccess.Success = true;
                Target_Nut.GetComponent<SpriteRenderer>().sprite = Change_Nut;
                Already_End = true;
                Nut.transform.rotation.Set(0, 0, 0, 0);
                Angle = 0;
                GameStart = false;
                GameReady = false;
                CurrentStep = 1;
                rotate = false;
                InArea = false;
            }
            if(CurrentStep==6)                              //Fail
            {
                MinigameSuccess.Fail=true;
                AnimeStart = true;
                Nut.transform.rotation.Set(0, 0, 0, 0);
                Angle = 0;
                GameStart = false;
                GameReady = false;
                CurrentStep = 1;
                rotate = false;
                InArea = false;
            }
        }

        if (CurrentStep < 5)
        {

            if (Rotation && !GameReady)
            {
                if(soundControl)
                {
                    if (!RotationSound.isPlaying)
                        RotationSound.Play();
                    soundControl = false;
                }
                Nut.GetComponent<SpriteRenderer>().sprite = Nut_Clicked;
                Angle = Time.deltaTime * 60f;
                if (rotate)
                    Nut.transform.Rotate(new Vector3(0, 0, Angle));
                if (CurrentStep == 1 && Nut.transform.rotation.z > 0.7079498f)
                {
                    Nut.transform.rotation.Set(0, 0, 90, 1);
                    CurrentStep += 1;
                    GameReady = true;
                    Rotation = false;
                    rotate = false;
                    soundControl = true;
                }
                else
                if (CurrentStep == 2 && Nut.transform.rotation.z > 0.99990f)
                {
                    Nut.transform.rotation.Set(0, 0, 180, 1);
                    CurrentStep += 1;
                    GameReady = true;
                    Rotation = false;
                    rotate = false;
                    soundControl = true;
                }
                else
                if (CurrentStep == 3 && Nut.transform.rotation.z < 0.7079498f)
                {
                    Nut.transform.rotation.Set(0, 0, 270, 1);
                    CurrentStep += 1;
                    GameReady = true;
                    Rotation = false;
                    rotate = false;
                    soundControl = true;
                }
                else
                if (CurrentStep == 4 && Nut.transform.rotation.z < 0.001f)
                {

                    Nut.transform.rotation.Set(0, 0, 0, 1);
                    CurrentStep += 1;
                    GameReady = true;
                    Rotation = false;
                    rotate = false;
                    soundControl = true;
                }

            }
            //#if UNITY_ANDROID
            if (GameStart && GameReady)
            {
                Nut.GetComponent<SpriteRenderer>().sprite = Nut_Noraml;
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.touches[0];
                    if (!Rotation)
                    {

                        switch (touch.phase)
                        {
                            case TouchPhase.Began:
                                startPos = touch.position;
                                break;

                            case TouchPhase.Moved:
                               

                                break;
                            case TouchPhase.Ended:
                                if (touch.position.x <= Screen.width * 0.30753246753246753246753246753247f)
                                {
                                    InArea = false;
                                    CurrentStep = 6;
                                }
                                else
                               if (touch.position.x >= Screen.width * 0.55948051948051948051948051948052f)
                                {
                                    InArea = false;
                                    CurrentStep = 6;
                                }
                                else
                                {
                                    InArea = true;
                                    Debug.Log("IN");
                                }
                                float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                                if (swipeDistVertical > minSwipeDistY)
                                {
                                    float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                                    if (swipeValue < 0)//down swipe
                                    {
                                        if (InArea)
                                        {
                                            Rotation = true;
                                            GameReady = false;
                                            swipeValue = 0;
                                            InArea = false;
                                            rotate = true;
                                        }
                                        else
                                        {
                                            CurrentStep = 6;
                                        }
                                        Debug.Log("Down");
                                    }
                                    else
                                    {
                                        CurrentStep = 6;
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
    void OnMouseUp()
    {
        if (!Stage1UI.ItemOpen && !Stage1UI.PauseOpen)
        {
            if (!Already_End)
            {
                if (Stage1UI.Equipped == Stage1UI.Item.Spaner)
                {
                    Stage1UI.AlphaOn = false;
                    Stage1UI.UI_OFF = true;
                    GameStart = true;
                }
                else
                {
                    HandScript.isNotMatch = true;
                    HandScript.Blink = true;
                }
            }
        }
    }
}
