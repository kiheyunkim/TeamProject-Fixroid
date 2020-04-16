using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveTile
{
    public List<bool> stageOpen;
    public List<float> stageTime;
    public List<bool> acheiveOpen;

    public SaveTile()
    {
        stageOpen = new List<bool>(5) { false, true, false, false, false };
        stageTime = new List<float>(5) { 0f, 0f, 0f, 0f, 0f };
        acheiveOpen = new List<bool>(7) { false, false, false, false, false, false, false };
    }
}

[System.Serializable]
public class SettingSaveTile
{
    public bool bgm;
    public bool effect;
    public bool vibrate;
    public bool handOpt;
    
    public SettingSaveTile()
    {
        bgm = effect = vibrate = true;
        handOpt = false;
    }
}
