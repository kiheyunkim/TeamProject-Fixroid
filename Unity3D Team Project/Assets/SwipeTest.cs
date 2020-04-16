using UnityEngine;
using System.Collections;

public class SwipeTest : MonoBehaviour
{
    public GameObject Cam;
    public bool GameStart;
    public UnityEngine.UI.Text text;
    public float Success_Count;
    public float minSwipeDistY;
    private Vector2 startPos;

    void Start()
    {
        Success_Count = 0;
        GameStart = true;
    }

    void Update()
    {
        //#if UNITY_ANDROID
        if (GameStart)
        {
            Vector2 pos = Cam.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if (hit.transform.position.x > Screen.width * 0.2265625f)
            {
                //GameStart = false;
                Debug.Log("In");
            }
            else
            if(hit.transform.position.x < Screen.width * 0.3046875f)
            {
                //GameStart = false;
                Debug.Log("In");
            }
            else
            {
                Debug.Log("Out");
            }
            if (Input.touchCount > 0)
            {
                Touch touch = Input.touches[0];
                //Ray ray = Cam.ScreenPointToRay(touch.position);
                
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        startPos = touch.position;
                        break;


                    case TouchPhase.Ended:
                        float swipeDistVertical = (new Vector3(0, touch.position.y, 0) - new Vector3(0, startPos.y, 0)).magnitude;
                        if (swipeDistVertical > minSwipeDistY)
                        {
                            float swipeValue = Mathf.Sign(touch.position.y - startPos.y);

                            if (swipeValue < 0)//down swipe
                            {
                                //성공
                                Debug.Log("Down");
                                Success_Count += 1;
                            }
                        }
                        break;
                }
            }
        }
    }
}
