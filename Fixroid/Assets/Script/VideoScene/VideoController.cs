using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoController : MonoBehaviour
{
    private UnityEngine.UI.Text text;
    private UnityEngine.Video.VideoPlayer videoPlayer;
    private bool touchEnable;

    private IEnumerator EnableMouseInput(int endTime)
    {
        int counter = 0;
        while (true)
        {
            if (endTime - 1 < counter)
            {
                StartCoroutine(Appearing());
                touchEnable = true;
                yield break;
            }

            counter++;
            yield return new WaitForSecondsRealtime(1.0f);
        }
    }
    private IEnumerator Appearing()
    {
        float alpha = 0;
        while (true)
        {
            alpha += 0.01f;

            if (alpha > 1)
            {
                text.color = new Color(1, 1, 1, 1);
                yield break;
            }

            text.color = new Color(1, 1, 1, alpha);
            yield return new WaitForSeconds(0.01f);
        }
    }

    // Use this for initialization
    void Start ()
    {
        text = GameObject.Find("Text").GetComponent<UnityEngine.UI.Text>();
        videoPlayer = GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.started += MovieStart;
        videoPlayer.loopPointReached += MovieEnd;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!touchEnable) return;

        if(Input.touchCount !=0)
        {
            videoPlayer.Stop();
            UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
        }
	}

    void MovieStart(UnityEngine.Video.VideoPlayer vp)
    {
        StartCoroutine(EnableMouseInput(10));
    }

    void MovieEnd(UnityEngine.Video.VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Start");
    }
}
