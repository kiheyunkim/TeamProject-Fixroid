using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPauseMenu : MonoBehaviour
{
    private SettingManager settingManager;
    private AudioSource buttonSound;

    private UnityEngine.UI.Image bgmImg;
    private UnityEngine.UI.Image effectImg;

    private UnityEngine.UI.Button bgmbttn;
    private UnityEngine.UI.Button effectbttn;

    private List<Sprite> onSprite = new List<Sprite>();
    private List<Sprite> offSprite = new List<Sprite>();

    private void Awake()
    {
        settingManager = SettingManager.GetInstance;
        UnityEngine.UI.Image[] imgs = GetComponentsInChildren<UnityEngine.UI.Image>();
        bgmImg = imgs[0];
        effectImg = imgs[1];

        UnityEngine.UI.Button[] buttons = GetComponentsInChildren<UnityEngine.UI.Button>();
        bgmbttn = buttons[0];
        effectbttn = buttons[1];

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/GameUI");
        onSprite.Add(sprites[12]);
        onSprite.Add(sprites[13]);
        offSprite.Add(sprites[8]);
        offSprite.Add(sprites[9]);

        SetBgmImgs(settingManager.SettingTile.bgm);
        SetEffectImgs(settingManager.SettingTile.effect);

        buttonSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/PuaseMenu/PauseSoundButtonSound");
    }

    private void SetBgmImgs(bool state)
    {
        bgmImg.sprite = state ? onSprite[0] : offSprite[0];
        bgmbttn.spriteState = new UnityEngine.UI.SpriteState { pressedSprite = state ? onSprite[1] : offSprite[1] };
    }

    private void SetEffectImgs(bool state)
    {
        effectImg.sprite = state ? onSprite[0] : offSprite[0];
        effectbttn.spriteState = new UnityEngine.UI.SpriteState { pressedSprite = state ? onSprite[1] : offSprite[1] };
    }

    public void ClickBgm()
    {
        buttonSound.Play();
        settingManager.SettingTile.bgm = !settingManager.SettingTile.bgm;
        AudioSetter.SetBGMVolume(settingManager.SettingTile);
        SetBgmImgs(settingManager.SettingTile.bgm);
    }

    public void ClickEffect()
    {
        buttonSound.Play();
        settingManager.SettingTile.effect = !settingManager.SettingTile.effect;
        AudioSetter.SetEffectVolume(settingManager.SettingTile);
        SetEffectImgs(settingManager.SettingTile.effect);
    }    
}
