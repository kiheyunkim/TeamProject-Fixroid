using UnityEngine;
using System.Collections;

public class Acheievement_Popup : MonoBehaviour
{
    //For Buttons
    public Texture2D Left;
    public Texture2D Left_Sel;
    public Texture2D Right;
    public Texture2D Right_Sel;
    public Texture2D Exit;
    public Texture2D Exit_Sel;

    //For Control Page
    public int CurrentPos;
    public Texture BackGround;
    public GUIStyle LeftButton;
    public GUIStyle RightButton;

    //For CutScenes
    public Texture[] CutScene1 = new Texture[3];
    public Texture[] CutScene2 = new Texture[4];
    private Texture[] TargetScene = new Texture[4];

    //For Contol Max/Min
    private int MinLeft;
    private int MaxRight;

    //For Sound
    public AudioSource LeftRightClickInButton;
    public AudioSource XButton;
    // Use this for initialization
    void Start ()
    {
        CurrentPos = 0;
	}

    // Update is called once per frame
    void Update()
    {
        if (CurrentPos == MinLeft)
        {
            LeftButton.normal.background = null;
            LeftButton.active.background = null;
            RightButton.normal.background = Right;
            RightButton.active.background = Right_Sel;
        }
        else
        if (CurrentPos == MaxRight)
        {
            LeftButton.normal.background = Left;
            LeftButton.active.background = Left_Sel;
            RightButton.normal.background = Exit;
            RightButton.active.background = Exit_Sel;
        }
        else
        {
            LeftButton.normal.background = Left;
            LeftButton.active.background = Left_Sel;
            RightButton.normal.background = Right;
            RightButton.active.background = Right_Sel;
        }

    }
    void OnGUI()
    {
        switch (Acheievement_SCrollRectSnap.MidCard)
        {
            case 0:
                TargetScene = CutScene1;
                MinLeft = 0;
                MaxRight = 2;
                break;
            case 1:
                TargetScene = CutScene2;
                MinLeft = 0;
                MaxRight = 3;
                break;
        }
        GUI.depth = 1;
        if(Acheievement.PopupOpen)
        {
            //Acheievement_SCrollRectSnap.MidCard
            if (CurrentPos >= MinLeft && CurrentPos <= MaxRight)
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), TargetScene[CurrentPos]); //For CutScene

            if(GUI.Button(new Rect(Screen.width * 0.015625f, Screen.height * 0.833333333333333333f, Screen.width * 0.078125f, Screen.height * 0.13888888888888888889f), "", LeftButton))
            {
                if (MainMenu.EffectValue)
                    LeftRightClickInButton.Play();
                if (CurrentPos > MinLeft)
                    CurrentPos -= 1;
            }

            if(GUI.Button(new Rect(Screen.width * 0.90625f, Screen.height * 0.833333333333333333f, Screen.width * 0.078125f, Screen.height * 0.13888888888888888889f), "", RightButton))
            {
                if (CurrentPos < MaxRight )
                {
                    CurrentPos += 1;
                    if (MainMenu.EffectValue)
                        LeftRightClickInButton.Play();

                }
                else
                if (CurrentPos == MaxRight )
                {
                    Acheievement.PopupOpen = false;
                    if (MainMenu.EffectValue)
                        XButton.Play();
                }
            }
        }
        else
        { 
            CurrentPos = 0;
        }
    }
}
