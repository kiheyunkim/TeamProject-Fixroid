using System.Collections;
using UnityEngine;

public class StartScene : MonoBehaviour
{
    private float scrollSpeed = 1f;
    private bool scrollEnd = false;
    private bool animationStart = false;

    private GameObject quad;
    private GameObject robot;

    private AudioSource bgmSound;
    private AudioSource clickSound;

    private BlinkController blinkController;
    private SettingManager settingManager;
    private SaveManager saveManager;

    void Awake()
    {
        bgmSound = AudioSetter.SetBgm(gameObject, "Sound/StartScene/StartSceneBgm");
        clickSound = AudioSetter.SetEffect(gameObject, "Sound/StartScene/StartSceneClick");
        blinkController = GetComponent<BlinkController>();

        settingManager = SettingManager.GetInstance;
        saveManager = SaveManager.GetInstance;

        AudioSetter.SetBGMVolume(settingManager.SettingTile);
        AudioSetter.SetEffectVolume(settingManager.SettingTile);
    }
    // Use this for initialization
    void Start()
    {
        quad                                        = transform.parent.gameObject;
        robot                                       = GameObject.Find("Body");
        saveManager.NotWarning();
        bgmSound.Play();
    }

    void Update()
    {
        if (!scrollEnd) quad.gameObject.transform.Translate(new Vector3(0, scrollSpeed * Time.deltaTime));
        if (quad.transform.position.y > 6.3f && !scrollEnd)
        {
            scrollEnd = true;
            animationStart = true;
        }

        if (animationStart)
        {
            blinkController.BlinkStart();
            animationStart = false;
        }

        if (robot.GetComponent<Animator>().GetBool("End"))
        {
            SceneManager.NextSceneNumber = 0;
            UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
        }
    }

    void OnMouseDown()
    {
        if(scrollEnd)
        {
            blinkController.BlinkEnd();
            clickSound.Play();
            robot.GetComponent<Animator>().SetTrigger("HeadUp");
        }
        
    }
}
