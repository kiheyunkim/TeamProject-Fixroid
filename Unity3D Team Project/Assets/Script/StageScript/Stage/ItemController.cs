using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour
{
    //For Message Sound
    public AudioSource ItemMessageSound;

    //Boolean for Control Item Get
    public static bool isGet1;
    public static bool isGet2;
    public static bool isGet3;
    public static bool isGet4;
    public static bool isGet5;
    public static bool isGet6;
    public static bool isGet7;

    //Variable for each Items
    public GameObject Item1;
    public GameObject Item2;
    public GameObject Item3;
    public GameObject Item4;
    public GameObject Item5;
    public GameObject Item6;
    public GameObject Item7;

    void Start ()
    {
        isGet1 = false;
        isGet2 = false;
        isGet3 = false;
        isGet4 = false;
        isGet5 = false;
        isGet6 = false;
        isGet7 = false;
    }
    
    void Update()
    {
        if (isGet1)
        {
            if (MainMenu.EffectValue)
                ItemMessageSound.Play();
            Stage1UI.ItemGaining(Stage1UI.Item.ChainSaw1);
            Item1.GetComponent<Animator>();//Animator Setting / off
            CharacterMoving.EventType = CharacterMoving.MessageType.GetSawToothA;
            EventMessage_Time.OnlyEventActivate = true;
            Destroy(Item1);
            isGet1 = false;
        }
        if (isGet2)
        {
            if (MainMenu.EffectValue)
                ItemMessageSound.Play();
            Stage1UI.ItemGaining(Stage1UI.Item.ChainSaw2);//No Use
            Item2.GetComponent<Animator>();//Animator Setting / off
            Destroy(Item2);
            isGet2 = false;
        }
        if (isGet3)
        {
            if (MainMenu.EffectValue)
                ItemMessageSound.Play();
            Stage1UI.ItemGaining(Stage1UI.Item.Nut);
            Item3.GetComponent<Animator>();//Animator Setting / off
            CharacterMoving.EventType = CharacterMoving.MessageType.GetNut;
            EventMessage_Time.OnlyEventActivate = true;
            Destroy(Item3);
            isGet3 = false;
        }
        if (isGet4)
        {
            if (MainMenu.EffectValue)
                ItemMessageSound.Play();
            Stage1UI.ItemGaining(Stage1UI.Item.OilSpray);
            Item4.GetComponent<Animator>();//Animator Setting / off
            CharacterMoving.EventType = CharacterMoving.MessageType.GetOilSpray;
            EventMessage_Time.OnlyEventActivate = true;
            Destroy(Item4);
            isGet4 = false;
        }
        if (isGet5)
        {
            Stage1UI.ItemGaining(Stage1UI.Item.pliers);//No Use
            Item5.GetComponent<Animator>();//Animator Setting / off
            Destroy(Item5);
            isGet5 = false;
        }
        if(isGet6)
        {
            Stage1UI.ItemGaining(Stage1UI.Item.Spaner);//No Use
            Item6.GetComponent<Animator>();//Animator Setting / off
            Destroy(Item6);
            isGet6 = false;
        }
	}
}
