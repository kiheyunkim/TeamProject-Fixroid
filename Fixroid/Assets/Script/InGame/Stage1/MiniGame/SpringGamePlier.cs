using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class SpringGamePlier : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    private AudioSource cuttingSound;

    public enum State { Normal, Pushed, Drag };
    public State state = State.Normal;

    private Canvas canvas;
    private bool isMouseEnable = true;

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        cuttingSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/MiniGame/SpringGame/CuttingSound");
    }
    
    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        if (!isMouseEnable) return;

        Vector2 point = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Ray2D ray = new Ray2D(point, Vector2.zero);
        RaycastHit2D raycastHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (raycastHit.collider != null)
        {
            if (Input.GetMouseButtonUp(0))
            {
                if (raycastHit.collider.name == "Right")
                {
                    cuttingSound.Play();
                    isMouseEnable = false;
                    GetComponentInParent<SpringGame>().IsCollision(false);
                }
                else if (raycastHit.collider.name == "Left")
                {
                    cuttingSound.Play();
                    isMouseEnable = false;
                    GetComponentInParent<SpringGame>().IsCollision(true);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isMouseEnable) return;

        OnDrag(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isMouseEnable) return;

        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, canvas.worldCamera, out pos);
        transform.position = canvas.transform.TransformPoint(pos);
    }
}
