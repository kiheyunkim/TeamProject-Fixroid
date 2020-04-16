using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlurManager : MonoBehaviour
{
    private Color clearColor = new Color(1, 1, 1, 0);

    private AudioSource successSound;
    private AudioSource failSound;
    private AudioSource pauseButtonSound;

    private Material blurMaterial;
    private UnityEngine.UI.Image blurBg;
    private UnityEngine.UI.Image acheiveOkImg;
    private UnityEngine.UI.Image sucessImg;
    private UnityEngine.UI.Image failImage;

    private PhrasesController phrasesController;
    private ProgressManager progressManager;
    private TimerController timerController;

    //Prefab
    private GameObject gamesuccessScreen;
    private GameObject gamefailScreen;
    private GameObject pauseMenu;
    //Prefab-Game
    private GameObject handleGame;
    private GameObject randomGameUp;
    private GameObject randomGameDown;
    private GameObject randomGameSwitch;
    private GameObject nutGame;
    private GameObject springGame;

    public enum RequestType { GameSucess, GameSucessWIthAcheive, GameFail, Pause, HandleGame, RandomTouchGameUp, RandomTouchGameDown, RandomTouchGameSwitch, NutGame, Spring };
    public enum EndType { MinigameSuccess, MinigameFail, Pause }

    private void Awake()
    {
        successSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/MiniGameResult/MiniGameSuccess");
        failSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/MiniGameResult/MiniGameFail");
        pauseButtonSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/GUI/PauseButtonSOund");

        blurBg = GetComponent<UnityEngine.UI.Image>();
        blurMaterial = blurBg.material;
        blurMaterial.SetColor(Shader.PropertyToID("_Color"), clearColor);
        blurMaterial.SetFloat(Shader.PropertyToID("_Size"), 0);

        UnityEngine.UI.Image[] images = transform.GetComponentsInChildren<UnityEngine.UI.Image>();
        acheiveOkImg = images[1];
        sucessImg = images[2];
        failImage = images[3];
        acheiveOkImg.color = sucessImg.color = failImage.color = clearColor;

        gamesuccessScreen = Resources.Load<GameObject>("Prefab/SuccessScore");
        gamefailScreen = Resources.Load<GameObject>("Prefab/failScore");
        pauseMenu = Resources.Load<GameObject>("Prefab/PauseMenu");

        handleGame = Resources.Load<GameObject>("Prefab/MiniGame/HandleGame");
        randomGameUp = Resources.Load<GameObject>("Prefab/MiniGame/RandomTouchUp");
        randomGameDown = Resources.Load<GameObject>("Prefab/MiniGame/RandomTouchDown");
        randomGameSwitch = Resources.Load<GameObject>("Prefab/MiniGame/RandomTouchSwitch");
        nutGame = Resources.Load<GameObject>("Prefab/MiniGame/NutGame");
        springGame = Resources.Load<GameObject>("Prefab/MiniGame/SpringGame");

        progressManager = ProgressManager.GetInstance;
    }

    private void Start()
    {
        phrasesController = UIController.GetInstance.PhrasesController;
        timerController = UIController.GetInstance.TimerController;
    }

    private IEnumerator Bluring(RequestType requestType)
    {
        float value = 1.0f;

        while (value > 0.09f)
        {
            blurMaterial.SetColor(Shader.PropertyToID("_Color"), new Color(value, value, value, 0));
            value -= 0.05f;
            yield return new WaitForEndOfFrame();
        }

        blurMaterial.SetColor(Shader.PropertyToID("_Color"), new Color(0.1f, 0.1f, 0.1f, 0));
        value = 0.0f;

        while (value < 1.0f)
        {
            blurMaterial.SetFloat(Shader.PropertyToID("_Size"), value);
            value += 0.05f;
            yield return new WaitForEndOfFrame();
        }

        switch (requestType)
        {
            case RequestType.GameSucess:
                Instantiate(gamesuccessScreen);
                break;
            case RequestType.GameSucessWIthAcheive:
                yield return StartCoroutine(Blinking(acheiveOkImg));
                Instantiate(gamesuccessScreen);
                break;
            case RequestType.GameFail:
                Instantiate(gamefailScreen);
                break;
            case RequestType.Pause:
                Instantiate(pauseMenu);
                break;
            case RequestType.HandleGame:
                Instantiate(handleGame);
                break;
            case RequestType.RandomTouchGameUp:
                Instantiate(randomGameUp);
                break;
            case RequestType.RandomTouchGameDown:
                Instantiate(randomGameDown);
                break;
            case RequestType.RandomTouchGameSwitch:
                Instantiate(randomGameSwitch);
                break;
            case RequestType.NutGame:
                Instantiate(nutGame);
                break;
            case RequestType.Spring:
                Instantiate(springGame);
                break;
        }

        yield break;
    }

    private IEnumerator DeBluring(RequestType requestType, EndType endType)
    {
        switch (endType)
        {
            case EndType.MinigameSuccess:
                successSound.Play();
                yield return StartCoroutine(Blinking(sucessImg));
                break;
            case EndType.MinigameFail:
                failSound.Play();
                yield return StartCoroutine(Shaking(failImage));
                break;
            case EndType.Pause:
                break;
        }

        float value = 1.0f;
        while (value >= 0)
        {
            blurMaterial.SetFloat(Shader.PropertyToID("_Size"), value);
            value -= 0.1f;
            yield return new WaitForEndOfFrame();
        }

        value = 0.1f;
        blurMaterial.SetFloat(Shader.PropertyToID("_Size"), value);

        while (value < 1.00f)
        {
            blurMaterial.SetColor(Shader.PropertyToID("_Color"), new Color(value, value, value, 0));
            value += 0.02f;
            yield return new WaitForEndOfFrame();
        }
        blurMaterial.SetColor(Shader.PropertyToID("_Color"), new Color(1.0f, 1.0f, 1.0f, 0));

        blurBg.raycastTarget = false;

        if (endType == EndType.MinigameSuccess)
        {
            switch (requestType)
            {
                case RequestType.HandleGame:
                    phrasesController.StartPhrase(PhrasesController.MiniGameEndNotify.Handle);
                    break;
                case RequestType.RandomTouchGameUp:
                    phrasesController.StartPhrase(PhrasesController.ItemGetNotify.Piler);
                    break;
                case RequestType.RandomTouchGameDown:
                    phrasesController.StartPhrase(PhrasesController.ItemGetNotify.Spanner);
                    break;
                case RequestType.RandomTouchGameSwitch:
                    phrasesController.StartPhrase(PhrasesController.ItemGetNotify.SawB);
                    yield return new WaitForSeconds(2.0f);
                    phrasesController.StartPhrase(PhrasesController.AreaNotify.NeedSaw);
                    break;
                case RequestType.Spring:
                    phrasesController.StartPhrase(PhrasesController.MiniGameEndNotify.Spring);
                    yield return new WaitForSeconds(1.5f);
                    Camera.main.GetComponent<CameraControl>().StartFinalWalking();
                    break;
                case RequestType.NutGame:
                    progressManager.RemoveItem(HandButton.ItemType.Nut);
                    break;
                default:
                    break;
            }
        }

        ProgressManager.GetInstance.IsGamePlaying = false;

        yield break;
    }

    private IEnumerator Blinking(UnityEngine.UI.Image image)
    {
        float value = 0.0f;

        while (value < 1)
        {
            image.color = new Color(1, 1, 1, value);
            value += 0.1f;
            yield return new WaitForEndOfFrame();
        }

        value = 1.0f;
        image.color = new Color(1, 1, 1, 1);
        yield return new WaitForSecondsRealtime(2.0f);

        while (value > 0)
        {
            image.color = new Color(1, 1, 1, value);
            value -= 0.1f;
            yield return new WaitForEndOfFrame();
        }

        image.color = new Color(1, 1, 1, 0);

        yield return new WaitForSecondsRealtime(2.0f);
        yield break;
    }

    private IEnumerator Shaking(UnityEngine.UI.Image target)
    {
        target.transform.localPosition = Vector3.zero;
        int step = 0;

        target.color = new Color(1, 1, 1, 1);

        while (step <= 20)
        {
            target.transform.localPosition = new Vector3(Random.Range(-50, 50), Random.Range(-50, 50), 0);
            step += 1;
            yield return new WaitForEndOfFrame();
        }

        target.color = new Color(1, 1, 1, 0);
        yield break;
    }

    public void StartBlur(int i)        //For Pause Push
    {
        pauseButtonSound.Play();
        if (progressManager.IsTimerAttackStart)
            timerController.PauseTimer();

        RequestType requestType = (RequestType)i;
        blurBg.raycastTarget = true;
        StartCoroutine(Bluring(requestType));
    }

    public void StartBlur(RequestType requestType)
    {
        blurBg.raycastTarget = true;
        StartCoroutine(Bluring(requestType));
    }


    public void CloseBlur(RequestType requestType, EndType endType)
    {
        StartCoroutine(DeBluring(requestType, endType));
    }
}