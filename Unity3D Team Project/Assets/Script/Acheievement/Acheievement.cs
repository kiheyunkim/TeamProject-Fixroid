//MainMenu에서
using UnityEngine;
using System.Collections;

public class Acheievement : MonoBehaviour
{
    //Global Trigger Boolean
    static public bool PopupOpen;
    static public bool[] isGet = new bool[6];

    //For BackGround;
    public Texture BackGroundTexture;
    
    //For Buttons
    public GUIStyle Next;
    public GUIStyle Prev;
    public GUIStyle Exit;

    //For Sound;
    public AudioSource AchievementBGM;
    public AudioSource LeftRightButton;
    public AudioSource XButtonSound;
    void Start()
    {
        if (MainMenu.BGMValue)
            AchievementBGM.Play();  
        Screen.SetResolution(1280, 720, true);
        PopupOpen = false;
        isGet[0] = true;
        isGet[1] = true; ;
    }
    void OnGUI()
    {
        GUI.depth = 2;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackGroundTexture);

        if (GUI.Button(new Rect(Screen.width * 0.175f, Screen.height * 0.42361f, Screen.width * 0.085975f, Screen.height * 0.1527f), " ",Prev))
        {
            if(!PopupOpen)
            {
                if (MainMenu.EffectValue)
                    LeftRightButton.Play();
                if (Acheievement_SCrollRectSnap.MidCard > 0)
                    Acheievement_SCrollRectSnap.MidCard--;
            }
        }
        if (GUI.Button(new Rect(Screen.width * 0.7390625f, Screen.height * 0.42361f, Screen.width * 0.085975f, Screen.height * 0.1527f)," ", Next))
        {
            if (!PopupOpen)
            {
                if (MainMenu.EffectValue)
                    LeftRightButton.Play();
                if (Acheievement_SCrollRectSnap.MidCard < 5)
                    Acheievement_SCrollRectSnap.MidCard++;
            }
        }
        if (GUI.Button(new Rect(Screen.width * 0.7390625f, Screen.height * 0.027f, Screen.width * 0.0859375f, Screen.height * 0.1527f)," " ,Exit))
        {
            if (!PopupOpen)
            {
                if (MainMenu.EffectValue)
                    XButtonSound.Play();
                Loading.NextSceneNumber = 0;
                UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
            }
        }
    }
   
 }
