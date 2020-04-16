using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuckooController : MonoBehaviour
{
    private AudioSource doorOpenSound;

    private enum State { None, Opened, Ended };
    private State state = State.None;

    private GameObject closedObject;
    private GameObject openObject;
    private List<GameObject> openObjects = new List<GameObject>();

    private bool stateA = false;
    private bool stateB = false;

    private HandButton handButton;
    private BlurManager blurManager;
    private ProgressManager progressManager;

    private void Awake()
    {
        doorOpenSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/OilSound");

        closedObject = transform.GetChild(0).gameObject;
        openObject = transform.GetChild(1).gameObject;
        foreach (Transform var in openObject.transform)
            openObjects.Add(var.gameObject);
        openObjects[2].SetActive(false);
        openObject.SetActive(false);
    }

    private void Start()
    {
        handButton = UIController.GetInstance.HandButton;
        blurManager = UIController.GetInstance.BlurManager;
        progressManager = ProgressManager.GetInstance;
    }

    public void OpenDoor(bool a, bool b)
    {
        if (a)
            stateA = true;
        if (b)
            stateB = true;

        if (stateA && stateB)
        {
            Destroy(closedObject);
            state = State.Opened;
            openObject.SetActive(true);
            doorOpenSound.Play();
        }
    }

    public void ChangeFixState()
    {
        openObjects[1].SetActive(false);
        openObjects[2].SetActive(true);
    }

    public void OnMouseUp()
    {
        if (state == State.Opened)
        {
            if (!progressManager.IsReady) return;
            
            if (progressManager.IsGamePlaying) return;

            if (progressManager.IsItemWindowOpen) return;

            if (handButton.CheckItemIsOk(HandButton.ItemType.Plier))
            {
                blurManager.StartBlur(BlurManager.RequestType.Spring);
                progressManager.IsGamePlaying = true;
            }
        }
    }
}
