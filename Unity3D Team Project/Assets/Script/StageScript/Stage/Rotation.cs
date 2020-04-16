using UnityEngine;
using System.Collections;

public class Rotation : MonoBehaviour
{
    //Global Trigger 
    static public bool SmallSawTooth_Activate;
    static public bool MiniGameSpannerEnd;
    static public bool FirstBarrier;
    //Small SawTooth
    public GameObject[] LittleSawTooth = new GameObject[6];
    public GameObject[] SawTooth_Set1 = new GameObject[2];

    public GameObject[] SawTooth_Set2 = new GameObject[8];
    public bool Set2_first;
    static public bool Set2_Second;
    public float Set2_Time;
    public float Set2_Direction;
    public bool Set2_stop;

    public GameObject[] SawTooth_Set3 = new GameObject[3];
    public float StopTime_Set3;
    public bool Fast_Set3;
    private float direction_Set3;
    private float count_Set3;

    public GameObject[] SawTooth_Set4 = new GameObject[3];
    public GameObject[] SawTooth_Set5 = new GameObject[2];
    public float StopTime_Set5;
    public bool Fast_Set5;
    private float direction_Set5;
    private float count_Set5;

    public GameObject[] SawTooth_Set6 = new GameObject[2];
    public GameObject[] SawTooth_Individual = new GameObject[1];
    public GameObject[] Trap_Set = new GameObject[2];

    public GameObject[] Falling_Barrier = new GameObject[4];

    public float StopTime_Trap;
    public bool Fast_TrapSet;
    private float direction_TrapSet;
    private float count_TrapSet;

    public Vector3 speed;

    void Start()
    {
        StopTime_Set3 = 3;
        Fast_Set3 = false;
        direction_Set3 = 1;
        count_Set3 = 0;

        StopTime_Set5 = 3;
        Fast_Set5 = false;
        direction_Set5 = 1;
        count_Set5 = 0;

        StopTime_Trap = 3;
        Fast_TrapSet = false;
        direction_TrapSet = 1;
        count_TrapSet = 0;

        Set2_first = true;
        Set2_Second = false;
        Set2_Time = 0.1f;
        Set2_Direction = 1;
        Set2_stop = true;
    }

