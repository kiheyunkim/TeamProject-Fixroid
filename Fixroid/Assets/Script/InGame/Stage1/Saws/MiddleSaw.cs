using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiddleSaw : MonoBehaviour
{
    private Sprite sawFilled;

    private List<GameObject> saws = new List<GameObject>();
    private List<GameObject> weights = new List<GameObject>();
    
    private readonly Vector3 stdAngle = new Vector3(0, 0,1 );
    private bool rotationEnable = false;

    private readonly float lowPoint = 4.5f;
    private readonly float HighPoint = 6.45f;
    private readonly float lowPoint2 = 4.8f;
    private readonly float HighPoint2 = 6.85f;
    private bool[] weightUp = new bool[4];
    private readonly Vector3 movingSpeed = new Vector3(0, 0.02f, 0);


    private void Awake()
    {
        foreach (Transform weight in transform.GetChild(0))
            saws.Add(weight.gameObject);
        foreach (Transform weight in transform.GetChild(1))
            weights.Add(weight.gameObject);

        for (int i = 0; i < 4; i++)
            weightUp[i] = (Random.Range(0, 10) % 2 == 0) ? true : false;

        sawFilled = Resources.LoadAll<Sprite>("Stage/Stage1/WeightParts")[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (!rotationEnable) return;

        for (int i = 0; i < saws.Count; i++)
            saws[i].transform.Rotate(stdAngle);

        for (int i = 0; i < weights.Count; i++)
        {

            if (weightUp[i])
                weights[i].transform.Translate(movingSpeed);
            else
                weights[i].transform.Translate(-movingSpeed);

            if (weights[i].transform.localPosition.y > (i != weights.Count - 1 ? HighPoint : HighPoint2))
                weightUp[i] = false;
            else if (weights[i].transform.localPosition.y < (i != weights.Count - 1 ? lowPoint : lowPoint2))
                weightUp[i] = true;
        }
	}

    public void StartRotate()
    {
        rotationEnable = true;
        saws[0].GetComponent<SpriteRenderer>().sprite = sawFilled;
    }
}
