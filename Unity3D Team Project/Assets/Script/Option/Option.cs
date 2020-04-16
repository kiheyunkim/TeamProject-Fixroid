//MainMenu에서 
using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour
{        
    //For Alpha Texture
    public Texture Alpha;//Texture for Alpha Channel

    //For Button Switching Texture
    public Texture2D SoundOn;
    public Texture2D SoundOff;
    public Texture2D SoundOn_On;
    public Texture2D SoundOff_On;
    public Texture2D VibrationOn;
    public Texture2D VibrationOff;
    public Texture2D VibrationOn_On;
    public Texture2D VibrationOff_On;
    public Texture2D RIghtHand;
    public Texture2D LeftHand;

    //For Buttons
    public GUIStyle ButtonX;
    public GUIStyle ButtonCredit;
    public GUIStyle BGM;
    public GUIStyle Effect; 
    public GUIStyle Vibration;
    public GUIStyle Hand;

    //boolean
    private bool BgmOn;//true : on , false : off
    private bool EffectOn;//true : on, false : off
    private bool VibrateOn;//true : on, false : off
    private bool HandOption;//true : right, false left

    //for sound
    public AudioSource OptionBGM;
    public AudioSource SoundClick;
    public AudioSource RightLeftClick;
    public AudioSource XButtonSound;
    public AudioSource CreditClick;

    void Start()
    {
        HandOption = MainMenu.LeftOrRight;
        if (MainMenu.BGMValue)  //for BGM Play
            OptionBGM.Play();

        //Load Original Sound Option
        BgmOn = MainMenu.BGMValue;
        EffectOn = MainMenu.EffectValue;
        VibrateOn = MainMenu.VibrateValue;
    }
    void Update()
    {
        MainMenu.LeftOrRight = HandOption;
        //Texture Switching for BGM Button
        if (BgmOn)
        {
            BGM.normal.background = SoundOn;
            BGM.active.background = SoundOn_On;
        }
        else
        {
            BGM.normal.background = SoundOff;
            BGM.active.background = SoundOff_On;
        }

        //Texture Switching for Effect Button
        if (EffectOn)
        {
            Effect.normal.background = SoundOn;
            Effect.active.background = SoundOn_On;  
        }
        else
        {
            Effect.normal.background = SoundOff;
            Effect.active.background = SoundOff_On;
        }

        //Texture Switching for Vibration Button
        if (VibrateOn)
        {
            Vibration.normal.background = VibrationOn;
            Vibration.active.background = VibrationOn_On;
        }
        else
        {
            Vibration.normal.background = VibrationOff;
            Vibration.active.background = VibrationOff_On;
        }

        //Texture Switching for HandOption Button
        if (!HandOption)
            Hand.normal.background = RIghtHand;
        else
            Hand.normal.background = LeftHand;
    }

    void OnGUI()
    {
        //For Option Alpha
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), Alpha);
    
        //For XButton
        if (GUI.Button(new Rect(Screen.width * 0.7390625f, Screen.height * 0.0277777777778f, Screen.width * 0.0859375f, Screen.height * 0.1527777778f), "", ButtonX))   //X Button Image
        {
            if (MainMenu.EffectValue)
                XButtonSound.Play();
            print("MainMenu");
            Loading.NextSceneNumber = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }

        //for Credit Button
        if (GUI.Button(new Rect(Screen.width * 0.875f, Screen.height * 0.902777777777777778f, Screen.width * 0.109375f, Screen.height * 0.06944444444444444444f), "", ButtonCredit))   //for Credit
        {
            if (MainMenu.EffectValue)
                CreditClick.Play();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Credit");
        }

        //for BGM Button
        if (GUI.Button(new Rect(Screen.width * 0.546875f, Screen.height * 0.315625f, Screen.width * 0.15625f, Screen.height * 0.07083333333f), "",BGM))   //for BGM
        {
            if (MainMenu.EffectValue)
                SoundClick.Play();
            if (BgmOn)
            {
                BgmOn = false;
                MainMenu.BGMValue = false;
            }
            else
            {
                BgmOn = true;
                MainMenu.BGMValue = true;
            }
        }

        //for EffectSound Button
        if (GUI.Button(new Rect(Screen.width * 0.5468875f, Screen.height * 0.436458333333333f, Screen.width * 0.15625f, Screen.height * 0.07083333333f), "", Effect))     //for Effect
        {
            if (MainMenu.EffectValue)
                SoundClick.Play();

            if (EffectOn)
            {
                EffectOn = false;
                MainMenu.EffectValue = false;
            }
            else
            {
                EffectOn = true;
                MainMenu.EffectValue = true;
            }
        }

        //for Vibrate Button
        if (GUI.Button(new Rect(Screen.width * 0.546875f, Screen.height * 0.55763888888889f, Screen.width * 0.15625f, Screen.height * 0.0708333333333333f), "", Vibration))     //for Vibrate
        {
            if (MainMenu.EffectValue)
                SoundClick.Play();

            if (VibrateOn)
            {
                VibrateOn = false;
                MainMenu.VibrateValue = false;
            }
            else
            {
                VibrateOn = true;
                MainMenu.VibrateValue = true;
            }   
        }

        //for HandOption Button
        if (GUI.Button(new Rect(Screen.width * 0.53046875f, Screen.height * 0.676736111111f, Screen.width * 0.1890625f, Screen.height * 0.072222222222f), "", Hand))     //for HandOption
        {

            if (MainMenu.EffectValue)
                RightLeftClick.Play();

            if (MainMenu.LeftOrRight)
                MainMenu.LeftOrRight = false;
            else
                MainMenu.LeftOrRight = true;

            if (HandOption)
                HandOption = false;
            else
                HandOption = true;
        }   
    }
}