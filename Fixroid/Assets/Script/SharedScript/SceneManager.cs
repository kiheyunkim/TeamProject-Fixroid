 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    private AsyncOperation loadScene;
    private GameObject percentCanvas;
    private float percent = 0;

    static public int NextSceneNumber;
    
    void Awake()
    {
        percentCanvas = GameObject.Find("Canvas");
    }

    private IEnumerator LoadScene(string sceneName)
    {   
        loadScene = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
        do
        {
            percent = loadScene.progress;
            yield return true;
        }
        while (!loadScene.isDone);
        yield return null;
    }
    
    void Start()
    {
        if (NextSceneNumber == 0) StartCoroutine("LoadScene", "MainMenu");
        if (NextSceneNumber == 1) StartCoroutine("LoadScene", "Stage1");
        if (NextSceneNumber == 2) StartCoroutine("LoadScene", "Stage2");
        if (NextSceneNumber == 3) StartCoroutine("LoadScene", "Stage3");
        if (NextSceneNumber == 4) StartCoroutine("LoadScene", "Stage4");
        if (NextSceneNumber == 5) StartCoroutine("LoadScene", "Option");
        if (NextSceneNumber == 6) StartCoroutine("LoadScene", "Acheievement");
        if (NextSceneNumber == 7) StartCoroutine("LoadScene", "Credit");
    }

    void Update()
    {
        percentCanvas.GetComponentInChildren<UnityEngine.UI.Text>().text = ((int)(percent * 100)).ToString("D3") + "%";
        percentCanvas.GetComponentInChildren<UnityEngine.UI.Image>().fillAmount = percent;
    }
}