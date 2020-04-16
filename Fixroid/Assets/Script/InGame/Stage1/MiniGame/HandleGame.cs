using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleGame : MonoBehaviour
{
    private GameObject handle;
    private GameObject background;
    private GameObject guideLine;

    private GameObject nut;
    private GameObject nutImage;
    private GameObject spaner;
    private GameObject spanerImage;

    private AudioSource handleSound;
    private AudioSource nutStopSound;
    private AudioSource spanerSound;

    private bool isStarted = false;
    private int successCount = 0;
    const int max = 390;

    private bool spanerDirection = true;
    private Vector3 spanerSpeed = new Vector3(0.09f, 0, 0);

    private BlurManager blurManager;
    private AreaController areaController;
    private ProgressManager progressManager;

    private void Awake()
    {
        handleSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/HandleGame/HandleRotationSound");
        nutStopSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/HandleGame/NutStopSound");
        spanerSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/HandleGame/SpanerSound");

        List<GameObject> gameObjects = new List<GameObject>();
        foreach (Transform var in transform)
            gameObjects.Add(var.gameObject);

        handle = gameObjects[0];
        background = gameObjects[1];
        guideLine = gameObjects[2];
        nut = gameObjects[3];
        nutImage = nut.transform.GetChild(0).gameObject;
        spaner = gameObjects[4];
        spanerImage = spaner.transform.GetChild(0).gameObject;

        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main.GetComponent<Camera>();
        progressManager = ProgressManager.GetInstance;

        StartCoroutine(Collocate());
    }

    private void Start()
    {
        blurManager = UIController.GetInstance.BlurManager;
        areaController = AreaController.GetInstance;
    }

    private IEnumerator Collocate()
    {
        spaner.transform.localPosition = Vector3.zero;

        bool direction = Random.Range(0, 3) % 2 == 0 ? true : false;
        float randomTimer = Random.Range(0.1f, 1.5f);
        Vector3 speed = new Vector3(0.1f, 0, 0);
        float timer = 0.0f;

        while (timer < randomTimer)
        {
            if (nut.transform.localPosition.x > max)
                direction = false;
            else if (nut.transform.localPosition.x < -max)
                direction = true;

            nut.transform.Translate((direction ? speed : -speed), Space.Self);

            timer += 0.01f;
            yield return new WaitForSeconds(0.01f);
        }

        nutStopSound.Play();
        isStarted = true;

        yield break;
    }

    private IEnumerator Rotate(bool isEnd)
    {
        Vector3 speed = new Vector3(0, 0, 1);
        float step = 0;

        spanerSound.Play();
        while (step <= 30)
        {
            nutImage.transform.Rotate(speed * 3);
            spanerImage.transform.Rotate(speed * 3);
            step += 1;
            yield return new WaitForEndOfFrame();
        }

        while (step >= 0)
        {
            spanerImage.transform.Rotate(-speed * 3);
            step -= 1;
            yield return new WaitForEndOfFrame();
        }

        if(isEnd)
        {
            yield return StartCoroutine(FinalRotate());
            AreaController.GetInstance.HandleRotation.HandleGameEnd();
            blurManager.CloseBlur(BlurManager.RequestType.HandleGame, BlurManager.EndType.MinigameSuccess);
            progressManager.HandleGameEnd = true;
            areaController.LadderController.ChangeState();
            Destroy(gameObject);
        }
        else
            StartCoroutine(Collocate());
    }

    private IEnumerator FinalRotate()
    {
        Destroy(background);
        Destroy(guideLine);
        Destroy(nut);
        Destroy(spaner);

        handleSound.Play();

        int step = 0;
        while (step <= 100)
        {
            handle.transform.Rotate(new Vector3(0, 0, 1));
            step += 1;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void Update()
    {
        if (!isStarted) return;

        if (spaner.transform.localPosition.x > max)
            spanerDirection = false;
        else if (spaner.transform.localPosition.x < -max)
            spanerDirection = true;

        spaner.transform.Translate((spanerDirection ? spanerSpeed : -spanerSpeed), Space.Self);
    }


    public void PushButton()
    {
        if (!isStarted) return;
        isStarted = false;
        spanerSpeed = Vector3.zero;

        float spanerPos = spaner.transform.localPosition.x;
        float nutPos = nut.transform.localPosition.x;

        if (spanerPos - 70 < nutPos && spanerPos + 25 > nutPos)
        {
            spanerSpeed = new Vector3(0.09f, 0, 0);
            successCount++;

            if (successCount < 3)
                StartCoroutine(Rotate(false));
            else
                StartCoroutine(Rotate(true));
        }
        else
        {
            Destroy(gameObject);
            blurManager.CloseBlur(BlurManager.RequestType.HandleGame, BlurManager.EndType.MinigameFail);
        }
    }
}
