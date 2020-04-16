using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownerSaw : MonoBehaviour
{
    private MiniGameController miniGameController;

    private List<GameObject> saws = new List<GameObject>();
    private List<Vector3> speedValues = new List<Vector3>();
    private readonly Vector3 stdAngle = new Vector3(0, 0, 5);
    private bool rotationEnable = false;

    private void Awake()
    {
        foreach (Transform saw in transform)
            saws.Add(saw.gameObject);

        speedValues.Add(stdAngle * Time.deltaTime * 3f);
        speedValues.Add(-speedValues[0] / 9 * 8);

        miniGameController = GetComponent<MiniGameController>();
    }

	// Update is called once per frame
	void Update ()
    {
        if (!rotationEnable) return;

        for (int i = 0; i < saws.Count; i++)
            saws[i].transform.Rotate(speedValues[i]);
    }

    public void StartRotation()
    {
        miniGameController.CleanUp();
        Destroy(miniGameController);
        rotationEnable = true;
    }
}
