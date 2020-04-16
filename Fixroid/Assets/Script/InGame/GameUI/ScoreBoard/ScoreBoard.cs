using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public enum ScoreBoardState { fail, Success };
    public ScoreBoardState boardState;

    private UnityEngine.UI.Text timer;
    private float time = 0;

    private Rigidbody2D buttonsGroup;
    private List<UnityEngine.UI.Button> buttons = new List<UnityEngine.UI.Button>();
    private ProgressManager progressManager;
    private SaveManager saveManager;

    private AudioSource buttonSound;
    private AudioSource timerSound;

    private void Awake()
    {
        buttonSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/ScoreBoard/ScoreBoardButtonSound");
        timerSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/ScoreBoard/TimerSound");

        Canvas canvas = GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main.GetComponent<Camera>();

        if (boardState == ScoreBoardState.Success)
            timer = transform.GetChild(2).GetComponentInChildren<UnityEngine.UI.Text>();

        buttonsGroup = transform.GetChild(1).GetComponent<Rigidbody2D>();
        foreach (var bttns in buttonsGroup.GetComponentsInChildren<UnityEngine.UI.Button>())
        {
            buttons.Add(bttns);
            bttns.enabled = false;
        }
        progressManager = ProgressManager.GetInstance;

        if (boardState == ScoreBoardState.Success)
            StartCoroutine(CalculateTime());
        else
        {
            buttonsGroup.bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(GravityControll());
        }
    }

    private void Start()
    {
        saveManager = SaveManager.GetInstance;
    }

    protected IEnumerator CalculateTime()
    {
        while(time < progressManager.time)
        {
            time += Time.deltaTime;
            int minute = (int)time / 60;
            int second = (int)time / 1;
            int millsecond = (int)((time * 100) % 100f);
            timer.text = minute.ToString("D2") + ":" + second.ToString("D2") + ":" + millsecond.ToString("D2");

            if(!timerSound.isPlaying)
                timerSound.Play();
            yield return new WaitForEndOfFrame();
        }

        time = progressManager.time;
        buttonsGroup.bodyType = RigidbodyType2D.Dynamic;
        saveManager.Save(1, time);
        StartCoroutine(GravityControll());
    }

    protected IEnumerator GravityControll()
    {
        int count = 0;
        while(count < 10)
        {
            if (buttonsGroup.velocity == Vector2.zero)
                count++;
            else
                count = 0;

            yield return new WaitForEndOfFrame();
        }

        foreach (var bttns in buttons)
            bttns.enabled = true;
    }

    public void PushRestart()
    {
        if (!buttonSound.isPlaying)
            buttonSound.Play();

        SceneManager.NextSceneNumber = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }

    public void PushReturnMenu()
    {
        if (!buttonSound.isPlaying)
            buttonSound.Play();
        
        SceneManager.NextSceneNumber = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
