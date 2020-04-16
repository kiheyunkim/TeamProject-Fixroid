using UnityEngine;
using System.Collections;

public class Plier : MonoBehaviour
{
    //For Sound
    public AudioSource ItemMessageSound;

    public GameObject Blink;
    public GameObject Collider1;
    public GameObject Collider2;
    public GameObject Collider3;

    private bool GameStart;
    static public bool PlierEnd;
    void Start()
    {
        GameStart = false;
        PlierEnd = false;
    }

    void Update()
    {
        if (TouchMiniGame.Success && GameStart)
        {
            if (MainMenu.EffectValue)
                ItemMessageSound.Play();
            PlierEnd = true;
            Destroy(Blink);
            Rotation.Set2_Second = true;
            TouchMiniGame.Fail = false;
            CharacterMoving.EventType = CharacterMoving.MessageType.GetPlier;
            EventMessage_Time.OnlyEventActivate = true;
            Stage1UI.ItemGaining(Stage1UI.Item.pliers);
            TouchMiniGame.Success = false;
            GameStart = false;
            Collider1.GetComponent<CircleCollider2D>().enabled = true;
            Collider2.GetComponent<CircleCollider2D>().enabled = true;
            Collider3.GetComponent<BoxCollider2D>().enabled = true;
        }
        if(TouchMiniGame.Fail&&GameStart)
        {
            TouchMiniGame.Fail = false;
            GameStart = false;
            Collider1.GetComponent<CircleCollider2D>().enabled = true;
            Collider2.GetComponent<CircleCollider2D>().enabled = true;
            Collider3.GetComponent<BoxCollider2D>().enabled = true;
        }
    }
    void OnMouseDown()
    {
        if (!Stage1UI.ItemOpen && !Stage1UI.PauseOpen)
        {
            if (CharacterMoving.PlierArea)
            {
                TouchMiniGame.Success = false;
                TouchMiniGame.Fail = false;
                TouchMiniGame.RandomButtonGameStart = true;
                GameStart = true;
                Collider1.GetComponent<CircleCollider2D>().enabled = false;
                Collider2.GetComponent<CircleCollider2D>().enabled = false;
                Collider3.GetComponent<BoxCollider2D>().enabled = false;
            }

        }
    }
}
