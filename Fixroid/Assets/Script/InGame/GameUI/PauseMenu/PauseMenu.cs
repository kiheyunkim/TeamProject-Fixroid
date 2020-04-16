using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    private UnityEngine.UI.Image bg;
    private UnityEngine.UI.Image bgmButton;
    private UnityEngine.UI.Image sysButton;

    private Sprite soundBG;
    private Sprite systemBG;

    private Sprite soundBttn;
    private Sprite soundBttnClicked;
    private Sprite systemBttn;
    private Sprite systemBttnClicked;

    private GameObject soundPrefab;
    private GameObject sysPrefab;
    private GameObject currenObject;

    private AudioSource switchButtonSound;
    private AudioSource xButtonSound;

    private TimerController timerController;
    private ProgressManager progressManager;

    private void Awake()
    {
        switchButtonSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/PuaseMenu/MenuSwitchSound");
        xButtonSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/PuaseMenu/PauseXSound");

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/PauseMenu");
        systemBG = sprites[0];
        soundBG = sprites[1];
        soundBttn = sprites[6];
        soundBttnClicked = sprites[7];
        systemBttn = sprites[10];
        systemBttnClicked = sprites[11];

        UnityEngine.UI.Image[] images = GetComponentsInChildren<UnityEngine.UI.Image>();
        bg = images[0];
        bgmButton = images[1];
        sysButton = images[2];

        bgmButton.sprite = soundBttnClicked;
        sysButton.sprite = systemBttn;

        soundPrefab = Resources.Load<GameObject>("Prefab/PauseSound");
        sysPrefab = Resources.Load<GameObject>("Prefab/PauseSys");

        bg.sprite = soundBG;
        currenObject = Instantiate(soundPrefab, transform, false);
    }

    private void Start()
    {
        timerController = UIController.GetInstance.TimerController;
        progressManager = ProgressManager.GetInstance;
    }

    private IEnumerator CloseSound()
    {
        xButtonSound.Play();
        yield break;
    }

    public void PushSound()
    {
        switchButtonSound.Play();
        bg.sprite = soundBG;
        bgmButton.sprite = soundBttnClicked;
        sysButton.sprite = systemBttn;
        Destroy(currenObject);
        currenObject = Instantiate(soundPrefab, transform, false);
    }

    public void PushSystem()
    {
        switchButtonSound.Play();
        bg.sprite = systemBG;
        bgmButton.sprite = soundBttn;
        sysButton.sprite = systemBttnClicked;
        Destroy(currenObject);
        currenObject = Instantiate(sysPrefab, transform, false);
    }

    public void ClosePauseMenu()
    {
        StartCoroutine(CloseSound());
        UIController.GetInstance.BlurManager.CloseBlur(BlurManager.RequestType.Pause, BlurManager.EndType.Pause);
        if (progressManager.IsTimerAttackStart)
            timerController.RestartTimer();
        Destroy(gameObject);
    }
}
