using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleRotation : MonoBehaviour
{
    private GameObject beforeHandle;
    private GameObject afterHandle;
    private GameObject currentHandle;
    private GameObject nut;

    private Coroutine brokenMoving;
    private readonly Vector3 rotationSpeed = new Vector3(0, 0, 1.0f);
    private bool isEnded = false;

    private void Awake()
    {
        beforeHandle = transform.GetChild(0).gameObject;
        afterHandle = transform.GetChild(1).gameObject;
        nut = transform.GetChild(2).gameObject;
        afterHandle.SetActive(false);
        currentHandle = beforeHandle;

        brokenMoving = StartCoroutine(Rotate());
    }
    private void Update()
    {
        if (!isEnded) return;

        currentHandle.transform.Rotate(rotationSpeed);
    }

    protected IEnumerator Rotate()
    {
        while(true)
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
            currentHandle.transform.Rotate(rotation ? rotationSpeed : -rotationSpeed);
            yield return new WaitForEndOfFrame();
            i++;
        }
    }

    public void HandleGameEnd()
    {
        StopCoroutine(brokenMoving);
        Destroy(beforeHandle);
        Destroy(nut);
        afterHandle.SetActive(true);
        currentHandle = afterHandle;
        isEnded = true;
    }
}
