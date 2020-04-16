using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemPauseMenu : MonoBehaviour
{
    private AudioSource buttonSound;

    private void Awake()
    {
        buttonSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/PuaseMenu/PauseRestartButtonSound"); 
    }

    public void Restart()
    {
        buttonSound.Play();
        SceneManager.NextSceneNumber = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }

    public void GoToMain()
    {
        buttonSound.Play();
        SceneManager.NextSceneNumber = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene("Loading");
    }
}
