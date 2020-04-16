using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class JoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private const float walkMin = -0.04f;
    private const float walkMax = 0.04f;

    public float XValue { get; private set; }
    public float YValue { get; private set; }

    private UnityEngine.UI.Image joyStickPedImg;
    private UnityEngine.UI.Image joyStickBkgImg;
    private Vector2 inputVec;

    void Awake()
    {
        joyStickPedImg = transform.GetChild(0).GetComponent<UnityEngine.UI.Image>();
        joyStickBkgImg = GetComponent<UnityEngine.UI.Image>();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        XValue = 0;
        YValue = 0;
        inputVec = Vector2.zero;
        joyStickPedImg.rectTransform.anchoredPosition = Vector2.zero;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joyStickBkgImg.rectTransform, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = pos.x / joyStickBkgImg.rectTransform.sizeDelta.x;
            pos.y = pos.y / joyStickBkgImg.rectTransform.sizeDelta.y;
        }

        inputVec = new Vector2(pos.x, pos.y);
        inputVec = (inputVec.magnitude > 0.3f) ? inputVec.normalized * 0.3f : inputVec;
        joyStickPedImg.rectTransform.anchoredPosition = new Vector2(inputVec.x * joyStickBkgImg.rectTransform.sizeDelta.x, inputVec.y * joyStickBkgImg.rectTransform.sizeDelta.y);

        XValue = Mathf.Abs(pos.x) > walkMax ? (pos.x > 0 ? walkMax : walkMin) : pos.x;
        XValue *= 0.8f;
        YValue = Mathf.Abs(pos.y) > walkMax ? (pos.y > 0 ? walkMax : walkMin) : pos.y;
    }
}
