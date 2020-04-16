using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{//5.3 - 2.5
    private AudioSource bgmNormal;
    private AudioSource bgmTimeAttack;
    private AudioSource shakingSound;

    private AreaController areaController;
    private UIController uIController;

    private bool tracingEnable = false;
    private GameObject character;
    private Camera currentCamera;

    private List<GameObject> firstTracaing = new List<GameObject>();
    private List<GameObject> finalTracing = new List<GameObject>();

    ProgressManager progressManager;


    private void Awake()
    {
        bgmNormal = AudioSetter.SetBgm(gameObject, "Sound/Stage1/UI/Stage1BgmNormal");
        bgmTimeAttack = AudioSetter.SetBgm(gameObject, "Sound/Stage1/UI/Stage1BgmTimeAttack");
        shakingSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/TimeAttack/CameraShakingSound");

        bgmNormal.Play();

        currentCamera = Camera.main;
        character = GameObject.FindGameObjectWithTag("Player");

        GameObject pivot = GameObject.FindGameObjectWithTag("Pivots");

        foreach(Transform var in pivot.transform.GetChild(0))
            firstTracaing.Add(var.gameObject);
        firstTracaing.Add(character);

        foreach (Transform var in pivot.transform.GetChild(1))
            finalTracing.Add(var.gameObject);
        finalTracing.Add(character);

        progressManager = ProgressManager.GetInstance;
        StartCoroutine(FirstTracing());
    }

    private void Start()
    {
        areaController = AreaController.GetInstance;
        uIController = UIController.GetInstance;
    }

    private IEnumerator CameraShaking()
    {
        uIController.SetUIState(false);

        int step = 0;
        bool direction = true;
        Vector3 startOrigin = transform.position;
        Vector3 originPos = finalTracing[0].transform.position;
        tracingEnable = false;
        shakingSound.Play();

        while (step < 20)
        {
            if (step % 2 == 0)
            {
                transform.localPosition = new Vector3(originPos.x + (direction ? 1 : -1), originPos.y, originPos.z);
                direction = !direction;
            }
            else
                transform.localPosition = originPos;

            step += 1;
            yield return new WaitForEndOfFrame();
        }

        transform.position = startOrigin;

        yield return new WaitForSeconds(1.0f);
        StartCoroutine(FinalTracing());
        yield break;
    }

    private IEnumerator FirstTracing()
    {
        float step = 0;
        while (step <= 1)
        {
            transform.localPosition = Vector3.Lerp(firstTracaing[0].transform.localPosition, firstTracaing[1].transform.localPosition, step);
            step += 0.005f;
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(0.5f);

        Vector3 characterNewPos = new Vector3(character.transform.position.x, character.transform.position.y, transform.position.z);
        float currentSize = currentCamera.orthographicSize;
        step = 0;

        while (step <= 1)
        {
            currentCamera.orthographicSize = Mathf.Lerp(currentSize, 2.5f, step);
            TracingForWalking(Vector3.Lerp(firstTracaing[1].transform.position, characterNewPos, step));
            step += 0.01f;
            yield return new WaitForEndOfFrame();
        }

        tracingEnable = true;
        UIController.GetInstance.SetUIState(true);
        ProgressManager.GetInstance.IsReady = true;

        yield break;
    }

    private IEnumerator FinalTracing()
    {

        float step = 0;
        while (step <= 1)
        {

            TracingForWalking(Vector3.Lerp(finalTracing[0].transform.position, finalTracing[1].transform.localPosition, step));
            step += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        areaController.FinalDoor.OpenDoor();
        yield return new WaitForSeconds(2.0f);

        step = 0;
        Vector3 characterNewPos = new Vector3(character.transform.position.x, character.transform.position.y, transform.position.z);

        float currentSize = currentCamera.orthographicSize;

        while (step <= 1)
        {
            currentCamera.orthographicSize = Mathf.Lerp(currentSize, 2.5f, step);
            TracingForWalking(Vector3.Lerp(finalTracing[1].transform.position, characterNewPos, step));
            step += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        tracingEnable = true;
        uIController.SetUIState(true);
        uIController.TimerController.StartTimer(50);
        progressManager.IsTimerAttackStart = true;
        bgmTimeAttack.Play();
        yield break;
    }

    private void TracingForWalking(Vector3 target)
    {
        float height = currentCamera.orthographicSize;
        float width = height * currentCamera.aspect;

        float currentX = target.x, currentY = target.y;
        float compensationX = currentX - width < -9.5f || currentX + width > 9.5f ? (currentX + width > 9.5f ? 9.5f - width : -9.5f + width) : currentX;
        float compensationY = currentY - height < -8.7f ? -8.7f + height : currentY;

        transform.position = new Vector3(compensationX, compensationY, transform.position.z);
    }

    public void Tracing(Vector3 target)
    {
        if (!tracingEnable) return;

        float height = currentCamera.orthographicSize;
        float width = height * currentCamera.aspect;

        float currentX = target.x, currentY = target.y;
        float compensationX = currentX - width < -9.5f || currentX + width > 9.5f ? (currentX + width > 9.5f ? 9.5f - width : -9.5f + width) : currentX;
        float compensationY = currentY - height < -8.5f ? -8.7f + height : currentY;

        transform.position = new Vector3(compensationX, compensationY, transform.position.z);
    }

    public void StartFinalWalking()
    {
        bgmNormal.Stop();
        StartCoroutine(CameraShaking());
    }

    public void StepTimeAttackBgm()
    {
        bgmTimeAttack.Stop();
    }
}
