using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchSaw2 : MonoBehaviour
{
    private AudioSource sawSound;

    private GameObject after;
    private bool isRotate = false;
    private ProgressManager progressManager;
    private CuckooController cuckooController;

    private Vector3 speed = new Vector3(0, 0, 1f);

    private void Awake()
    {
        sawSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/OilSound");
        after = transform.GetChild(1).gameObject;
        after.SetActive(false);
    }

    // Use this for initialization
    void Start ()
    {
        progressManager = ProgressManager.GetInstance;
        cuckooController = AreaController.GetInstance.CuckooController;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (!isRotate) return;

        after.transform.Rotate(speed);
	}

    private void OnMouseUp()
    {
        if (!progressManager.IsReady) return;

        if (progressManager.IsItemWindowOpen) return;

        if (progressManager.IsGamePlaying) return;

        if (progressManager.CheckItem(HandButton.ItemType.SawB))
        {
            sawSound.Play();
            progressManager.RemoveItem(HandButton.ItemType.SawB);
            after.SetActive(true);
            isRotate = true;
            cuckooController.OpenDoor(false, true);
        }
    }
}
