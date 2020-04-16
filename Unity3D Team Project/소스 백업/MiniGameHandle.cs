
using UnityEngine;
using System.Collections;

public class MiniGameHandle : MonoBehaviour
{
    public bool Fail;
    public GameObject Target_Handle;
    public GameObject Camera;

    public bool TouchStart;
    public float Angle;
	// Use this for initialization
	void Start ()
    {
        TouchStart = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Target_Handle.transform.position = Camera.transform.position;
	    if(Input.touchCount>0)
        {
            Touch touch = Input.touches[0];
            Debug.Log(touch.position);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    if (touch.position.y > Screen.height * 0.7888349514563107f)
                    {
                        Fail = true;
                    }
                    else
                    if (touch.position.y < Screen.height * 0.7402912621359223f)
                    {
                        Fail = true;
                    }
                    else
                    if (touch.position.x > Screen.width * 0.3175675675675676f)
                    {
                        Fail = true;
                    }
                    if (touch.position.x < Screen.width * 0.2905405405405405f)
                    {
                        Fail = true;
                    }
                    else
                    if (!TouchStart)
                    {
                        TouchStart = true;
                    }
                    //Not In First Area, Fail
                    break;

                case TouchPhase.Moved:

                    Angle = Mathf.Atan(touch.position.y / touch.position.x);        //Theta

                    if (Angle % 90 != 0)
                    {
                        if (touch.position.x - Screen.width * 0.5f < touch.position.x - Screen.width * 0.5f - (Screen.width * 0.1351351351351351f) * Mathf.Cos(Angle))
                        {
                            Fail = true;
                        }
                        else
                        if (touch.position.x - Screen.width * 0.5f > touch.position.x - Screen.width * 0.5f + (Screen.width * 0.1351351351351351f) * Mathf.Cos(Angle))
                        {
                            Fail = true;
                        }
                        else
                        if (touch.position.y - Screen.height * 0.5097087378640777f < touch.position.y - Screen.height * 0.5097087378640777f - (Screen.width * 0.1351351351351351f) * Mathf.Cos(Angle))
                        {
                            Fail = true;
                        }
                        else
                        if (touch.position.y - Screen.height * 0.5097087378640777f > touch.position.y - Screen.height * 0.5097087378640777f + (Screen.width * 0.1351351351351351f) * Mathf.Cos(Angle))
                        {
                            Fail = true;
                        }
                    }


                    //In Circle
                    break;

                case TouchPhase.Ended:
                    //Not in Last Area, Fail
                    break;
            }
        }
	}
}
