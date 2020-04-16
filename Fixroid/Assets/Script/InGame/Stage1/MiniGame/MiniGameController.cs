using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameController : MonoBehaviour
{
    private BlurManager blurManager;
    private HandButton handButton;
    private ProgressManager progressManager;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    public enum GameType { RandomTouchUp, RandomTouchDown, Handle, Nut, Spring };
    public GameType gameType;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Start ()
    {
        blurManager = UIController.GetInstance.BlurManager;
        handButton = UIController.GetInstance.HandButton;
        progressManager = ProgressManager.GetInstance;
    }

    public void CleanUp()
    {
        switch (gameType)
        {
            case GameType.RandomTouchUp:
            case GameType.RandomTouchDown:
                Destroy(spriteRenderer);
                Destroy(animator);
                break;
            case GameType.Spring:
                break;
            default:
                break;
        }
    }

    private void OnMouseDown()
    {
        if (!progressManager.IsReady)
            return;

        if (progressManager.IsGamePlaying)
            return;

        if (progressManager.IsItemWindowOpen)
            return;

        progressManager.IsGamePlaying = true;

        switch (gameType)
        {
            case GameType.RandomTouchUp:
                blurManager.StartBlur(BlurManager.RequestType.RandomTouchGameUp);
                break;
            case GameType.RandomTouchDown:
                blurManager.StartBlur(BlurManager.RequestType.RandomTouchGameDown);
                break;
            case GameType.Handle:
                if (handButton.CheckItemIsOk(HandButton.ItemType.Spaner) && !progressManager.HandleGameEnd)
                    blurManager.StartBlur(BlurManager.RequestType.HandleGame);
                else
                    progressManager.IsGamePlaying = false;
                break;
            case GameType.Nut:
                if (handButton.CheckItemIsOk(HandButton.ItemType.Spaner) && !progressManager.NutGameEnd && progressManager.CheckItem(HandButton.ItemType.Nut))
                    blurManager.StartBlur(BlurManager.RequestType.NutGame);
                else
                    progressManager.IsGamePlaying = false;
                break;
            case GameType.Spring:
                break;
            default:
                break;
        }
    }
}
