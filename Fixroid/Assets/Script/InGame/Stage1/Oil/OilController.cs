using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilController : MonoBehaviour
{
    private AudioSource dropSound;
    private AudioSource oilSound;

    private GameObject oilPrefab;
    private GameObject instantiateObject;

    private ProgressManager progressManager;
    private HandButton handButton;
    private Coroutine dropCoroutine;

    private OilSoundArea oilSoundArea;

    private void Awake()
    {
        dropSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/OilDropSound");
        oilSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/OilSound");

        oilSoundArea = GetComponentInChildren<OilSoundArea>();

        oilPrefab = Resources.Load<GameObject>("Prefab/Oil");
        dropCoroutine = StartCoroutine(Drops());
    }

    private void Start()
    {
        progressManager = ProgressManager.GetInstance;
        handButton = UIController.GetInstance.HandButton;
    }
    
    private IEnumerator Drops()
    {
        while (true)
        {
            if (instantiateObject == null)
            {
                instantiateObject = Instantiate(oilPrefab, transform.position, Quaternion.identity);
                if(oilSoundArea.IsOilSoudArea)
                    dropSound.Play();
            }

            yield return new WaitForSeconds(3.0f);
        }
    }

    private void OnMouseUp()
    {
        if (progressManager.IsItemWindowOpen) return;
        if (progressManager.IsGamePlaying) return;
        if (!progressManager.IsReady) return;

        if (handButton.CheckItemIsOk(HandButton.ItemType.Spray))
        {
            oilSound.Play();
            progressManager.RemoveItem(HandButton.ItemType.Spray);
            progressManager.AddItem(HandButton.ItemType.FullSpray);
            handButton.ItemRemove();
            StopCoroutine(dropCoroutine);
            if (instantiateObject != null)
                Destroy(instantiateObject);
            Destroy(gameObject);
        }
    }
}
