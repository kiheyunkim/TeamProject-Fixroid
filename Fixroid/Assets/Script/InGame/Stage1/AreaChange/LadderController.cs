using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderController : MonoBehaviour
{
    private List<GameObject> ladderObject = new List<GameObject>();
    private AudioSource ladderDown;

    private void Awake()
    {
        ladderDown = AudioSetter.SetEffect(gameObject, "Sound/Stage1/AreaChange/LadderDown");

        foreach (Transform transform in transform)
            ladderObject.Add(transform.gameObject);
        ladderObject[0].SetActive(false);
    }

    public void ChangeState()
    {
        ladderDown.Play();
        ladderObject[0].SetActive(true);
        ladderObject[1].SetActive(false);
    }

}
