using UnityEngine;
using System.Collections;

public class Loading : MonoBehaviour
{
    static public int NextSceneNumber;
    public UnityEngine.UI.Text PercentText;
    public UnityEngine.UI.Image PercentImage;

    private AsyncOperation Async;
    private int Percent_int = 0;
    private float Percent = 0.0f;
    private bool isLoadGame = false;
    public IEnumerator StartLoad(string strSceneName)
    {
        if (!isLoadGame)
            isLoadGame = true;

        AsyncOperation Async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(strSceneName);

        while (!Async.isDone)
        {
            float p = Async.progress * 100f;
            Percent = p;
            yield return true;
        }
    }
    // Use this for initialization
    void Start()
    {
        Time.timeScale = 1;
        if (NextSceneNumber == 0)
            StartCoroutine("StartLoad", "MainMenu");
        if (NextSceneNumber == 1)
            StartCoroutine("StartLoad", "Stage1");
        if (NextSceneNumber == 2)
            StartCoroutine("StartLoad", "Stage2");
        if (NextSceneNumber == 3)
            StartCoroutine("StartLoad", "Stage3");
        if (NextSceneNumber == 4)
            StartCoroutine("StartLoad", "Stage4");
        if (NextSceneNumber == 5)
            StartCoroutine("StartLoad", "Option");
        if (NextSceneNumber == 6)
            StartCoroutine("StartLoad", "Acheievement");
        if (NextSceneNumber == 7)
            StartCoroutine("StartLoad", "Credit");
    }

	// Update is called once per frame
	void Update ()
    {
       
        Percent_int = (int)(Percent / 90*100);
        PercentText.text = Percent_int.ToString("D3") + "%";
        PercentImage.fillAmount = Percent / 100;
	}
}
