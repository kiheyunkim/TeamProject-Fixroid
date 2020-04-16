using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System;
//8:07
public class VirtualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    static public bool JoystickEnable;
    public float SpeedLimitX = 0.5f;
    public float SpeedLimitY = 2;
    static public float StopMovingX;
    static public float StopMovingY;
    private float XSpeed;
    private float YSpeed;
    private Image bgImg;
    private Image JoyStickImg;
    private Vector3 InputVector;

    private void Start()
    {
        StopMovingX = 1;
        StopMovingY = 1;
        //for JoyStick Image
        JoystickEnable = true;
        bgImg = GetComponent<Image>();
        JoyStickImg = transform.GetChild(0).GetComponent<Image>();
    }
    

    public virtual void OnDrag(PointerEventData ped) //Behaviour for Drag   
    {
        if(JoystickEnable)
        {
            Vector2 pos;
            CharacterMoving.isMove = true;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(bgImg.rectTransform, ped.position, ped.pressEventCamera, out pos))
            {
                pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
                pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);


                InputVector =new Vector3(pos.x , 0, pos.y );// new Vector3(0, 0, 0);//

                InputVector = (InputVector.magnitude > 1.0f) ? InputVector.normalized : InputVector;
            
                //Move JoyStick Img
                JoyStickImg.rectTransform.anchoredPosition = new Vector3(InputVector.x*1/3 * bgImg.rectTransform.sizeDelta.x , InputVector.z*1/3 * (bgImg.rectTransform.sizeDelta.y ));//범위 설정(벗어나지 말라는)

                //transfer Speed Value
                XSpeed = pos.x;
                YSpeed = pos.y;

                //adjust Speed
                XSpeed = Mathf.Clamp(XSpeed, -0.6f / SpeedLimitX, 0.6f / SpeedLimitX) * StopMovingX;
                YSpeed = Mathf.Clamp(YSpeed, -0.6f / SpeedLimitY, 0.6f / SpeedLimitY) * StopMovingY;


                //adjust for Right Left hand Mode
                if(MainMenu.LeftOrRight)
                    CharacterMoving.Position = new Vector2(-XSpeed, YSpeed);
                else
                    CharacterMoving.Position = new Vector2(XSpeed, YSpeed);
                //Debug.Log(pos);
            }
        }
    }
        

    public virtual void OnPointerDown(PointerEventData ped)//Behaviour for Click
    {
        if (JoystickEnable)
            OnDrag(ped);
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)//Behavior for UnClick
    {
        if (JoystickEnable)
        {
            InputVector = Vector3.zero;
            CharacterMoving.Position = new Vector2(0,0);       //조이스틱에 마우스를 떼면 캐릭터 정지.
            JoyStickImg.rectTransform.anchoredPosition = Vector3.zero;
            CharacterMoving.isMove = false;
        }
    }


}
