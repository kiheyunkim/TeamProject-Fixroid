using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StartTouch : MonoBehaviour
{
    public GameObject ClickSound;
    void OnMouseDown()
    {
        if(StartScript.isEnd)
        {
            ClickSound.GetComponent<AudioSource>().Play();
            StartScript.isTouched = true;
            StartScript.isEnd = false;
        }
    }
} 