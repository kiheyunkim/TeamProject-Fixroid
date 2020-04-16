using UnityEngine;
using System.Collections;

public class MixRotationScript : MonoBehaviour
{
    public float Speed1;
    public GameObject SawToothSet1_1;
    public GameObject SawToothSet1_2;

    //
    //public GameObject SawToothSet2_1;
    //public GameObject SawToothSet2_2;
    //public GameObject SawToothSet2_3;
    //
    //public GameObject SawToothSet3_1;
    //public GameObject SawToothSet3_2;
    //public GameObject SawToothSet3_3;
    //public GameObject SawToothSet3_4;
    //
    //public GameObject SawToothSet4_1;
    //public GameObject SawToothSet4_2;
    //
    //public GameObject SawToothSet5_1;
    //public GameObject SawToothSet5_2;
    //
    //public GameObject SawToothSet6_1;
    //public GameObject SawToothSet6_2;
    //
    //// Use this for initialization
    //
    //// Update is called once per frame
    void Update()
    {
        SawToothSet1_1.transform.Rotate(new Vector3(0, 0, Speed1 *Time.deltaTime * 360 / 8f));              //11개
        SawToothSet1_2.transform.Rotate(new Vector3(0, 0, -Speed1 * Time.deltaTime * 360 /11f));              //8개
    }
}
