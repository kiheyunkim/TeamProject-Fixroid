using UnityEngine;
using System.Collections;

public class MainMenuExit : MonoBehaviour
{
    //Global Trigger Boolean
    static public bool ExitMenu;
    
    //Background Texture
    public Texture ExitAlphaBG;

    //For Buttons
    public GUIStyle Prev;
    public GUIStyle Exit;
    public Texture2D ExitClicked;

    //For Sound
    public AudioSource ExitButton;
    public AudioSource ReturnButton;
    void Start()
    {
        ExitMenu = false;
    }
    void OnGUI()
    {
        if (ExitMenu)
        {
            GUI.depth = -1;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), ExitAlphaBG);

            if (GUI.Button(new Rect(Screen.width * 0.37734375f, Screen.height * 0.5583333333f, Screen.width * 0.075f, Screen.height * 0.133333333f), "", Exit))//For Prev Button
            {
                ExitButton.Play();
                Exit.normal.background = ExitClicked;
                Application.Quit();
            }
            if (GUI.Button(new Rect(Screen.width * 0.55f, Screen.height * 0.55833333333f, Screen.width * 0.075f, Screen.height * 0.13f), "", Prev))//For Nex Button
            {
                ReturnButton.Play();
                MainMenuExit.ExitMenu = false;
            }
        }
    }
}
