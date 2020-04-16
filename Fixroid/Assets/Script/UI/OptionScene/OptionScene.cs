using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class OptionScene : MonoBehaviour
{
    private List<Texture2D> texture2Ds;

    //for alpha texture
    private Texture alphaBkg;

    //for button switching Texture
    private Texture2D onNormal;
    private Texture2D onClicked;
    private Texture2D offNormal;
    private Texture2D offClicked;

    private AudioSource bgmSound;
    private AudioSource creditBttnSound;
    private AudioSource bttnSound;
    private AudioSource xBttnSound;

    private GUIStyle buttonX = new GUIStyle();
    private GUIStyle buttonCredit = new GUIStyle();
    private GUIStyle bgm = new GUIStyle();
    private GUIStyle effect = new GUIStyle();
    private GUIStyle vibration = new GUIStyle();

    private Rect bttnXRect;
    private Rect bttnCreditRect;
    private Rect bttnBGMRect;
    private Rect bttnEffectRect;
    private Rect bttnVibrateRect;
    private Rect screemRect;

    private bool isUpdated;
    private SettingManager settingManager;

    void Awake()
    {
        isUpdated = true;

        bttnXRect = new Rect(Screen.width * 0.739f, Screen.height * 0.0278f, Screen.width * 0.0859f, Screen.height * 0.1528f);
        bttnCreditRect = new Rect(Screen.width * 0.875f, Screen.height * 0.9028f, Screen.width * 0.1094f, Screen.height * 0.0694f);
        bttnBGMRect = new Rect(Screen.width * 0.547f, Screen.height * 0.3156f, Screen.width * 0.1563f, Screen.height * 0.0708f);
        bttnEffectRect = new Rect(Screen.width * 0.547f, Screen.height * 0.4365f, Screen.width * 0.1563f, Screen.height * 0.0708f);
        bttnVibrateRect = new Rect(Screen.width * 0.547f, Screen.height * 0.5576f, Screen.width * 0.1563f, Screen.height * 0.0708f);
        screemRect = new Rect(0, 0, Screen.width, Screen.height);
    }

    void Start()
    {
        settingManager = SettingManager.GetInstance;

        texture2Ds = Utils.Sprites2Textures2D(Resources.LoadAll<Sprite>("Option/OptionUI"));
        alphaBkg = texture2Ds[11];
        onNormal = texture2Ds[10];
        onClicked = texture2Ds[8];
        offNormal = texture2Ds[6];
        offClicked = texture2Ds[4];
        buttonX.normal.background = texture2Ds[9];
        buttonX.active.background = texture2Ds[7];
        buttonCredit.normal.background = texture2Ds[2];
        buttonCredit.active.background = texture2Ds[1];

        bgmSound = AudioSetter.SetBgm(gameObject, "Sound/Option/OptionBgm");
        creditBttnSound = AudioSetter.SetEffect(gameObject, "Sound/Option/CreditBttn");
        bttnSound = AudioSetter.SetEffect(gameObject, "Sound/Option/OptionBttnClick");
        xBttnSound = AudioSetter.SetEffect(gameObject, "Sound/Option/OptionXBttn");

        bgmSound.Play();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!isUpdated) return;

        if(settingManager.SettingTile.bgm)
        {
            bgm.normal.background = onNormal;
            bgm.active.background = onClicked;
        }
        else
        {
            bgm.normal.background = offNormal;
            bgm.active.background = offClicked;
        }

        //Texture Switching for Effect Button
        if (settingManager.SettingTile.effect)
        {
            effect.normal.background = onNormal;
            effect.active.background = onClicked;
        }
        else
        {
            effect.normal.background = offNormal;
            effect.active.background = offClicked;
        }

        //Texture Switching for Vibration Button
        if (settingManager.SettingTile.vibrate)
        {
            vibration.normal.background = onNormal;
            vibration.active.background = onClicked;
        }
        else
        {
            vibration.normal.background = offNormal;
            vibration.active.background = offClicked;
        }

        isUpdated = false;
    }

    void OnGUI()
    {
        GUI.DrawTexture(screemRect, alphaBkg);

        if(GUI.Button(bttnXRect,"",buttonX))
        {
            xBttnSound.Play();
            SceneManager.NextSceneNumber = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }

        if (GUI.Button(bttnCreditRect, "", buttonCredit))
        {
            creditBttnSound.Play();
            SceneManager.NextSceneNumber = 7;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }

        if (GUI.Button(bttnBGMRect, "", bgm))
        {
            bttnSound.Play();

            settingManager.SettingTile.bgm = settingManager.SettingTile.bgm ? false : true;
            AudioSetter.SetBGMVolume(settingManager.SettingTile);
            isUpdated = true;
        }

        if (GUI.Button(bttnEffectRect, "", effect))
        {
            bttnSound.Play();

            settingManager.SettingTile.effect = settingManager.SettingTile.effect ? false : true;
            AudioSetter.SetEffectVolume(settingManager.SettingTile);
            isUpdated = true;
        }

        if (GUI.Button(bttnVibrateRect, "", vibration))
        {
            bttnSound.Play();

            settingManager.SettingTile.vibrate = settingManager.SettingTile.vibrate ? false : true;
            isUpdated = true;
        }
    }
}
