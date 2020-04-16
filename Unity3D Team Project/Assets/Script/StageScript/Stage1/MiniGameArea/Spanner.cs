using UnityEngine;
using System.Collections;

public class Spanner : MonoBehaviour
{
    public AudioSource MessageSound;
    public GameObject Blink;
    private bool GameStart;

    void Start()
    {
        GameStart = false;
    }

    void Update()
    {
        if (TouchMiniGame.Success && GameStart)
        {
            if(MainMenu.EffectValue)
                MessageSound.Play();
            CharacterMoving.MessageDisplay[3] = false;
            Rotation.FirstBarrier = true;
            Rotation.MiniGameSpannerEnd = true;
            Stage1UI.ItemGaining(Stage1UI.Item.Spaner);
            TouchMiniGame.Success = false;
            TouchMiniGame.Fail = false;
            CharacterMoving.EventType = CharacterMoving.MessageType.Spaner_After;
            EventMessage_Time.OnlyEventActivate = true;
            GameStart = false;
            Destroy(Blink);
        }
    }
    void OnMouseDown()
    {
        if (!Stage1UI.ItemOpen && !Stage1UI.PauseOpen)
        {
            TouchMiniGame.Success = false;
            TouchMiniGame.Fail = false;
            TouchMiniGame.RandomButtonGameStart = true;
            GameStart = true;

        }
    }
}
