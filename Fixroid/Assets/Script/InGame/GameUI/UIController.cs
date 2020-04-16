using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    static public UIController GetInstance { get; private set; }

    public TimerController TimerController { get; private set; }
    public PhrasesController PhrasesController { get; private set; }
    public JoyStick JoyStick { get; private set; }
    public BlurManager BlurManager { get; private set; }
    public HandButton HandButton { get; private set; }

    private Canvas canvas;

    private void Awake()
    {
        GetInstance = this;

        TimerController = GetComponentInChildren<TimerController>();
        PhrasesController = GetComponentInChildren<PhrasesController>();
        JoyStick = GetComponentInChildren<JoyStick>();
        BlurManager = GetComponentInChildren<BlurManager>();
        HandButton = GetComponentInChildren<HandButton>();

        canvas = GetComponent<Canvas>();
        SetUIState(false);
    }

    public void SetUIState(bool visible)
    {
        canvas.enabled = visible;
    }
}