    void Update()
    {
        if (SmallSawTooth_Activate)
        {
            for (int i = 0; i < 6; i++)
                LittleSawTooth[i].transform.Rotate(new Vector3(0, 0, 50f * Time.deltaTime));
        }
        //Set 1
        SawTooth_Set1[0].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 11 * 100));
        SawTooth_Set1[1].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100));

        //Set 2
        if (Set2_first)
        {
            if (Set2_Time > 0)
            {
                Set2_Time -= Time.deltaTime;
                if (!Set2_stop)
                {
                    if (Set2_Time < 0.5f / 2)
                    {
                        Set2_Direction *= -1;
                    }
                    SawTooth_Set2[0].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100 * Set2_Direction));
                    SawTooth_Set2[1].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 11 * 100 * Set2_Direction));
                    SawTooth_Set2[2].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100 * Set2_Direction));
                    SawTooth_Set2[3].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 8 * 100 / 2 * Set2_Direction));
                    SawTooth_Set2[4].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 8 * 100 / 2 * Set2_Direction));
                    SawTooth_Set2[5].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100 / 2 * Set2_Direction));
                }
            }
            else
            {
                if (Set2_stop)
                {
                    Set2_Time = 0.1f;
                    Set2_stop = false;
                    Set2_Direction = 1;
                    return;
                }
                if (!Set2_stop)
                {
                    Set2_Time = 1.5f;
                    Set2_stop = true;
                    Set2_Direction = 1;
                    return;
                }
            }
        }
        if (Set2_Second)
        {
            Set2_first = false;
            SawTooth_Set2[0].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100));
            SawTooth_Set2[1].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 11 * 100));
            SawTooth_Set2[2].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100));
            SawTooth_Set2[3].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 8 * 100 / 2));
            SawTooth_Set2[4].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 8 * 100 / 2));
            SawTooth_Set2[5].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100 / 2));
        }

        SawTooth_Set2[6].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 10 / 8 * 100 / 2));
        SawTooth_Set2[7].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 10 / 8 * 100 / 2));


        //Set 3
        if (MiniGameSpannerEnd)
        {
            //Set
            SawTooth_Set3[0].transform.Rotate(new Vector3(0, 0, -Time.deltaTime * 600 / 32));     //36
            SawTooth_Set3[1].transform.Rotate(new Vector3(0, 0, Time.deltaTime * 600 / 36));                  //32    
            //Individual Part
            SawTooth_Set3[2].transform.Rotate(new Vector3(0, 0, 13.75f * Time.deltaTime * 10));
        }

        //Set 4(Trap)
        if (FirstBarrier)
        {
            if (Fast_Set3)
            {
                SawTooth_Set4[0].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 2 * direction_Set3));      //20EA
                SawTooth_Set4[1].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 3 * direction_Set3));       //30EA
            }
            if (!Fast_Set3)
            {
                SawTooth_Set4[0].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 6 * direction_Set3));       //20EA
                SawTooth_Set4[1].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 9 * direction_Set3));        //30EA
            }
            SawTooth_Set4[2].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 10));                                 //Set4 - Individual

            //Trigger Activate
            SawTooth_Set4[1].GetComponent<PolygonCollider2D>().isTrigger = false;

            if (StopTime_Set3 > 0)
            {
                StopTime_Set3 -= Time.deltaTime;
            }
            else
            {
                if (Fast_Set3)
                {
                    StopTime_Set3 = 3;
                    Fast_Set3 = false;
                    count_Set3++;
                    return;
                }
                if (!Fast_Set3)
                {
                    StopTime_Set3 = 2;
                    Fast_Set3 = true;
                    count_Set3++;
                    return;
                }
            }

            if (count_Set3 == 2)
            {
                count_Set3 = 0;
                direction_Set3 *= -1;
            }
        }
        else
        {
            //Trigger activate
            SawTooth_Set4[1].GetComponent<PolygonCollider2D>().isTrigger = false;
        }

        //Set5(Trap)    
        if (Fast_Set5)
        {
            SawTooth_Set5[0].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 2 * direction_Set5));  //20EA
            SawTooth_Set5[1].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 3 * direction_Set5));    //30EA

        }
        if (!Fast_Set3)
        {
            SawTooth_Set5[0].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 6 * direction_Set5));    //20EA
            SawTooth_Set5[1].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 9 * direction_Set5));    //30EA
        }


        if (StopTime_Set5 > 0)
        {
            StopTime_Set5 -= Time.deltaTime;
        }
        else
        {
            if (Fast_Set5)
            {
                StopTime_Set5 = 3;
                Fast_Set5 = false;
                count_Set5++;
                return;
            }

            if (!Fast_Set5)
            {
                StopTime_Set5 = 2;
                Fast_Set5 = true;
                count_Set5++;
                return;
            }
        }
        if (count_Set5 == 2)
        {
            count_Set5 = 0;
            direction_Set5 *= -1;
        }



        //Individual
        for (int i = 0; i < 1; i++)
            SawTooth_Individual[i].transform.Rotate(new Vector3(0, 0, 5 * Time.deltaTime * 10));

        //Set6 //21
        //36
        SawTooth_Set6[0].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 21));
        SawTooth_Set6[1].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 36));

        //TrapSet

        if (!Fast_TrapSet)
        {
            Trap_Set[0].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 2 * direction_TrapSet));  //20EA
            Trap_Set[1].transform.Rotate(new Vector3(0, 0, 9.666666666666666f * Time.deltaTime * 100 / 3 * (-direction_TrapSet)));    //30EA

        }
        if (Fast_TrapSet)
        {
            Trap_Set[0].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 6 * direction_TrapSet));    //20EA
            Trap_Set[1].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 100 / 9 * (-direction_TrapSet)));   //30EA

        }


        if (StopTime_Trap > 0)
        {
            StopTime_Trap -= Time.deltaTime;
        }
        else
        {
            if (Fast_TrapSet)
            {
                StopTime_Trap = 3;
                Fast_TrapSet = false;
                count_TrapSet++;
                return;
            }
            if (!Fast_TrapSet)
            {
                StopTime_Trap = 4;
                Fast_TrapSet = true;
                count_TrapSet++;
                return;
            }
        }
        if (count_TrapSet == 2)
        {
            count_TrapSet = 0;
            direction_TrapSet *= -1;
        }

        for (int i = 0; i < 4; i++)
        {
            Falling_Barrier[i].transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 300 / 6));
        }
    }

}
