using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandButton : MonoBehaviour
{
    public struct Item
    {
        public ItemType itemType;
        public bool isUsable;
    };

    public enum ItemType { Spray, FullSpray, Spaner, Plier, Nut, SawA, SawB, Normal, Open };      //None부터는 Sprite 이용안함  / Normal = Empty
    public bool OpenState { get; set; }

    private ProgressManager progressManager;
    private ItemWindow itemWindow;
    private GameObject itemWindowObject;

    private UnityEngine.UI.Image Image;
    private UnityEngine.UI.Button button;
    private Sprite errorImage;
    private Coroutine handCoroutine;
    private Sprite tempSpriteForCoroutine;

    private List<Sprite> spray = new List<Sprite>();
    private List<Sprite> openHand = new List<Sprite>();
    private List<Sprite> spaner = new List<Sprite>();
    private List<Sprite> closeHand = new List<Sprite>();
    private List<Sprite> plier = new List<Sprite>();

    private AudioSource buttonClickSound;

    private void Awake()
    {
        buttonClickSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/GUI/HandButtonClick");

        itemWindow = GetComponentInChildren<ItemWindow>();
        progressManager = ProgressManager.GetInstance;
        itemWindowObject = itemWindow.gameObject;

        Image = GetComponent<UnityEngine.UI.Image>();
        button = GetComponent<UnityEngine.UI.Button>();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/HandItem");
        for (int i = 0; i < 3; i++) spray.Add(sprites[i]);
        for (int i = 3; i < 5; i++) closeHand.Add(sprites[i]);
        for (int i = 5; i < 8; i++) spaner.Add(sprites[i]);
        for (int i = 8; i < 10; i++) openHand.Add(sprites[i]);
        for (int i = 10; i < 13; i++) plier.Add(sprites[i]);

        OpenState = false;
    }
    
    private IEnumerator ErrorCoroutine()
    {
        tempSpriteForCoroutine = Image.sprite;
        Image.sprite = errorImage;
        Handheld.Vibrate();

        yield return new WaitForSeconds(0.3f);

        Image.sprite = tempSpriteForCoroutine;
        yield break;
    }

    public void SetItemBttn(ItemType itemHandType)
    {
        switch (itemHandType)
        {
            case ItemType.Open:
                Image.sprite = openHand[0];
                button.spriteState = new UnityEngine.UI.SpriteState { pressedSprite = openHand[1] };
                break;

            case ItemType.Spray:
            case ItemType.FullSpray:
                Image.sprite = spray[0];
                button.spriteState = new UnityEngine.UI.SpriteState { pressedSprite = spray[1] };
                errorImage = spray[2];
                break;

            case ItemType.Spaner:
                Image.sprite = spaner[0];
                button.spriteState = new UnityEngine.UI.SpriteState { pressedSprite = spaner[1] };
                errorImage = spaner[2];
                break;

            case ItemType.Plier:
                Image.sprite = plier[0];
                button.spriteState = new UnityEngine.UI.SpriteState { pressedSprite = plier[1] };
                errorImage = plier[2];
                break;

            default:
                Image.sprite = closeHand[0];
                button.spriteState =  new UnityEngine.UI.SpriteState { pressedSprite = closeHand[1] };
                break;
        }
    }

    public void PushItemBttn()
    {
        if (handCoroutine != null)
        {
            StopCoroutine(handCoroutine);
            Image.sprite = tempSpriteForCoroutine;
            handCoroutine = null;
        }

        buttonClickSound.Play();

        ItemType currenType = progressManager.currentItem;
        if (currenType == ItemType.Normal || currenType == ItemType.Open)
        {
            if (OpenState)       //열린상태
            {
                SetItemBttn(ItemType.Normal);
                progressManager.IsItemWindowOpen = false;
            }
            else
            {
                progressManager.IsItemWindowOpen = true;
                SetItemBttn(ItemType.Open);
                itemWindow.ReAllocButtons();
            }

            itemWindowObject.SetActive(!OpenState);
            OpenState = !OpenState;
        }
        else
        {
            SetItemBttn(ItemType.Normal);
            progressManager.currentItem = ItemType.Normal;
        }
    }

    public bool CheckItemIsOk(ItemType itemType)
    {
        ItemType currentItem = progressManager.currentItem;

        if (currentItem != itemType)
        {
            if (handCoroutine != null)
            {
                StopCoroutine(handCoroutine);
                Image.sprite = tempSpriteForCoroutine;
                handCoroutine = null;
            }

            if (currentItem != ItemType.Open && currentItem != ItemType.Normal)
                handCoroutine = StartCoroutine(ErrorCoroutine());
            else
                Handheld.Vibrate();

            return false;
        }
        return true;
    }

    public void ItemRemove()
    {
        PushItemBttn();
    }
}
