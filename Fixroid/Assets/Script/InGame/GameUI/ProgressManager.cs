using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour
{
    static public ProgressManager Progress = null;
    static public ProgressManager GetInstance
    {
        get
        {
            if (Progress == null)
            {
                GameObject progressObject = new GameObject() { name = "Progress" };
                Progress = progressObject.AddComponent<ProgressManager>();
                Progress.NeedUpdate = false;
                Progress.IsReady = false;
                Progress.IsGamePlaying = false;
                Progress.HandleGameEnd = false;
                Progress.NutGameEnd = false;
                Progress.IsItemWindowOpen = false;
            }

            return Progress;
        }
    }

    public List<HandButton.Item> itemGets = new List<HandButton.Item>();
    public HandButton.ItemType currentItem = HandButton.ItemType.Normal;
    public bool NeedUpdate { get; set; }
    public bool IsReady { get; set; }
    public bool IsGamePlaying { get; set; }
    public bool HandleGameEnd { get; set; }
    public bool NutGameEnd { get; set; }
    public bool IsSawBGet { get; set; }
    public bool IsTimerAttackStart { get; set; }
    public bool IsItemWindowOpen { get; set; }

    public float time = 5;

    public void AddItem(HandButton.ItemType itemType)
    {
        HandButton.Item item = new HandButton.Item
        {
            itemType = itemType
        };

        switch (itemType)
        {
            case HandButton.ItemType.Spray:
            case HandButton.ItemType.FullSpray:
            case HandButton.ItemType.Spaner:
            case HandButton.ItemType.Plier:
                item.isUsable = true;
                break;
            case HandButton.ItemType.Nut:
            case HandButton.ItemType.SawA:
            case HandButton.ItemType.SawB:
            case HandButton.ItemType.Normal:
            case HandButton.ItemType.Open:
                item.isUsable = false;
                break;
        }

        itemGets.Add(item);
        NeedUpdate = true;
    }

    public void RemoveItem(HandButton.ItemType itemType)
    {
        itemGets.RemoveAll(x => x.itemType == itemType);
        NeedUpdate = true;
    }

    public bool CheckItem(HandButton.ItemType itemType)
    {
        return itemGets.Exists(x => x.itemType == itemType);
    }

}
