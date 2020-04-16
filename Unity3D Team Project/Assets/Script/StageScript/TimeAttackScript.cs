using UnityEngine;
using System.Collections;

public class TimeAttackScript : MonoBehaviour
{
    //Activation Option
    static public float LeftTime;
    static public bool TimeAttackStart;
    static public bool TimeEnd;
    static public bool TimeRecord;

    //Windows
    public GameObject Text;

    //for Time Display
    private int Minute;
    private int Second;
    private int Milisecond;

    //for Character Dead
    private bool DeadTrig;
    public bool DeadStop;
    public GameObject Character;

    void Start ()
    {
        
        DeadTrig = false;
        TimeRecord = false;
        LeftTime = 50.0f;//30sec
	}
	
	// Update is called once per frame
	void Update ()
    {
       if(TimeAttackStart)
        {
            LeftTime -= Time.deltaTime;
            Minute = (int)LeftTime / 60;
            Second = (int)LeftTime / 1;
            Milisecond = (int)((LeftTime * 100) % 100f);

            if (LeftTime > 0)
                Text.GetComponent<UnityEngine.UI.Text>().text = Minute.ToString("D2") + ":" + Second.ToString("D2") + ":" + Milisecond.ToString("D2") + "  ";
            else
            {
                Text.GetComponent<UnityEngine.UI.Text>().text = "00" + ":" + "00" + ":" + "00  ";
                TimeEnd = false;
                DeadTrig = true;        //Character Dead
                TimeAttackStart = false;
                EventMessage_Time.TimeAttackActivate = false;
            }
        }
       if(TimeRecord)
        {
            FinalScore.Result = LeftTime;
            TimeRecord = false;
        }
       if(DeadTrig)
        {
            Character.GetComponent<Animator>().SetTrigger("Dead");//Character Dead
            DeadStop = true;
            DeadTrig = false;
        }
        if (DeadStop)
        {
            VirtualJoyStick.StopMovingX = 0;
            VirtualJoyStick.StopMovingY = 0;
        }

    }
}
