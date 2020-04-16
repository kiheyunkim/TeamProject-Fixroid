using UnityEngine;
using System.Collections;

public class MiniGameHandle : MonoBehaviour
{
    //For Sound
    public AudioSource MessageSound;
    public AudioSource RotationSound;

    static public bool Success;
    static public bool Fail;
    static public bool Gamestart;

    public Sprite Handle_Prev;
    public Sprite Handle_Next;
    public Sprite Nut_Next;
    public Sprite Nut_Prev;

    public GameObject Integral;
    public GameObject CAMERA;
    public GameObject TargetNut;
    public GameObject Handle;
    public GameObject BackGround;
    public GameObject Character;
    
    public bool MoveStart;
    public bool IsClicked;
    public bool LogicStart;

    public bool Direction;      //true : Right, false , Left;
    public bool Move;

    public bool HandleRotation;
    public float Angle;
    public float RotationTime;
	// Use this for initialization
	void Start ()
    {
        HandleRotation = false;
        LogicStart = false;
        Direction = false;
        MoveStart = false;
        IsClicked = false;
        RotationTime = 3.0f;
        Move = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = CAMERA.transform.position;
        if(Gamestart)
        {
            HandleRotation = false;
            Move = true;
            MoveStart = true;
            Direction = true;
            Fail = false;
            Success = false;
            Gamestart = false;
        }
        if(MoveStart)
        {
            if (TargetNut.transform.position.x < transform.position.x+ (-3.5f))
            {
                Direction = true;
            }
            if (TargetNut.transform.position.x > transform.position.x + 3.5f)
            {
                Direction = false;
            }

            if(Direction&&Move)
            {
                TargetNut.transform.Translate(10.0f * Time.deltaTime, 0, 0);
            }   
            if(!Direction&&Move)
            {
                TargetNut.transform.Translate(-10.0f * Time.deltaTime, 0, 0);
            }
        }
        if(LogicStart)
        {
            if (TargetNut.transform.localPosition.x > -0.2f && TargetNut.transform.localPosition.x < 0.2f)
            {
                Move = false;
                Success = true;
                LogicStart = false;
                BackGround.SetActive(false);
                Handle.GetComponent<SpriteRenderer>().sprite = Handle_Next;
                TargetNut.GetComponent<SpriteRenderer>().sprite = Nut_Next;
                HandleRotation = true;
            }
            else
            {
                Move = false;
                MinigameSuccess.Fail = true;
                Fail = true;
                IsClicked = false;
                MoveStart = false;
                LogicStart = false;
                Handle.GetComponent<SpriteRenderer>().sprite = Handle_Prev;
                TargetNut.GetComponent<SpriteRenderer>().sprite = Nut_Prev;
                Integral.SetActive(false);
            }
        }
        if(HandleRotation)
        {
            if(!RotationSound.isPlaying)
            {
                RotationSound.Play();
            }
            Angle = Handle.transform.rotation.z;    
            if (Angle < 0)
            {
                RotationSound.Stop();
                if (MainMenu.EffectValue)
                    MessageSound.Play();
                Handle.GetComponent<SpriteRenderer>().sprite = Handle_Prev;
                HandleRotation = false;
                Handle.SetActive(false);
                TargetNut.SetActive(false);
                Integral.SetActive(false);
                Character.GetComponent<Animator>().SetTrigger("TriggerVictory");
                HandScript.EquippedItem = Stage1UI.Item.Basic;
                CharacterMoving.EventType = CharacterMoving.MessageType.Handle_After;
                EventMessage_Time.OnlyEventActivate = true;
                MinigameSuccess.Success = true;
            }
            Handle.transform.Rotate(new Vector3(0, 0, 100 * Time.deltaTime));
        }
    }
    void OnMouseDown()
    {
        if(MoveStart)
        {
            Direction = true;
            MoveStart = false;
            IsClicked = true;
            LogicStart = true;
            Move = false;
        }
    }
}
