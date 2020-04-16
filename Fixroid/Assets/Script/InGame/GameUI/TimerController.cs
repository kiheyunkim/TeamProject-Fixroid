using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerController : MonoBehaviour
{
    static public TimerController GetInstance { get; private set; }
    
    private const int highPoint = 0;
    private const int lowPoint = -106;
    private bool isTimerStart = false;
    private float time;

    private UnityEngine.UI.Image phrasesImage;
    private UnityEngine.UI.Text timerTime;
    private Coroutine timerMove;

    protected IEnumerator Appearing()
    {
        while (transform.localPosition.y > lowPoint)
        {
            Vector3 currentPos = transform.localPosition;
            currentPos.y -= 2;

            transform.localPosition = currentPos;
            yield return new WaitForEndOfFrame();
        }
    }

    private void Awake()
    {
        phrasesImage = GetComponent<UnityEngine.UI.Image>();
        timerTime = GetComponentInChildren<UnityEngine.UI.Text>();
        phrasesImage.enabled = false;

        GetInstance = this;
    }

    private void Update()
    {
        if (!isTimerStart) return;

        if (time >= 0)
            time -= Time.deltaTime;
        else
        {
            time = 0;
            UIController.GetInstance.BlurManager.StartBlur(BlurManager.RequestType.GameFail);
            isTimerStart = false;
        }
        int minute = (int)time / 60;
        int second = (int)time / 1;
        int millsecond = (int)((time * 100) % 100f);
        timerTime.text = minute.ToString("D2") + ":" + second.ToString("D2") + ":" + millsecond.ToString("D2");
    }

    public void StartTimer(int setTimer)
    {
        transform.localPosition = new Vector3(0, highPoint, 0);
        phrasesImage.enabled = true;

        if (timerMove != null)
            StopCoroutine(timerMove);
        timerMove = StartCoroutine(Appearing());
        isTimerStart = true;
        time = setTimer;
    }
    
    public void PauseTimer()
    {
        isTimerStart = false;
    }

    public void RestartTimer()
    {
        isTimerStart = true;
    }


    public void StopTimer()
    {
        isTimerStart = false;
        ProgressManager.GetInstance.time = time;
    }
}
