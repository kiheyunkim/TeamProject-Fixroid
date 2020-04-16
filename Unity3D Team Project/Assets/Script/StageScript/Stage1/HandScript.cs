using UnityEngine;
using System.Collections;

public class HandScript : MonoBehaviour
{
    //For Sound
    public AudioSource HandClickSound;

    public static Stage1UI.Item EquippedItem;
    //Item Open Option
    public UnityEngine.UI.Image ButtonImage;
    static public bool isNotMatch;
    static public bool Blink;
    private bool isVibrate = false;

    //Hand Texture 
    private bool isOpenItemPage;
    private UnityEngine.UI.SpriteState OpenHandState;
    private UnityEngine.UI.SpriteState CloseHandState;
    private UnityEngine.UI.SpriteState OilSprayState;
    private UnityEngine.UI.SpriteState OilSprayFullState;
    private UnityEngine.UI.SpriteState SpanerState;
    private UnityEngine.UI.SpriteState PlierState;

    //Hand Texture
    public Sprite OpenHand;
    public Sprite CloseHand;
    public Sprite CloseHand_sel;
    
    public Sprite OilSpray;
    public Sprite OilSpray_Sel;
    public Sprite OilSpray_Blink;

    public Sprite Spaner;
    public Sprite Spaner_Sel;
    public Sprite Spaner_Blink;

    public Sprite Pliers;
    public Sprite Pliers_sel;
    public Sprite Pliers_Blink;

    public float time;
	// Update is called once per frame
    void Start()
    {
        EquippedItem = Stage1UI.Item.Basic;
        isVibrate = true;
        time = 0.5f;

        isNotMatch = false;
        OpenHandState.pressedSprite = null;
        CloseHandState.pressedSprite = CloseHand_sel;
        OilSprayState.pressedSprite = OilSpray_Sel;
        SpanerState.pressedSprite = Spaner_Sel;
        PlierState.pressedSprite = Pliers_sel;
    }
    void Update()
    {
        Stage1UI.Equipped = EquippedItem;
        if (isNotMatch)
        {
            if (Blink)
            {
                switch (Stage1UI.Equipped)
                {
                    case Stage1UI.Item.Basic:
                        time = 0.5f;
                        Blink = false;
                        isNotMatch = false;
                        break;

                    case Stage1UI.Item.OilSpray:
                        if (time >= 0)
                        {
                            if (isVibrate)
                                Handheld.Vibrate();
                            isVibrate = false;
                            ButtonImage.sprite = OilSpray_Blink;
                            time -= Time.deltaTime;
                        }
                        else
                        {
                            ButtonImage.sprite = OilSpray;
                            time = 0.5f;
                            Blink = true;
                            isNotMatch = false;
                            isVibrate = true;
                        }
                        break;

                    case Stage1UI.Item.Oil_Full:
                        if (time >= 0)
                        {
                            if (isVibrate)
                                Handheld.Vibrate();
                            isVibrate = false;
                            ButtonImage.sprite = OilSpray_Blink;
                            time -= Time.deltaTime;
                        }
                        else
                        {
                            ButtonImage.sprite = OilSpray;
                            time = 0.5f;
                            Blink = true;
                            isNotMatch = false;
                            isVibrate = true;
                        }
                        break;

                    case Stage1UI.Item.pliers:
                        if (time >= 0)
                        {
                            if (isVibrate)
                                Handheld.Vibrate();
                            isVibrate = false;
                            ButtonImage.sprite = Pliers_Blink;
                            time -= Time.deltaTime;
                        }
                        else
                        {
                            ButtonImage.sprite = Pliers;
                            time = 0.5f;
                            Blink = true;
                            isNotMatch = false;
                            isVibrate = true;
                        }
                        break;

                    case Stage1UI.Item.Spaner:
                        if (time >= 0)
                        {
                            if (isVibrate)
                                Handheld.Vibrate();
                            isVibrate = false;
                            ButtonImage.sprite = Spaner_Blink;
                            time -= Time.deltaTime;
                        }
                        else
                        {
                            ButtonImage.sprite = Spaner;
                            time = 0.5f;
                            Blink = true;
                            isNotMatch = false;
                            isVibrate = true;
                        }
                        break;

                }
            }
        }

        if (Stage1UI.ItemOpen)
        {
            ButtonImage.sprite = OpenHand;
            GetComponent<UnityEngine.UI.Button>().spriteState = OpenHandState;
            return;
        }
        else
        {
            if (!isNotMatch)
            {
                switch (Stage1UI.Equipped)
                {

                    case Stage1UI.Item.Basic:
                        ButtonImage.sprite = CloseHand;
                        GetComponent<UnityEngine.UI.Button>().spriteState = CloseHandState;
                        break;
                    case Stage1UI.Item.OilSpray:
                        ButtonImage.sprite = OilSpray;
                        GetComponent<UnityEngine.UI.Button>().spriteState = OilSprayState;
                        break;
                    case Stage1UI.Item.Oil_Full:
                        ButtonImage.sprite = OilSpray;
                        GetComponent<UnityEngine.UI.Button>().spriteState = OilSprayState;
                        break;
                    case Stage1UI.Item.pliers:
                        ButtonImage.sprite = Pliers;
                        GetComponent<UnityEngine.UI.Button>().spriteState = PlierState;
                        break;
                    case Stage1UI.Item.Spaner:
                        ButtonImage.sprite = Spaner;
                        GetComponent<UnityEngine.UI.Button>().spriteState = SpanerState;
                        break;
                }

            }
        }


    }
    public void OnClick()
    {
        if (MainMenu.EffectValue)
            HandClickSound.Play();
        if(!Stage1UI.ItemOpen)
        {
            if (EquippedItem == Stage1UI.Item.Basic)
            {
                Stage1UI.ItemOpen = true;
            }

            if (EquippedItem != Stage1UI.Item.Basic)
            EquippedItem = Stage1UI.Item.Basic;
            else
            {
                Stage1UI.ItemOpen = true;
            }
        }
        else
        {
            EquippedItem = Stage1UI.Item.Basic;
            Stage1UI.ItemOpen = false;
        }
    }
}
