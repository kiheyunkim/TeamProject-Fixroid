//for Only for Stage UI
using UnityEngine;
using System.Collections;

public class Stage1UI : MonoBehaviour
{
    public struct ItemArray                                                                                                 //struct for Item Gaining
    {
        public Item ItemName;
        public bool empty;
        public bool use;
        public Texture2D ItemNormal;
        public Texture2D ItemActive;
    }

    //For Camera Start
    private bool StartCamera = false;                                                                                       //Camera Down Start
    private bool isStart = false;                                                                                           //Camera Trace Start
    static public bool TraceCamera = false;
    private bool first;

    //for Trace
    public GameObject Target_Object;                                                                                        //Target Object
    public GameObject Camera_Object;                                                                                        //Camera Object

    //for Popup
    static public bool ItemOpen;                                                                                            //true : Oepn, false : Close
    static public bool PauseOpen;                                                                                                 //true : Oepn, false : Close
    private bool Menu;                                                                                                      //true : Sound, false : System; 
    
    //Game UI
    static public bool AlphaOn;                                                                                             //true : ON / false : OFF
    public Texture2D Alpha;                                                                                                   //Game Background Alpha
    public GUIStyle PauseButton;                                                                                            //Pause Menu BackGround
    public GUIStyle HandGUI;                                                                                                //Pause Menu BackGround
    public Sprite Jump_Right;                                                                                               //Sprite For Jump Button(Right)
    public Sprite Jump_Right_Sel;                                                                                           //Sprite For Jump Button Select(Right)
    public Sprite Jump_Left;                                                                                                //Sprite For Jump Button(Left)
    public Sprite Jump_Left_Sel;                                                                                           //Sprite For Jump Button Select(Left)
    private UnityEngine.UI.SpriteState Left_Press;
    private UnityEngine.UI.SpriteState Right_Press;
    public Texture2D PauseButton_Sel;
    public Texture2D PauseButton_Left;
    public Texture2D PauseButton_Normal;
    public Texture2D PauseButton_Normal_Left;
                                                                                                                            //Pause Button Active
    public GameObject IntegralUI;
    public UnityEngine.UI.Button JumpButton;                                                                                //JumpButton
    public GameObject JoyStick;
    public GameObject ItemButton;
    //Integral Stage1 UI

    //PopUp UI
    public GUIStyle ItemPopUp_Left;                                                                                         //Item Popup BackGround Left Option
    public GUIStyle ItemPopUp_Right;                                                                                        //Item Popup BackGround RIght Option
    public GUIStyle PausePopUp;                                                                                             //Popup Button PuasePopup
    public Texture2D PausePopUp_SYS;                                                                                        //Pause Menu Button - System
    public Texture2D PausePopUp_SOUND;                                                                                      //Pause Menu Button - Sound

    //for Sound boolean
    private bool BgmSound;                                                                                                  //true : BGM ON / false BGM OFF
    private bool EffectSound;                                                                                               //true : Effect ON / false Effect OFF

    //For Puase Popup Integral
    public GUIStyle XButton;                                                                                                //Exit Button
    public GUIStyle SoundButton;                                                                                            //Sound Button
    public GUIStyle SystemButton;                                                                                           //System Button

    public Texture2D SoundButton_ON;                                                                                        //SoundButtonOn
    public Texture2D SoundButton_OFF;                                                                                       //SoundButtonOff
    public Texture2D SystemButton_ON;                                                                                       //SystemButtonOn
    public Texture2D SystemButton_OFF;                                                                                      //SystemButtonOff

    //for Sound menu
    public GUIStyle BGM_Sound;                                                                                              //BGM Sound Button
    public GUIStyle Effect_Sound;                                                                                           //Effect Sound Button

    public Texture2D SoundOn;                                                                                               //Sound On normal
    public Texture2D SoundOn_Sel;                                                                                           //Sound On Active
    public Texture2D SoundOff;                                                                                              //Sound Off normal
    public Texture2D SoundOff_Sel;                                                                                          //Sound off Active

    //For System Menu
    public GUIStyle ButtonRestart;                                                                                          //RestartButton
    public GUIStyle ButtonMainMenu;                                                                                         //ButtonGoMain

