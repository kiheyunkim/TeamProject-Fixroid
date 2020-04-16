using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAnimationControl : MonoBehaviour
{
    private AudioSource downSound;
    private AudioSource spreadSound;

    private List<UnityEngine.UI.Image> images = new List<UnityEngine.UI.Image>();
    private UnityEngine.UI.Text text;

    private void Awake()
    {
        downSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/FirstAnimation/StageAnimDown");
        spreadSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/FirstAnimation/StageAnimSpread");

        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main.GetComponent<Camera>();

        foreach (var image in transform.GetComponentsInChildren<UnityEngine.UI.Image>())
        {
            image.color = new Color(1, 1, 1, 0);
            images.Add(image);
        }
        text = images[1].GetComponentInChildren<UnityEngine.UI.Text>();
        text.color = Color.clear;


        StartCoroutine(FirstStep());
    }

    private IEnumerator FirstStep()
    {
        downSound.Play();
        images[0].color = Color.white;
        float max = 360, min = -220;
        float pos = max;

        while (pos > min)
        {
            pos -= 10.0f;
            images[0].transform.localPosition = new Vector3(0, pos, 0);
            yield return new WaitForEndOfFrame();
        }

        images[0].color = Color.clear; 
        StartCoroutine(SecondStep());
    }

    private IEnumerator SecondStep()
    {
        spreadSound.Play();
        float step = 0;
        images[1].color = Color.white;

        while (step<1)
        {
            step += 0.05f;
            images[1].transform.localScale = new Vector3(step, 1, 1);
            yield return new WaitForEndOfFrame();
        }

        StartCoroutine(ThirdStep());
    }

    private IEnumerator ThirdStep()
    {
        float step = 0;

        while (step < 1)
        {
            step += 0.05f;
            text.color = new Color(1, 1, 1, step);
            yield return new WaitForEndOfFrame();
        }

        text.color = Color.white;
        yield return new WaitForSeconds(2.0f);

        Destroy(gameObject);
        yield break;
    }
}
