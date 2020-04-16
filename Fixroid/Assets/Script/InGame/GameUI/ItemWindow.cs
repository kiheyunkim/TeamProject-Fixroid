using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWindow : MonoBehaviour
{
    private AudioSource buttonClickSound;

    private ProgressManager progressManager;
    private HandButton itemButton;
    private List<UnityEngine.UI.Button> buttons = new List<UnityEngine.UI.Button>();
    private List<Sprite> itemNormal = new List<Sprite>();
    private List<Sprite> itemClicked = new List<Sprite>();

    private void Awake()
    {
        buttonClickSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/GUI/HandButtonClick");

        progressManager = ProgressManager.GetInstance;
        itemButton = transform.parent.GetComponent<HandButton>();

        foreach (var button in transform.GetComponentsInChildren<UnityEngine.UI.Button>())
            buttons.Add(button);

        gameObject.SetActive(false);

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/HandItem");
        for (int i = 13; i <= 28; i++)
        {
            if (i % 2 == 1)
                itemClicked.Add(sprites[i]);
            else
                itemNormal.Add(sprites[i]);
        }
    }

    public void ReAllocButtons()
    {
        if (!progressManager.NeedUpdate) return;
        progressManager.NeedUpdate = false;


        int itemCount = progressManager.itemGets.Count;
        for (int i = 0; i < itemCount; i++)     //코드 작성
        {
            buttons[i].image.sprite = itemNormal[(int)progressManager.itemGets[i].itemType];
            UnityEngine.UI.SpriteState spriteState = new UnityEngine.UI.SpriteState() { pressedSprite = itemClicked[(int)progressManager.itemGets[i].itemType] };
            buttons[i].spriteState = spriteState;
        }
        
        for (int i = itemCount; i < 9; i++)
        {
            buttons[i].image.sprite = itemNormal[(int)HandButton.ItemType.Normal];
            UnityEngine.UI.SpriteState spriteState = new UnityEngine.UI.SpriteState() { pressedSprite = itemClicked[(int)HandButton.ItemType.Normal] };
            buttons[i].spriteState = spriteState;
        }

    }

    public void PushButton(int buttonNum)
    {
        buttonClickSound.Play();

        if (progressManager.itemGets.Count > buttonNum && progressManager.itemGets[buttonNum].isUsable)
        {
            itemButton.SetItemBttn(progressManager.itemGets[buttonNum].itemType);
            progressManager.currentItem = progressManager.itemGets[buttonNum].itemType;
        }
        else
            itemButton.SetItemBttn(HandButton.ItemType.Normal);

        gameObject.SetActive(false);
        itemButton.OpenState = false;
        progressManager.IsItemWindowOpen = false;
    }
}
