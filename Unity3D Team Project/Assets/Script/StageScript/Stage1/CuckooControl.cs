using UnityEngine;
using System.Collections;

public class CuckooControl : MonoBehaviour
{

    public bool masterKey;
    public bool GameOver;
    
    //For Global Trigger Boolean
    static public bool CuckooGameEnd;
    static public bool CharacterMove;
    static public bool EventOpen;

    //For Sound
    public AudioSource DoorOpenSound;

    //For Cuckoo 
    public GameObject Cuckoo;
    public Sprite BeforeCuckoo;
    public Sprite AfterCuckoo;

    //for Door
    public GameObject Door;
    public Sprite AfterDoor;
    public bool SawtoohEnd;

    //TraceTarget
    public GameObject FinalDoorObject;
    public GameObject Camera;
    public GameObject CharacterObject;

    //is Complete Mission?
    static public bool SawB;
    static public bool SawA;
    static public bool CuckooGame;                                                          //Cuckoo MiniGame End
    static public bool FinalStep;

    static public bool AllEnd;
    private bool FinalSecondStep;

    //For SpringGame
    public GameObject Spring;

    public float time;

    private bool Vibrate;
    void Start()
    {
        time = 1;
        SawtoohEnd = false;
        Vibrate = false;
        SawA = false;
        SawB = false;
        FinalStep = false;
        FinalSecondStep = false;
    }
    void Update()
    {
        if(masterKey)
        {
            SawA = true;
            SawB = true;
            masterKey = false;
        }
        if(GameOver)
        {
            CuckooGame = true;
            CuckooGameEnd = false;
            Spring.SetActive(false);
            MiniGameSpring.Success = false;
            GameOver = false;
        }

        if (MiniGameSpring.Success)
        {
            CuckooGame = true;
            CuckooGameEnd = false;
            Spring.SetActive(false);
            MiniGameSpring.Success = false;
        }

        if (SawA && SawB)                                                                      //if Mission A and B is Completed, Open Door and Create Cuckoo
        {
            //CharacterObject.transform.position = new Vector3(2, -0.975078f, 1);                 //Character Move First !!! 
            if (MainMenu.EffectValue)
                DoorOpenSound.Play();
            Door.GetComponent<SpriteRenderer>().sprite = AfterDoor;
            Cuckoo.GetComponent<SpriteRenderer>().sprite = BeforeCuckoo;
            SawtoohEnd = true;
            EventOpen = true;
            SawA = false;
            SawB = false;
        }
        if (CharacterMove)
        {
            CharacterObject.transform.position = new Vector3(2, -0.975078f, 1);                 //Character Move First !!! 
            CharacterMove = false;
        }

        if (CuckooGame)
        {
            HandScript.EquippedItem = Stage1UI.Item.Basic;
            CameraShake.CurrentPos = Camera.transform.position;
            Cuckoo.GetComponent<SpriteRenderer>().sprite = AfterCuckoo;
            Stage1UI.TraceCamera = false;                                                      //Stop Tracing //MainMenu
            CameraShake.StartPosition = true;
            Vibrate = true;
            VirtualJoyStick.JoystickEnable = false;
            StageView.ViewDeActivate = true;
            CharacterMoving.EventType = CharacterMoving.MessageType.Cuckoo_After;
            EventMessage_Time.TimeAttackActivate = true;
            Stage1UI.TimeAttackStart = true;
            CuckooGame = false;
        }
        if (Vibrate)
        {
            CameraShake.CameraShaking = true;
            ///////////////////////
            if (CameraShake.Count == 0)
            {
                Vibrate = false;
                CameraShake.CameraShaking = false;
            }
        }


        if (FinalStep && !Vibrate)
        {
            if (Camera.transform.position.x >= 4.29f && Camera.transform.position.y <= -34.7f)
            {
                if (FinalDoor.DoorEnd)
                {
                    FinalStep = false;
                    FinalSecondStep = true;
                }
                else
                {
                    CharacterMoving.EventType = CharacterMoving.MessageType.Cuckoo_After2nd;
                    //Camera.transform.position = new Vector3(4.4f, -34.8f, -10);
                    FinalDoor.StartDoorOpen = true;
                }
            }
            else
            {
                TraceTarget(Camera, FinalDoorObject);
            }
        }

        if (FinalSecondStep)
        {
            TraceTarget_After(Camera, CharacterObject);
            if (Camera.transform.position.x <= CharacterObject.transform.position.x + 0.15f && Camera.transform.position.y >= CharacterObject.transform.position.y - 0.15f)
            {
                Stage1UI.AlphaOn = true;
                Stage1UI.UI_OFF = false;
                Stage1UI.TraceCamera = true;
                EventMessage_Time.EventMessageExit = true;
                FinalSecondStep = false;
                TimeAttackScript.TimeAttackStart = true;
                VirtualJoyStick.JoystickEnable = true;
            }
        }
    }

    void TraceTarget(GameObject Camera, GameObject Target)
    {
        float newX = Mathf.Lerp(Camera.transform.position.x, Target.transform.position.x, 0.0054f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        float newY = Mathf.Lerp(Camera.transform.position.y, Target.transform.position.y, 0.02f);
        if (Camera.transform.position.x <= -4.14f)
            newX = -4.14f;
        if (Camera.transform.position.x >= 4.35f)
            newX = 4.35f;
        if (Camera.transform.position.y <= -34.8f)
            newY = -34.8f;
        Vector3 newPosition = new Vector3(newX, newY, -10);
        Camera.transform.position = newPosition;
    }

    void TraceTarget_After(GameObject Camera, GameObject Target)
    {
        float newX = Mathf.Lerp(Camera.transform.position.x, Target.transform.position.x, 0.015f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        float newY = Mathf.Lerp(Camera.transform.position.y, Target.transform.position.y, 0.02f);
        if (Camera.transform.position.x <= -4.14f)
            newX = -4.14f;
        Vector3 newPosition = new Vector3(newX, newY, -10);
        Camera.transform.position = newPosition;
    }
    void OnMouseDown()
    {
        if (!Stage1UI.ItemOpen && !Stage1UI.PauseOpen)
        {

            if (HandScript.EquippedItem == Stage1UI.Item.pliers)
            {
                if (SawtoohEnd)
                {
                    Spring.SetActive(true);
                    MiniGameSpring.Fail = false;
                    MiniGameSpring.Success = false;
                    MiniGameSpring.SpringGameStart = true;
                    Stage1UI.AlphaOn = false;
                    Stage1UI.UI_OFF = true;
                }
            }
            else
            {
                HandScript.isNotMatch = true;
                HandScript.Blink = true;
            }
        }
    }
}