using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainChange : MonoBehaviour
{
    Vector3 originPos;

    private void Awake()
    {
        originPos = transform.position;
        transform.Translate(new Vector3(10f, 0, 0));
    }

    public void ChangeState()
    {
        transform.position = originPos;
    }

}
