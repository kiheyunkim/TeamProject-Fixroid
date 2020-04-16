using UnityEngine;
using System.Collections;

public class FinalDoor : MonoBehaviour
{
    //For Sound
    public AudioSource OpenSound;

    //Global Trigger Boolean
    static public bool StartDoorOpen;
    static public bool DoorEnd;
    
    //For Door Parts
    public GameObject UpDoor;
    public GameObject DownDoor;

    //Control Boolean
    private bool IsSoundPlay;

	// Use this for initialization
	void Start ()
    {
        IsSoundPlay = true;
        StartDoorOpen = false;
        DoorEnd = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(StartDoorOpen)
        {
            if(IsSoundPlay)
            {
                OpenSound.Play();
                IsSoundPlay = false;
            }
            UpDoor.transform.Translate(0, 1f * Time.deltaTime, 0);
            DownDoor.transform.Translate(0, -1f * Time.deltaTime, 0);
            if (DownDoor.transform.position.y < -38)
            {
                DoorEnd = true;
                StartDoorOpen = false;
            }
        }
	}
}
