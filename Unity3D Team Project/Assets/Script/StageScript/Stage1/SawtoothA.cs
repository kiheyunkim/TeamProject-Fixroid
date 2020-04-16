using UnityEngine;
using System.Collections;

public class SawtoothA : MonoBehaviour
{
    //For Sound
    public AudioSource ItemMessageSound;
    public AudioSource MessageSound;
    public AudioSource NoItemSound;

    //For Object
    public GameObject TargetSawTooth;
    public Sprite AfterSawTooth;

    public bool AllEnd;
    public bool Get;
    private bool ItemGet;
    private bool Itemchange;
    private bool MiniGameEnd;
    private bool Rotation;
    private float time;

    public GameObject Collider1;
    public GameObject Collider2;
    public GameObject Collider3;

    // Use this for initialization
    void Start()
    {
        Get = false;
        ItemGet = false;
        Itemchange = false;
        MiniGameEnd = false;
        AllEnd = false;
        time = 2.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (TouchMiniGame.Success && MiniGameEnd)
        {
            if (MainMenu.EffectValue)
                ItemMessageSound.Play();
            CharacterMoving.EventType = CharacterMoving.MessageType.GetSawToothB;
            EventMessage_Time.OnlyEventActivate = true;
            
            Get = true;
            Collider1.GetComponent<CircleCollider2D>().enabled = true;
            Collider2.GetComponent<CircleCollider2D>().enabled = true;
            if (!Plier.PlierEnd)
                Collider3.GetComponent<BoxCollider2D>().enabled = true;
            if(time>=0)
            {
                time -= Time.deltaTime;
            }
            else
            {
                MiniGameEnd = false;
                if (MainMenu.EffectValue)
                    MessageSound.Play();
                CharacterMoving.EventType = CharacterMoving.MessageType.SawToothB_Before;
                EventMessage_Time.OnlyEventActivate = true;
                TouchMiniGame.Success = false;
                ItemGet = true;
            }

        }

        if(TouchMiniGame.Fail && MiniGameEnd)
        {
            Collider1.GetComponent<CircleCollider2D>().enabled = true;
            Collider2.GetComponent<CircleCollider2D>().enabled = true;
            if(!Plier.PlierEnd)
                Collider3.GetComponent<BoxCollider2D>().enabled = true;
            TouchMiniGame.Fail = false;
        }

        if (ItemGet)
        {
            Stage1UI.ItemGaining(Stage1UI.Item.ChainSaw2);
            TargetSawTooth.GetComponent<SpriteRenderer>().sprite = null;
            ItemGet = false;
        }
        if (Itemchange)
        {
            CuckooControl.SawA = true;
            Stage1UI.DeleteItem(Stage1UI.Item.ChainSaw1);
            TargetSawTooth.GetComponent<SpriteRenderer>().sprite = AfterSawTooth;
            Rotation = true;
            Itemchange = false;
            TargetSawTooth.GetComponent<PolygonCollider2D>().enabled = false;
        }
        if(Rotation)
        {
            TargetSawTooth.transform.Rotate(0, 0, Time.deltaTime * 30f);
        }
    }
    void OnMouseDown()
    {
        if (!Get && !AllEnd)
        {
            TouchMiniGame.RandomButtonGameStart = true;
            Collider1.GetComponent<CircleCollider2D>().enabled = false;
            Collider2.GetComponent<CircleCollider2D>().enabled = false;
            if (!Plier.PlierEnd)
                Collider3.GetComponent<BoxCollider2D>().enabled = false;
            MiniGameEnd = true;
            return;
        }

        if (Get && !AllEnd)
            if (Stage1UI.FindItem(Stage1UI.Item.ChainSaw1))
            {
                if (MainMenu.EffectValue)
                {
                    NoItemSound.Play();
                }
                Itemchange = true;
                AllEnd = true;
                return;
            }
    }
}
