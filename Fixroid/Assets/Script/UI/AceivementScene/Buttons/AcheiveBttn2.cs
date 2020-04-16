using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcheiveBttn2 : MonoBehaviour
{
    //Sprites
    private Sprite lockedUp;
    private Sprite lockedDown;
    private Sprite unLockedUp;
    private Sprite unLockedDown;

    //Sound
    private AudioSource clickSound;

    //AceiveMgr
    private AcheiveManager acheiveMgr;
    private SaveManager saveManager;

    private Sprite bttnNormal;
    private Sprite bttnActive;

    private float time = 0.5f;
    private bool clicked = false;

    void Awake()
    {
        clickSound = AudioSetter.SetEffect(gameObject, "Sound/Acheievement/AcheieveBttn");
    }

    void Start()
    {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Acheivement/AceiveIntegrate");

        acheiveMgr = Camera.main.GetComponentInChildren<AcheiveManager>();
        saveManager = SaveManager.GetInstance;

        if (saveManager.SaveTile.acheiveOpen[2])
        {
            unLockedUp = sprites[26];
            unLockedDown = sprites[27];
            bttnNormal = unLockedUp;
            bttnActive = unLockedDown;
        }
        else
        {
            lockedUp = sprites[10]; 
            lockedDown = sprites[14];
            bttnNormal = lockedUp;
            bttnActive = lockedDown;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (clicked)
        {
            if (time >= 0)
            {
                time -= Time.deltaTime;
                return;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = bttnNormal;
                time = 0.5f;
                acheiveMgr.GetComponent<AcheiveManager>().StartPopup();
                clicked = false;
            }
        }
        else
            gameObject.GetComponent<SpriteRenderer>().sprite = bttnNormal;
    }

    void OnMouseDown()
    {
        if (!acheiveMgr.GetComponent<AcheiveManager>().IsPopUpOpen &&  saveManager.SaveTile.acheiveOpen[2])
        {
            clickSound.Play();
            clicked = true;
            gameObject.GetComponent<SpriteRenderer>().sprite = bttnActive;
        }
    }
}
