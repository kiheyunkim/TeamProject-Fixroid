using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTouchGame : MonoBehaviour
{
    public enum GameType { Up, Down, Switch };
    public GameType gameType;

    private UnityEngine.UI.Image barImg;
    private List<GameObject> buttons = new List<GameObject>();
    private bool[] checker = new bool[5] { false, false, false, false, false };

    private BlurManager blurManager;
    private Coroutine timerCoroutine;
    private List<Coroutine> disAppearRoutines = new List<Coroutine>();
    private bool gameStart = false;

    private DownerSaw downerSaw;
    private UpperSaw upperSaw;
    private ProgressManager progressManager;
    private AreaController areaController;

    private AudioSource appearSound;
    private AudioSource touchSound;

    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main.GetComponent<Camera>();
        barImg = transform.GetChild(1).GetComponent<UnityEngine.UI.Image>();

        foreach (Transform button in transform.GetChild(2))
            buttons.Add(button.gameObject);

        appearSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/RandomTouch/ButtonAppearSound");
        touchSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/RandomTouch/ButtonTouchSound");

        StartCoroutine(CollocateButtons());
    }

    private IEnumerator TimerCount(float time)
    {
        float tick = 0f;
        while (tick < time)
        {
            barImg.fillAmount = (time - tick) / time;
            tick += 0.01f;
            yield return new WaitForSecondsRealtime(0.01f);
        }

        CleanUpGame();

        ProcessForPerState(gameType, BlurManager.EndType.MinigameFail);

        Destroy(gameObject);
        StopCoroutine(timerCoroutine);
    }

    private IEnumerator CollocateButtons()
    {
        List<Vector2> poses = new List<Vector2>();

        for (int i = 0; i < buttons.Count;)
        {
            int row = Random.Range(0, 3);
            int col = Random.Range(0, 7);

            if (poses.Contains(new Vector2(col, row)))
                continue;
            poses.Add(new Vector2(col, row));
            appearSound.Play();

            buttons[i].transform.localPosition = new Vector3(-568 + 52 + col * 152, -280 + 52 + row * 152, 0);

            float step = 0;
            UnityEngine.UI.Image button = buttons[i].GetComponent<UnityEngine.UI.Image>();

            while (step <= 1)
            {
                button.fillAmount = step;
                step += 0.05f;
                yield return new WaitForEndOfFrame();
            }
            button.fillAmount = 1.0f;

            i++;
        }

        timerCoroutine = StartCoroutine(TimerCount(10));
        gameStart = true;
    }

    private IEnumerator Disappear(GameObject target)
    {
        UnityEngine.UI.Image image = target.GetComponent<UnityEngine.UI.Image>();

        float step = 1;
        while (step > 0)
        {
            image.fillAmount = step;
            step -= 0.05f;
            yield return new WaitForSeconds(0.01f);
        }
        image.fillAmount = 0f;
    }

    private void Start()
    {
        blurManager = UIController.GetInstance.BlurManager;
        upperSaw = SawController.GetInstance.UpperSaw;
        downerSaw = SawController.GetInstance.DownerSaw;

        progressManager = ProgressManager.GetInstance;
        areaController = AreaController.GetInstance;
    }

    public void Touch(int i)
    {
        if (!gameStart)
            return;

        if (checker[i])
            return;

        touchSound.Play();

        if (i != 0)
            if (!checker[i - 1])
            {
                CleanUpGame();
                StopCoroutine(timerCoroutine);
                Destroy(gameObject);
                ProcessForPerState(gameType, BlurManager.EndType.MinigameFail);
            }

        checker[i] = true;
        StartCoroutine(Disappear(buttons[i]));

        if (checker[3] && i == 4)
        {
            StopCoroutine(timerCoroutine);
            CleanUpGame();
            Destroy(gameObject);
            ProcessForPerState(gameType, BlurManager.EndType.MinigameSuccess);

            switch (gameType)
            {
                case GameType.Up:
                    upperSaw.StartRotation();
                    progressManager.AddItem(HandButton.ItemType.Plier);
                    break;
                case GameType.Down:
                    downerSaw.StartRotation();
                    progressManager.AddItem(HandButton.ItemType.Spaner);
                    break;

                case GameType.Switch:
                    areaController.SwitchSaw.SuccessToRemoveBefore();
                    progressManager.AddItem(HandButton.ItemType.SawB);
                    break;
            }
        }
    }

    private void CleanUpGame()
    {
        foreach(var routine in disAppearRoutines)
            if (routine != null)
                StopCoroutine(routine);
    }

    private void ProcessForPerState(GameType gameType, BlurManager.EndType endType)
    {
        switch (gameType)
        {
            case GameType.Up:
                blurManager.CloseBlur(BlurManager.RequestType.RandomTouchGameUp, endType);
                break;
            case GameType.Down:
                blurManager.CloseBlur(BlurManager.RequestType.RandomTouchGameDown, endType);
                break;
            case GameType.Switch:
                blurManager.CloseBlur(BlurManager.RequestType.RandomTouchGameSwitch, endType);
                break;
        }
    }
}
