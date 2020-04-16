using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    static public bool CameraShaking;
    static public bool StartPosition;
    public Vector3 Standard;

    //For Sound
    public AudioSource ShakingSound;

    static public int Count;
    private bool left;
    private bool Right;
    private bool ShakeControl;

    static public Vector3 CurrentPos;
	// Use this for initialization
	void Start ()
    {
        Count = 12;
        CameraShaking = false;
        ShakeControl = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(StartPosition)
        {
            left = true;
            Right=false;
            StartPosition = false;
            ShakingSound.Play();
        }
        if (CameraShaking)
        {
            if(ShakeControl)    
            {
                if (transform.position.x < CurrentPos.x - 0.2f)
                {
                    left = true;
                    Right = false;
                    Count--;
                }
                if (transform.position.x > CurrentPos.x + 0.2f)
                {
                    left = false;
                    Right = true;
                    Count--;
                }
            }

            if (Count == 0)
            {
                ShakeControl = false;
                transform.Translate(Time.deltaTime * 0.001f, 0, 0);
                if(transform.position.x >= CurrentPos.x)
                      CameraShaking = false;
            }

            if (left)
            {
                transform.Translate(Time.deltaTime * 10f, 0, 0);                        //Camera
            }
            if (Right)
            {
                transform.Translate(-Time.deltaTime * 10f, 0, 0);
            }
        }
    }
    
}
