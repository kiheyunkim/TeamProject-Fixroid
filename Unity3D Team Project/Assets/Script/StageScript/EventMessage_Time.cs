using UnityEngine;
using System.Collections;

public class EventMessage_Time : MonoBehaviour
{
    //Global Window Control
    static public bool TimeAttackActivate;
    static public bool OnlyEventActivate;
    static public bool EventMessageExit;
    //Windows
    public GameObject EventWindow;
    public GameObject TimeWindow;
    
    //Internal Control Boolean
    private bool TimeActivate;
    private bool WordActivate;
    private float DelayTime;

    //Position Saving
    //Vector3 Prev_EventPosition;
    //Vector3 Prev_TimePosition;
    public Vector3 TimePosition;
    //1.09069444444444444444444444444444//Time
    //1.25597222222222222222222222222222//Message Original
    //

    public bool Message;
    void Start()
    {
        DelayTime = 2;
        TimeActivate = true;
        WordActivate = true;

        //Save Prev Position for Return
        //Prev_EventPosition = EventWindow.transform.position;
        //Prev_TimePosition = TimeWindow.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (OnlyEventActivate)
        {
            if (WordActivate)
            {
                LerpMoving(Screen.height* 1.25597222222222222222222222222222f - Screen.height * 0.3333333333333f, EventWindow);
                if (EventWindow.transform.position.y < Screen.height * 1.25597222222222222222222222222222f - Screen.height * 0.3333333333333f + 1)
                {
                    TimePosition = EventWindow.transform.position;
                    WordActivate = false;
                }
            }
            else
            {
                if (DelayTime < 0)
                {
                    LerpMoving(Screen.height * 1.25597222222222222222222222222222f, EventWindow);
                    if (EventWindow.transform.position.y > Screen.height * 1.25597222222222222222222222222222f - 2)
                    {
                        WordActivate = true;
                        OnlyEventActivate = false;
                    }
                }
                else
                {
                    DelayTime -= Time.deltaTime;
                    return;
                }
            }

        }
        else
        if (TimeAttackActivate)//Time Attack Activate Condition
        {
            if (WordActivate)//For EventWindow
            {
                LerpMoving(Screen.height * 1.25597222222222222222222222222222f - Screen.height * 0.3333333333333f, EventWindow);
                if (EventWindow.transform.position.y < Screen.height * 1.25597222222222222222222222222222f - Screen.height * 0.3333333333333f + 1)
                {
                    TimePosition = EventWindow.transform.position;
                    WordActivate = false;
                }
            }
            if (TimeActivate)//For TimeWindow
            {
                LerpMoving(Screen.height * 1.09069444444444444444444444444444f - Screen.height * 0.3333333333333f, TimeWindow);
                if (TimeWindow.transform.position.y < Screen.height * 1.09069444444444444444444444444444f - Screen.height * 0.3333333333333f + 1)
                { 
                    TimeActivate = false;
                }
            }
            if (!TimeActivate && !WordActivate)//For Showing Windows and Returning EventWindow Position
            {
                if (EventMessageExit)
                {
                    LerpMoving(Screen.height * 1.25597222222222222222222222222222f - Screen.height * 0.3333333333333f, TimeWindow);
                    LerpMoving(Screen.height * 1.25597222222222222222222222222222f, EventWindow);
                }
                //if (DelayTime < 0)
                //{
                //    LerpMoving(TimePosition.y, TimeWindow);
                //    LerpMoving(Prev_EventPosition.y, EventWindow);
                //}
                //else
                //{
                //    DelayTime -= Time.deltaTime;
                //    return;
                //}
            }
        }
        else
            DelayTime = 2;
    }
    void LerpMoving(float position,GameObject Target)
    {
        float newY = Mathf.Lerp(Target.transform.position.y, position, Time.deltaTime * 2.5f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        Vector3 newPosition = new Vector3(Target.transform.position.x, newY);
        Target.transform.position = newPosition;
    }
}
