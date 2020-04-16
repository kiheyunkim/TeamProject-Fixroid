using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSpray : MonoBehaviour
{
    private AudioSource oilSound;

    private PhrasesController phrasesController;
    private ProgressManager progressManager;
    private HandButton handButton;
    private bool isOk = false;

    private readonly Vector3 rotationSpeed = new Vector3(0, 0, 1.0f);
    private Coroutine brokenMoving;
    private GameObject before;
    private GameObject after;

    private void Awake()
    {
        oilSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/OilSound");
        before = transform.GetChild(0).gameObject;
        after = transform.GetChild(1).gameObject;
        after.SetActive(false);
    }

    void Start ()
    {
        handButton = UIController.GetInstance.HandButton;
        brokenMoving = StartCoroutine(Rotate());
        phrasesController = PhrasesController.GetInstance;
        progressManager = ProgressManager.GetInstance;
    }
	
	void Update ()
    {
        if (!isOk) return;

        after.transform.Rotate(rotationSpeed);
    }

    protected IEnumerator Rotate()
    {
        while (true)
        {
            StartCoroutine(SubRoate(true));
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(SubRoate(false));
            yield return new WaitForSeconds(2.0f);
        }
    }

    protected IEnumerator SubRoate(bool rotation)
    {
        int i = 0;
        while (i < 10)
        {
            before.transform.Rotate(rotation ? rotationSpeed : -rotationSpeed);
            yield return new WaitForEndOfFrame();
            i++;
        }
    }

    private void OnMouseUp()
    {
        if (!progressManager.IsReady) return;

        if (isOk) return;

        if (progressManager.IsItemWindowOpen) return;

        if(handButton.CheckItemIsOk(HandButton.ItemType.FullSpray))
        {
            oilSound.Play();
            AreaController.GetInstance.TerrainChange.ChangeState();
            StopCoroutine(brokenMoving);
            before.SetActive(false);
            after.SetActive(true);
            isOk = true;
            phrasesController.StartPhrase(PhrasesController.MiniGameEndNotify.Spray);
        }
    }

}