    //Item PopUp
    public enum Item { Empty,Basic, OilSpray,Oil_Full, Spaner, pliers, ChainSaw1, ChainSaw2, Nut};                          //Enum for Item
    static public Item Equipped;                                                                                            //Current Item Equipped
    static public ItemArray[] ItemStruct = new ItemArray[9];                                                                //Item Struct
    public GUIStyle[] ItemButtonGUI = new GUIStyle[9];                                                                      //Item Button
    public Texture2D[] _Item = new Texture2D[9];                                                                            //Item Image normal
    public Texture2D[] _Item_Sel = new Texture2D[9];                                                                        //Item Image Active
    static public Texture2D[] ItemTexture = new Texture2D[9];                                                               //Item Image normal
    static public Texture2D[] ItemTexture_Sel = new Texture2D[9];                                                           //Item Image Active
    static public int CurrentPos;                                                                                           //Next Item Position

    public Texture2D BasicWindow;
    public Texture2D BasicWindowSel;
    static public Texture2D _BasicWindow;
    static public Texture2D _BasicWindowSel;

    //for Lighting
    public GameObject Light_Effect;

    //for Animation 
    public GameObject StageAnimation;
    static public bool Destoryed_anime;


    //Global
    static public bool UI_OFF;
    static public bool Destory_Character;

    //For Oil Change
    static public bool OilComplete;

    //For Animation SOund;
    static public bool IntegralSoundControl;
    static public bool FirstAnimationSound;
    static public bool SecondAnimationSound;
    public AudioSource FirstAnimation;
    public AudioSource SecondAnimation;

    //For OilSound;
    static public bool OilDrop;
    public AudioSource OilDropSound;

    //BGM Change & Stop
    public static bool TimeAttackStart;
    public AudioClip TimeAttack;
    static public bool BGMStop;

    //For Sound Source
    public AudioSource Stage1BGM;
    public AudioSource Stage1AppearSound;
    public AudioSource ItemPageButtonClick;
    public AudioSource PauseButtonClick;
    public AudioSource JumpButtonClick;

    public AudioSource InGameOption_SoundSystem;
    public AudioSource InGameOption_SoundButton;
    public AudioSource InGameOption_SystemButton;
    public AudioSource InGameOption_XButton;

    //ItemSort
    static public bool NeedSort;


