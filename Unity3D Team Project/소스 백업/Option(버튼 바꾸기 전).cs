//MainMenu에서 
using UnityEngine;
using System.Collections;

public class Option : MonoBehaviour
{
    //for Position
    public float TitlePosX1;
    public float TitlePosY1;
    public float BGMSoundPosX1;
    public float BGMSoundPosY1;
    public float EffectSoundPosX1;
    public float EffectSoundPosY1;
    public float RightLeftPosX1;
    public float RightLeftPosY1;

    public float XPosX;
    public float XPosY;
    public float CreditPosX;
    public float CreditPosY;

    //For Texture
    public Texture backgroundTexture;//Texture for Option background
    public Texture OptionTitle;//Texture for Option Title
    public Texture BGMSound;
    public Texture EffectSound;
    public Texture RightLeft;
    public Texture Previous;

    //For Buttons
    public GUIStyle ButtonX;
    public GUIStyle ButtonClicked;


    public GUIStyle ButtonCreadit;
    public GUIStyle ButtonCreditClicked;
    public GUIStyle BgmOn;
    public GUIStyle BgmOff;
    public GUIStyle EffectOn;
    public GUIStyle EffectOff;
    

    //for sound
    public UnityEngine.Audio.AudioMixer Mixer; 
    public AudioSource ClickSound;

    void Start()
    {
       
        Mixer = Resources.Load("MasterMix") as UnityEngine.Audio.AudioMixer;
        string _OutputMixer = "OptionEffect";
        GetComponent<AudioSource>().outputAudioMixerGroup = Mixer.FindMatchingGroups(_OutputMixer)[0];


    }
    void OnGUI()
    { 
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), backgroundTexture);//for Display Option Background Texture
        GUI.DrawTexture(new Rect(Screen.width * TitlePosX1, Screen.height * TitlePosY1, Screen.width * 0.8f, Screen.height * 0.1f), OptionTitle);//for Display Option Title
        GUI.DrawTexture(new Rect(Screen.width * BGMSoundPosX1, Screen.height * BGMSoundPosY1, Screen.width * 0.3f, Screen.height * 0.1f), BGMSound);//for Dipslay BgmSound Option Title
        GUI.DrawTexture(new Rect(Screen.width * EffectSoundPosX1, Screen.height * EffectSoundPosY1, Screen.width * 0.3f, Screen.height * 0.1f), EffectSound);//for Dipslay EfectSound Option Title
        GUI.DrawTexture(new Rect(Screen.width * RightLeftPosX1, Screen.height * RightLeftPosY1, Screen.width * 0.3f, Screen.height * 0.1f), RightLeft);//for Display Right or Left Option
        MainMenu.LeftOrRight = GUI.Toggle(new Rect(Screen.width * RightLeftPosX1 + 500, Screen.height * RightLeftPosY1 + 15, Screen.width * 0.3f, Screen.height * 0.1f), MainMenu.LeftOrRight, "Left Hand Option");

        if (MainMenu.showGuiOutline)
        {
            if (GUI.Button(new Rect(Screen.width * CreditPosX, Screen.height * CreditPosY, Screen.width * 0.1f, Screen.height * 0.1f), "Credit"))
            {
                print("Credit");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Credit");
            }

            if (GUI.Button(new Rect(Screen.width *XPosX, Screen.height *XPosY, Screen.width *0.05f, Screen.height *0.1f), "X"))
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }

            if (GUI.Button(new Rect(Screen.width * XPosX, Screen.height * XPosY, Screen.width * 0.05f, Screen.height * 0.1f), "BGM ON"))
            {
                
            }

            if (GUI.Button(new Rect(Screen.width * XPosX, Screen.height * XPosY, Screen.width * 0.05f, Screen.height * 0.1f), "BGM OFF"))
            {
                //BGM OFF
            }

            if (GUI.Button(new Rect(Screen.width * XPosX, Screen.height * XPosY, Screen.width * 0.05f, Screen.height * 0.1f), "EFFECT ON"))
            {
                //Effect On
            }

            if (GUI.Button(new Rect(Screen.width * XPosX, Screen.height * XPosY, Screen.width * 0.05f, Screen.height * 0.1f), "EFFECT OFF"))
            {
              //Effect OFf
            }
        }
        else
        {
            if (GUI.Button(new Rect(Screen.width * CreditPosX, Screen.height * CreditPosY, Screen.width * 0.1f, Screen.height * 0.1f), "",ButtonCreadit))
            {
                print("Credit");
                UnityEngine.SceneManagement.SceneManager.LoadScene("Credit");
            }
            if (GUI.Button(new Rect(Screen.width * XPosX, Screen.height * XPosY, Screen.width * 0.1f, Screen.height * 0.1f), "", ButtonX))
            {
                print("MainMenu");
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }

    }
}