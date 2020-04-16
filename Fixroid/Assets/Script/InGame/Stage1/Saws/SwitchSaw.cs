using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSaw : MonoBehaviour
{
    private AudioSource sawSound;

    private BlurManager blurManager;
    private ProgressManager progressManager;
    private CuckooController cuckooController;

    private enum SwitchState { None, RemoveBefore, AddAfter }
    private SwitchState state;
    private GameObject before;
    private GameObject after;

    private Vector3 speed = new Vector3(0, 0, 3f);
    private bool isRotate = true;

    private void Awake()
    {
        sawSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/OilSound");
        before = transform.GetChild(1).gameObject;
        after = transform.GetChild(2).gameObject;
        after.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        blurManager = UIController.GetInstance.BlurManager;
        progressManager = ProgressManager.GetInstance;
        cuckooController = AreaController.GetInstance.CuckooController;
    }

    // Update is called once per frame
    void Update ()
    {
        if (!isRotate) return;

        switch (state)
        {
            case SwitchState.None:
                before.transform.Rotate(speed * 10);
                break;
            case SwitchState.AddAfter:
                after.transform.Rotate(speed);
                break;
        }
	}

    public void OnMouseUp()
    {
        if (!progressManager.IsReady) return;
        if (progressManager.IsGamePlaying) return;
        if (progressManager.IsItemWindowOpen) return;

        if (state == SwitchState.AddAfter) return;

        switch (state)
        {
            case SwitchState.None:
                blurManager.StartBlur(BlurManager.RequestType.RandomTouchGameSwitch);
                progressManager.IsGamePlaying = true;
                break;

            case SwitchState.RemoveBefore:
                if (progressManager.CheckItem(HandButton.ItemType.SawA) && !progressManager.IsSawBGet)
                {
                    sawSound.Play();
                    isRotate = true;
                    after.SetActive(true);
                    state = SwitchState.AddAfter;
                    progressManager.RemoveItem(HandButton.ItemType.SawA);
                    cuckooController.OpenDoor(true, false);
                }
                break;
        }
    }

    public void SuccessToRemoveBefore()
    {
        isRotate = false;
        before.SetActive(false);
        state = SwitchState.RemoveBefore;
    }
}
