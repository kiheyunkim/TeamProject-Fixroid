using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainMoving : MonoBehaviour
{
    private float min = 5.2f;
    private float max = 7.3f;

    private void Awake()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Move(bool direction)
    {
        if(direction)
        {
            float step = transform.position.x;
            while (step <= max)
            {
                transform.position = new Vector3(step, transform.position.y, transform.position.z);
                step += 0.05f;
                yield return new WaitForEndOfFrame();
            }

        }
        else
        {
            float step = transform.position.x;
            while (step >= min)
            {
                transform.position = new Vector3(step, transform.position.y, transform.position.z);
                step -= 0.05f;
                yield return new WaitForEndOfFrame();
            }
        }
    }

    private IEnumerator Moving()
    {
        while(true)
        {
            yield return StartCoroutine(Move(true));
            yield return new WaitForSeconds(2.0f);
            yield return StartCoroutine(Move(false));
            yield return new WaitForSeconds(2.0f);
        }
    }
}