    void Start()
    {
        
        AlphaOn = false;
        UI_OFF = true;

        NeedSort = false;
        OilComplete = false;
        Destory_Character = false;
        first = true;
        Destoryed_anime = false;
        Screen.SetResolution(1280, 720, true);                                                                              //Set Resolution
        CurrentPos = -1;                                                                                                     //Item Allocation Position
        ItemOpen = false;                                                                                                   //Item Page Open?
        PauseOpen = false;                                                                                                  //Pause Page Open?
        Menu = true;                                                                                                        //default is system menu
        //Equipped = Item.Basic;                                                                                           //First Equipped(Test)

        //Load Sound Setting 
        BgmSound = MainMenu.BGMValue;                                                                                       //Load BGM value      
        EffectSound = MainMenu.EffectValue;                                                                                 //Load Effectsound value

        for (int i = 0; i < _Item.Length; i++)                                                                              //Initialized ItemStruct
        {
            ItemTexture[i] = _Item[i];
            ItemTexture_Sel[i] = _Item_Sel[i];
        }

        for (int i = 0; i < ItemStruct.Length; i++)                                                                         //Initialize ItemSturct
        {
            ItemStruct[i].empty = true;
            ItemStruct[i].use = false;
            ItemStruct[i].ItemName = Item.Basic;
        }

        Left_Press.pressedSprite = Jump_Left_Sel;
        Right_Press.pressedSprite = Jump_Right_Sel;

        //For UI Moving(Right And Left)
        if (MainMenu.LeftOrRight)                            //Left -> JoyStick Left
        {
            //for Jump Button
            JumpButton.GetComponent<UnityEngine.UI.Image>().sprite = Jump_Left;
            JumpButton.GetComponent<UnityEngine.UI.Button>().spriteState = Left_Press;
            JumpButton.transform.position = new Vector3(Screen.width * 0.8396875f, Screen.height * 0.13805555555555555555555555555556f, 0);
            //for JoyStick
            JoyStick.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.114140625f, Screen.height * 0.20416666666666666666666666666667f, 0);
            JoyStick.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            //for ItemButton
            ItemButton.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.93625f, Screen.height * 0.33597222222222222222222222222222f, 0);
            ItemButton.GetComponent<RectTransform>().localScale = new Vector3(-1, 1, 1);
            //For Pause Button
            PauseButton.active.background = PauseButton_Left;
            PauseButton.normal.background = PauseButton_Normal_Left;
        }
        else                                                //Right -> JoyStick RIght            
        {
            //for Jump Button
            JumpButton.GetComponent<UnityEngine.UI.Image>().sprite = Jump_Right;
            JumpButton.GetComponent<UnityEngine.UI.Button>().spriteState = Right_Press;
            JumpButton.transform.position = new Vector3(Screen.width * 0.159765625f, Screen.height * 0.13805555555555555555555555555556f, 0);
            //for JoyStick
            JoyStick.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.8859375f, Screen.height * 0.20416666666666666666666666666667f, 0);
            JoyStick.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
            //for ItemButton
            ItemButton.GetComponent<RectTransform>().position = new Vector3(Screen.width * 0.063359375f, Screen.height * 0.33597222222222222222222222222222f, 0);
            ItemButton.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

            //For Pause Button
            PauseButton.active.background = PauseButton_Sel;
            PauseButton.normal.background = PauseButton_Normal;
        }
        if(MainMenu.BGMValue)
            Stage1BGM.Play();
    }

    void Update()
    {
        //Final Open BGM Stop
        if(BGMStop)
        {
            Stage1BGM.Stop();
            BGMStop = false;
        }

        //TimeAttackBGM Chnage
        if(TimeAttackStart)
        {
            Stage1BGM.clip = TimeAttack;
            if(MainMenu.BGMValue)
                Stage1BGM.Play();
            TimeAttackStart = false;
        }

        //In Oil Area, Oil Drop Sound Play
        if (OilDrop)
            if(CharacterMoving.OilSoundArea)
                OilDropSound.Play();

        //First Animation Sound Control
        if(IntegralSoundControl)
        {
            if(FirstAnimationSound)
            {
                FirstAnimation.Play();
                FirstAnimationSound = false;
            }

            if(SecondAnimationSound)
            {
                SecondAnimation.Play();
                SecondAnimationSound = false;
                IntegralSoundControl = false;
            }
        }

        
        //If You Get Oil, Changing Image
        if(OilComplete)
        {
            for (int i = 0; i < 9; i++)
            {
                if (ItemStruct[i].ItemName == Item.OilSpray)
                {
                    ItemStruct[i].ItemName = Item.Oil_Full;
                    ItemStruct[i].ItemNormal = ItemTexture[2];
                    ItemStruct[i].ItemActive = ItemTexture_Sel[2];
                    OilComplete = false;
                    return;
                }
            }
        }
        
        //First Animation Tracing
        if(!Destoryed_anime)
            StageAnimation.transform.position = transform.position;
        
        //UI Off or ON
        if (UI_OFF)
            IntegralUI.SetActive(false);
        else
            IntegralUI.SetActive(true);

        //Start Camera
        if (gameObject.transform.position.y > -32.3f && !isStart && !StartCamera&&first)           //Camera Down//
        {
            gameObject.transform.Translate(new Vector3(0.0f, -Time.deltaTime * 5.0f, 0.0f));
        }

        if (gameObject.transform.position.y < -32.3f && !StartCamera)                       //Camera Down Movement End
        {
            float Size = Mathf.Lerp(gameObject.GetComponent<Camera>().orthographicSize, 3, 0.04f);
            gameObject.GetComponent<Camera>().orthographicSize = Size;
            TraceTarget_Start(Camera_Object, Target_Object);

            if (gameObject.GetComponent<Camera>().orthographicSize < 3.01f)
                StartCamera = true;
        }

        //Resize And place Tracing Position
        if (StartCamera&&first)
        {
            gameObject.GetComponent<Camera>().orthographicSize = 3;
            if (Camera_Object.transform.position.x <= -4.14f && Camera_Object.transform.position.y <= -34.79f)
            {
                isStart = true;
                StartCamera = false;
            }
            if (Camera_Object.transform.position.x >= 4.35f && Camera_Object.transform.position.y <= -34.79f)
            {
                isStart = true;
                StartCamera = false;
            }
            if ((int)Camera_Object.transform.position.x == (int)Target_Object.transform.position.x && (int)Camera_Object.transform.position.y == (int)Target_Object.transform.position.y)
            {
                isStart = true;
                StartCamera = false;
            }
        }

        //Default Start  
        if (isStart&&first)
        {
            AlphaOn = true;
            UI_OFF = false;
            StageView.ViewActivate = true;
            first = false;
            TraceCamera = true;    
        }      

        if (TraceCamera&&!first)
        {
            TraceTarget(Target_Object, Camera_Object, Target_Object.transform.position.x, Target_Object.transform.position.y);//Camera Alway trace Character                                                              
        }

        //////////////////////////////////////////////////////////////////////For Sound//////////////////////////////////////////////////////////////////////////////////
        if (BgmSound)
        {
            BGM_Sound.normal.background = SoundOn;
            BGM_Sound.active.background = SoundOn_Sel;
        }
        else
        {
            BGM_Sound.normal.background = SoundOff;
            BGM_Sound.active.background = SoundOff_Sel;
        }

        if (EffectSound)
        {
            Effect_Sound.normal.background = SoundOn;
            Effect_Sound.active.background = SoundOn_Sel;
        }
        else
        {
            Effect_Sound.normal.background = SoundOff;
            Effect_Sound.active.background = SoundOff_Sel;
        }

        ////////////////////////////////////////////////////////////For Pause PopUp///////////////////////////////////////////////////////////////////////////////////////////
        if (Menu)
        {
            //Integral button
            SoundButton.normal.background = SoundButton_ON;
            SystemButton.normal.background = SystemButton_OFF;
            //Menu BackGround
            PausePopUp.normal.background = PausePopUp_SOUND;

        }
        else
        {
            //Integral button
            SoundButton.normal.background = SoundButton_OFF;
            SystemButton.normal.background = SystemButton_ON;
            //Menu BackGround
            PausePopUp.normal.background = PausePopUp_SYS;
        }


        //Transfer Sound Setting
        MainMenu.BGMValue = BgmSound;
        MainMenu.EffectValue = EffectSound;

        //if Item or Pause menu Open deActivate PauseButtonClick
        if (!PauseOpen && !ItemOpen)
        {
            if (!MainMenu.LeftOrRight)
                PauseButton.active.background = PauseButton_Sel;
            else
                PauseButton.active.background = PauseButton_Left;

        }
        else 
        {
            PauseButton.active.background = null;
        }
        if(!Destory_Character)
            Light_Effect.transform.position = Target_Object.transform.position;

        for (int i = 0; i < ItemStruct.Length; i++)                                                    //Item Allocation
        {
            if (!ItemStruct[i].empty)
            {
                ItemButtonGUI[i].normal.background = ItemStruct[i].ItemNormal;
                ItemButtonGUI[i].active.background = ItemStruct[i].ItemActive;
            }
        }

        if(NeedSort)
        {
            for (int i = 0; i < ItemStruct.Length - 1; i++)                                                //sorting
            {
                if (ItemStruct[i].empty && !ItemStruct[i + 1].empty)
                {
                    ItemStruct[i].ItemName = ItemStruct[i + 1].ItemName;
                    ItemStruct[i].ItemActive = ItemStruct[i + 1].ItemActive;
                    ItemStruct[i].ItemNormal = ItemStruct[i + 1].ItemNormal;
                    ItemStruct[i].empty = ItemStruct[i + 1].empty;
                    ItemStruct[i].use = ItemStruct[i + 1].use;
                    ItemStruct[i + 1].empty = true;
                }
                else
                if (ItemStruct[i].empty && ItemStruct[i + 1].empty)
                {
                    ItemButtonGUI[i].normal.background = BasicWindow;
                    ItemButtonGUI[i].active.background = BasicWindowSel;
                    ItemStruct[i].empty = true;
                    ItemStruct[i].use = false;
                }
            }
            NeedSort = false;
        }
    }

    void OnGUI()
    {
        GUI.depth = 3;
        if (MainMenu.LeftOrRight)
        {
            if (GUI.Button(new Rect(Screen.width * 0.015625f, Screen.height * 0.02777777777777778f, Screen.width * 0.0859375f, Screen.height * 0.15277777777777777778f), "", PauseButton))//Pause Button
            {
                if (!PauseOpen && !ItemOpen)
                {
                    if (MainMenu.EffectValue)
                        PauseButtonClick.Play();
                    PauseOpen = true;
                }
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width * 0.8984375f, Screen.height * 0.027777777777777778f, Screen.width * 0.0859375f, Screen.height * 0.15277777777777777778f), "", PauseButton))//Pause Button
            {
                if (!PauseOpen && !ItemOpen)
                {
                    if (MainMenu.EffectValue)
                        PauseButtonClick.Play();
                    PauseOpen = true;
                }
            }
        }

        if (AlphaOn)
        {
            //Left Right Option
            if (MainMenu.LeftOrRight)                        //Left -> JoyStick Left
            {
                GUI.DrawTexture(new Rect(Screen.width, 0, -Screen.width, Screen.height), Alpha);

                if (ItemOpen)                               //Item Window
                {
                    GUI.Window(0, new Rect(Screen.width * 0.5953125f, Screen.height * 0.052083f, Screen.width * 0.3375f, Screen.height * 0.6f), ItemPagePopUp_Left, " ", ItemPopUp_Left/*,PopupBack*/);
                    VirtualJoyStick.JoystickEnable = false;
                    JumpButton.enabled = false;
                }
                else
                {
                    VirtualJoyStick.JoystickEnable = true;
                    JumpButton.enabled = true;
                }
            }
            else                                             //Right -> JoyStick Right
            {
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Alpha);



                if (ItemOpen)                               //Item Window
                {
                    GUI.Window(0, new Rect(Screen.width * 0.0671875f, Screen.height * 0.052083f, Screen.width * 0.3375f, Screen.height * 0.6f), ItemPagePopUp_Right, " ", ItemPopUp_Right/*,PopupBack*/);
                    VirtualJoyStick.JoystickEnable = false;
                    JumpButton.enabled = false;
                }
                else
                {
                    VirtualJoyStick.JoystickEnable = true;
                    JumpButton.enabled = true;
                }
            }
        }
        if (PauseOpen)
        {
            GUI.Window(0, new Rect(0, 0, Screen.width, Screen.height), PauseMenu, " ", PausePopUp);
            VirtualJoyStick.JoystickEnable = false;
            JumpButton.enabled = false;
            //if Pause Menu Open, All stops
            Time.timeScale = 0;
            if (BgmSound)
                Stage1BGM.Pause();
        }
        else
        {
            if (BgmSound)
                Stage1BGM.UnPause();
            //if Pause Menu doesn't Open, All parts move
            Time.timeScale = 1;
        }
    }

   
    void ItemPagePopUp_Right(int windowsID)                                     //ItemWindow 
    {
        float PopupWidth = Screen.width * 0.3375f;
        float PopupHeight = Screen.height * 0.6f;
        //////////////////////////////////////////////////////////////Popup First Row
        if (GUI.Button(new Rect(PopupWidth * 0.0566448801742919f, PopupHeight * 0.1154684095860566f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[0]))
        {
            if (!ItemStruct[0].empty)
            {
                if (ItemStruct[0].use)
                    HandScript.EquippedItem = ItemStruct[0].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.3398692810457516f, PopupHeight * 0.1154684095860566f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[1]))
        {
            if (!ItemStruct[1].empty)
            {
                if (ItemStruct[1].use)
                    HandScript.EquippedItem = ItemStruct[1].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.6230936819172113f, PopupHeight * 0.1154684095860566f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[2]))
        {
            if (!ItemStruct[2].empty)
            {
                if (ItemStruct[2].use)
                    HandScript.EquippedItem = ItemStruct[2].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        //////////////////////////////////////////////////////////////Popup Second Row
        if (GUI.Button(new Rect(PopupWidth * 0.0566448801742919f, PopupHeight * 0.3986928104575163f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[3]))
        {
            if (!ItemStruct[3].empty)
            {
                if (ItemStruct[3].use)
                    HandScript.EquippedItem = ItemStruct[3].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.3398692810457516f, PopupHeight * 0.3986928104575163f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[4]))
        {
            if (!ItemStruct[4].empty)
            {
                if (ItemStruct[4].use)
                    HandScript.EquippedItem = ItemStruct[4].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.6230936819172113f, PopupHeight * 0.3986928104575163f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[5]))
        {
            if (!ItemStruct[5].empty)
            {
                if (ItemStruct[5].use)
                    HandScript.EquippedItem = ItemStruct[5].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        //////////////////////////////////////////////////////////////Popup Third Row
        if (GUI.Button(new Rect(PopupWidth * 0.0566448801742919f, PopupHeight * 0.681917211328976f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[6]))
        {
            if (!ItemStruct[6].empty)
            {
                if (ItemStruct[6].use)
                    HandScript.EquippedItem = ItemStruct[6].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.3398692810457516f, PopupHeight * 0.681917211328976f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[7]))
        {
            if (!ItemStruct[7].empty)
            {
                if (ItemStruct[7].use)
                    HandScript.EquippedItem = ItemStruct[7].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.6230936819172113f, PopupHeight * 0.681917211328976f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[8]))
        {
            if (!ItemStruct[8].empty)
            {
                if (ItemStruct[8].use)
                    HandScript.EquippedItem = ItemStruct[8].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
    }
    void ItemPagePopUp_Left(int windowsID)                                     //ItemWindow 
    {
        float PopupWidth = Screen.width * 0.3375f;
        float PopupHeight = Screen.height * 0.6f;
        //////////////////////////////////////////////////////////////Popup First Row

        if (GUI.Button(new Rect(PopupWidth * 0.1154684095860566f, PopupHeight * 0.1154684095860566f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[0]))
        {
            if (!ItemStruct[0].empty)
            {
                if (ItemStruct[0].use)
                    HandScript.EquippedItem = ItemStruct[0].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.3986928104575163f, PopupHeight * 0.1154684095860566f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[1]))
        {
            if (!ItemStruct[1].empty)
            {
                if (ItemStruct[1].use)
                    HandScript.EquippedItem = ItemStruct[1].ItemName;
            }
            ItemOpen = false;
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
        }
        if (GUI.Button(new Rect(PopupWidth * 0.681917211328976f, PopupHeight * 0.1154684095860566f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[2]))
        {
            if (!ItemStruct[2].empty)
            {
                if (ItemStruct[2].use)
                    HandScript.EquippedItem = ItemStruct[2].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
            //HandScript.EquippedItem = Item.Spaner;
        }   
        //////////////////////////////////////////////////////////////Popup Second Row
        if (GUI.Button(new Rect(PopupWidth * 0.1154684095860566f, PopupHeight * 0.3986928104575163f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[3]))
        {
            if (!ItemStruct[3].empty)
            {
                if (ItemStruct[3].use)
                    HandScript.EquippedItem = ItemStruct[3].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.3986928104575163f, PopupHeight * 0.3986928104575163f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[4]))
        {
            if (!ItemStruct[4].empty)
            {
                if (ItemStruct[4].use)
                    HandScript.EquippedItem = ItemStruct[4].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.681917211328976f, PopupHeight * 0.3986928104575163f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[5]))
        {
            if (!ItemStruct[5].empty)
            {
                if (ItemStruct[5].use)
                    HandScript.EquippedItem = ItemStruct[5].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        //////////////////////////////////////////////////////////////Popup Third Row
        if (GUI.Button(new Rect(PopupWidth * 0.1154684095860566f, PopupHeight * 0.681917211328976f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[6]))//7번
        {
            if (!ItemStruct[6].empty)
            {
                if (ItemStruct[6].use)
                    HandScript.EquippedItem = ItemStruct[6].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.3986928104575163f, PopupHeight * 0.681917211328976f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[7]))//8번
        {
            if (!ItemStruct[7].empty)
            {
                if (ItemStruct[7].use)
                    HandScript.EquippedItem = ItemStruct[7].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
        if (GUI.Button(new Rect(PopupWidth * 0.681917211328976f, PopupHeight * 0.681917211328976f, PopupWidth * 0.261437908496732f, PopupHeight * 0.261437908496732f), "", ItemButtonGUI[8]))//9번
        {
            if (!ItemStruct[8].empty)
            {
                if (ItemStruct[8].use)
                    HandScript.EquippedItem = ItemStruct[8].ItemName;
            }
            if (MainMenu.EffectValue)
                ItemPageButtonClick.Play();
            ItemOpen = false;
        }
    }
    void PauseMenu(int windowID)  //function for Pause Menu
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            PauseOpen = false;
            VirtualJoyStick.JoystickEnable = true;
            JumpButton.enabled = true;
        }

        if (GUI.Button(new Rect(Screen.width * 0.8109375f, Screen.height * 0.07361111111f, Screen.width * 0.0640625f, Screen.height * 0.1138888889f), "", XButton))
        {
            if (MainMenu.EffectValue)
                InGameOption_XButton.Play();
            PauseOpen = false;
            VirtualJoyStick.JoystickEnable = true;
            JumpButton.enabled = true;
        }
        if (GUI.Button(new Rect(Screen.width * 0.3234375f, Screen.height * 0.177777778f, Screen.width * 0.1765625f, Screen.height * 0.0791666667f), "", SoundButton))
        {
            if (MainMenu.EffectValue)
                InGameOption_SoundSystem.Play();
            Menu = true;
        }
        if (GUI.Button(new Rect(Screen.width * 0.5f, Screen.height * 0.17777778f, Screen.width * 0.1765625f, Screen.height * 0.07916666667f), "", SystemButton))
        {
            if (MainMenu.EffectValue)
                InGameOption_SoundSystem.Play();
            Menu = false;
        }
        if (Menu)//Sound Menu
        {
            if (GUI.Button(new Rect(Screen.width * 0.5203125f, Screen.height * 0.391666667f, Screen.width * 0.1671875f, Screen.height * 0.076388889f), "", BGM_Sound))
            {
                //if Option is chnaged. Stop BGM sound

                if (MainMenu.EffectValue)
                    InGameOption_SoundButton.Play();
                if (BgmSound)
                {
                    BgmSound = false;
                }
                else
                if (!BgmSound)
                {
                    BgmSound = true;
                }
                if (BgmSound)
                {
                    if (Stage1BGM.isPlaying)
                        if (!MainMenu.BGMValue)
                            Stage1BGM.Stop();
                        else
                        {
                            if (!Stage1BGM.isPlaying)
                                if (MainMenu.BGMValue)
                                    Stage1BGM.Play();
                        }
                }
            }
            if (GUI.Button(new Rect(Screen.width * 0.5203125f, Screen.height * 0.5694444444f, Screen.width * 0.1671875f, Screen.height * 0.0763888889f), "", Effect_Sound))
            {
                if (MainMenu.EffectValue)
                    InGameOption_SoundButton.Play();
                if (EffectSound)
                    EffectSound = false;
                else
                    EffectSound = true;
            }
        }
        else//System Menu
        {
            if (GUI.Button(new Rect(Screen.width * 0.38359375f, Screen.height * 0.375f, Screen.width * 0.2328125f, Screen.height * 0.10277778f), "", ButtonRestart))
            {
                if (MainMenu.EffectValue)
                    InGameOption_SystemButton.Play();
                Loading.NextSceneNumber = 1;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");   
            }

            if (GUI.Button(new Rect(Screen.width * 0.38359375f, Screen.height * 0.569444444f, Screen.width * 0.2328125f, Screen.height * 0.1027777778f), "", ButtonMainMenu))
            {
                if (MainMenu.EffectValue)
                    InGameOption_SystemButton.Play();
                Loading.NextSceneNumber = 0;
                
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            }
        }
    }

    void TraceTarget(GameObject Target, GameObject Camera, float Char_x, float Char_y)
    {
        float newX = Mathf.Lerp(Target.transform.position.x, Char_x, Time.deltaTime * 0.001f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        float newY = Mathf.Lerp(Target.transform.position.y, Char_y, Time.deltaTime * 0.001f);
        
        if (Target.transform.position.x <= -4.14f)
            newX = -4.14f;
        if (Target.transform.position.x >= 4.35f)
            newX = 4.35f;
        if (Target.transform.position.y <= -34.8f)
            newY = -34.8f;

        Vector3 newPosition = new Vector3(newX, newY , -10);
        Camera.transform.position = newPosition;
    }

    void TraceTarget_Start(GameObject Camera, GameObject Target)
    {
        float newX = Mathf.Lerp(Camera.transform.position.x, Target.transform.position.x, 0.01f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        float newY = Mathf.Lerp(Camera.transform.position.y, Target.transform.position.y, 0.01f);
        if (Camera.transform.position.x <= -4.14f)
            newX = -4.14f;
        if (Camera.transform.position.x >= 4.35f)
            newX = 4.35f;
        if (Camera.transform.position.y <= -34.8f)
            newY = -34.8f;
        Vector3 newPosition = new Vector3(newX, newY, -10);
        Camera.transform.position = newPosition;
    }

    static public void ItemGaining(Item ItemName)               //Item Gaining
    {
        //int i = CurrentPos;
        CurrentPos++;
        
        ItemStruct[CurrentPos].ItemName = ItemName;
        ItemStruct[CurrentPos].empty = false;
        switch (ItemName)
        {
            case Item.Spaner:
                ItemStruct[CurrentPos].ItemNormal = ItemTexture[0];
                ItemStruct[CurrentPos].ItemActive = ItemTexture_Sel[0];
                ItemStruct[CurrentPos].use = true;
                break;
            case Item.OilSpray:
                ItemStruct[CurrentPos].ItemNormal = ItemTexture[1];
                ItemStruct[CurrentPos].ItemActive = ItemTexture_Sel[1];
                ItemStruct[CurrentPos].use = true;
                break;
            case Item.Nut:
                ItemStruct[CurrentPos].ItemNormal = ItemTexture[3];
                ItemStruct[CurrentPos].ItemActive = ItemTexture_Sel[3];
                ItemStruct[CurrentPos].use = false;
                break;
            case Item.ChainSaw1:
                ItemStruct[CurrentPos].ItemNormal = ItemTexture[4];
                ItemStruct[CurrentPos].ItemActive = ItemTexture_Sel[4];
                ItemStruct[CurrentPos].use = false;
                break;
            case Item.pliers:
                ItemStruct[CurrentPos].ItemNormal = ItemTexture[5];
                ItemStruct[CurrentPos].ItemActive = ItemTexture_Sel[5];
                ItemStruct[CurrentPos].use = true;
                break;
            case Item.ChainSaw2:
                ItemStruct[CurrentPos].ItemNormal = ItemTexture[6];
                ItemStruct[CurrentPos].ItemActive = ItemTexture_Sel[6];
                ItemStruct[CurrentPos].use = false;
                break;
        }

    }

    static public bool FindItem(Item ItemName)
    {
        for (int i = 0; i < 9; i++)
            if (ItemStruct[i].ItemName == ItemName)
            {
                return true;
            }
        return false;
    }

    static public void DeleteItem(Item ItemName)
    {
        for (int i = 0; i < 9; i++)
            if (ItemStruct[i].ItemName == ItemName)
            {
                ItemStruct[i].ItemName = Item.Basic;
                ItemStruct[i].empty = true;
                ItemStruct[i].use = false;
                ItemStruct[i].ItemNormal = _BasicWindow;
                ItemStruct[i].ItemActive = _BasicWindowSel;
                NeedSort = true;
                CurrentPos--;
                return;
            }
    }
    public void JumpClick()
    {
        if (VirtualJoyStick.JoystickEnable)
        {
            if (MainMenu.EffectValue)
                JumpButtonClick.Play();
            CharacterMoving.IsJump = true;
        }
    }

}