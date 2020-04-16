using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainButton1 : MonoBehaviour
{
    private AudioSource clickSound;
    public Sprite SpriteClicked;
    public Sprite SpriteUped;
    public GameObject MiniRobot;

    private float time;
    private bool clicked;
    private MainMenuManager MenuMgr;

    void Awake()
    {
        clickSound = AudioSetter.SetEffect(gameObject, "Sound/MainMenu/StageClick");
        MenuMgr = Camera.main.GetComponent<MainMenuManager>();
        time = 1;
        clicked = false;
    }

	void Update ()
    {
        if (!clicked) return;

        if(time>=0)
        {
            time -= Time.deltaTime;
            return;
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = SpriteUped;
        SceneManager.NextSceneNumber = 1;
        MiniRobot.GetComponent<Animator>().SetTrigger("Continue");

    }

    void OnMouseDown()
    {
        if(!MenuMgr.IsExitMenuOpen)
        {
            clickSound.Play();
            clicked = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = SpriteClicked;
        }
    }
}
