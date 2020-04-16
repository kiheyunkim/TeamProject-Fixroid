using UnityEngine;
using System.Collections;

public class CharacterMoving : MonoBehaviour
{
    //For Sound
    public AudioSource EnterAreaSound;
    static public bool OilSoundArea;

    //Item Area
    static public bool OilArea;
    static public bool PlierArea;

    //Jump Control
    private bool isGround;

    //Character (this)
    public GameObject Character;

    //For Ending
    public GameObject EndingAnimation;

    //Control Boolean
    public bool InLadder;
    public bool OnGround;
    public bool Laddering;
    public bool AdjustSector;
    
    public bool Climbing;
    static public bool isMove;
    static public bool IsJump;
    
    //for initial position
    public static Vector2 Position;

    //for Equipped Item
    public Stage1UI.Item EquippedItem;

    // Set EventMessage
    public enum MessageType
    {
        Oil_Before, Oil_After, Cuckoo_Before, Cuckoo_After, Handle_Before, Handle_After, Spaner_Before, Spaner_After, ChainNut_Insert, SawToothA_Before, SawToothB_Before,
        GetOil, GetOilSpray, GetNut, Handle_Before2, GetSawToothA, GetSawToothB, GetPlier, Cuckoo_After2nd
    }
    static public bool[] MessageDisplay = new bool[10];   //Display EventMessage Only for once
    static public MessageType EventType;

    //Control Ending Animation;
    public bool EndingAnimeStart;

    //Dead Judge
    public bool Dead;
    public bool Dead2;

    void Start()
    {
        Dead = false;
        Dead2 = false;
        AdjustSector = false;
        OilArea = false;
        InLadder = false;
        Laddering = false;

        EndingAnimeStart = true;
        for(int i=0;i<10;i++)
        {
            MessageDisplay[i] = true;
        }
    }
    void Update()
    {
        EquippedItem = Stage1UI.Equipped;                                                                           //Current Equipped Item

        if (Laddering)                                                                                              //In Ladder AnSimation
        {
            if (Position.y == 0)
                transform.GetComponent<Animator>().SetBool("LadderIdleEnter", true);
            else
            {
                transform.GetComponent<Animator>().SetBool("LadderIdleEnter", false);
                transform.GetComponent<Animator>().SetBool("Ladder", true);
            }
        }
        else
            transform.GetComponent<Animator>().SetBool("Ladder", false);


        //Character Flip  Right/Left
        if (Position.x < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Position.x > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if(AdjustSector)
        {
            Character.transform.Translate(0, Position.y / 7 * 9 / 4, 0);
        }
        else
        {
            //Moving
            if (InLadder && !OnGround)
            {
                Character.transform.Translate(0, Position.y / 7 * 9 / 4, 0);
            }
            else
            if (InLadder && OnGround)
            {
                Character.transform.Translate(Position.x / 7 * 1, Position.y / 7 * 9 / 4, 0);
            }
            else
            if (!InLadder && OnGround)
            {
                Character.transform.Translate(Position.x / 7 * 1, 0, 0);
            }
            else
            {
                Character.transform.Translate(Position.x / 7 * 1, 0, 0);
            }
        }

        //for Jump Option
        if (IsJump)
        {
            Jump();
            IsJump = false;
        }
        else
            gameObject.GetComponent<Animator>().SetBool("EnterJump", false);


        if (!InLadder)
        {
            if (isMove)
                gameObject.GetComponent<Animator>().SetBool("EnterWalk", true);
            else
                gameObject.GetComponent<Animator>().SetBool("EnterWalk", false);
        }
        else
        {
            if(!isMove)
                gameObject.GetComponent<Animator>().SetBool("EnterWalk", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D Col) 
    {



        //For Oil Sound 
        if (Col.gameObject.name == "OilSoundArea")
        {
            OilSoundArea = true;
        }

        //Item Use Control
        if (Col.gameObject.name == "Plier")
        {
            PlierArea = true;
        }

        if (Col.gameObject.tag== "LadderAdjust")
        {
            AdjustSector = true;
        }

        if(Col.gameObject.tag== "KnifeSawtooth")
        {
            if(EventMessage_Time.TimeAttackActivate)
            {
                if(!Dead2)
                {
                    Dead2 = true;
                    Dead = true;
                    Debug.Log("Collision with knife sawtooth");
                    Character.GetComponent<Animator>().SetTrigger("Dead");//Character Dead
                    VirtualJoyStick.StopMovingX = 0;
                    VirtualJoyStick.StopMovingY = 0;
                }
            }
        }

        if (Col.gameObject.tag == "OilArea")
        {
            OilArea = true;
            Debug.Log("Enter Oil Area");
        }

        //Ending(Exiting)
        if (Col.gameObject.name == "Ending")
        {
            if (EventMessage_Time.TimeAttackActivate)
            {
                if (EndingAnimeStart)
                {
                    if(!Dead)
                    {
                        Stage1UI.BGMStop = true;
                        TimeAttackScript.TimeAttackStart = false;
                        Stage1UI.TraceCamera = false;
                        Stage1UI.Destory_Character = true;
                        Destroy(Character);
                        TimeAttackScript.TimeRecord = true;
                        EndingAnimation.GetComponent<Animator>().SetTrigger("Ending");
                        EndingAnimeStart = false;
                    }
                }
            }
        }

        //Message Area
        if (Col.gameObject.name == "Cuckoo")
        {
            if (MessageDisplay[0])
            {
                if (CuckooControl.EventOpen)
                {
                    if (MainMenu.EffectValue)
                        EnterAreaSound.Play();
                    EventType = MessageType.Cuckoo_Before;
                    EventMessage_Time.OnlyEventActivate = true;
                    MessageDisplay[0] = false;
                }
            }
        }
        if (Col.gameObject.name == "Handle")
        {
            if(MessageDisplay[1])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.Handle_Before;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[1] = false;
            }
        }
        if (Col.gameObject.name == "Oil")
        {
            if(MessageDisplay[2])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.Oil_Before;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[2] = false;
            }
        }
        if (Col.gameObject.name == "Spaner")
        {
            if(MessageDisplay[3])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.Spaner_Before;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[3] = false;
            }
        }
        
        if (Col.gameObject.name == "SawToothB")
        {
            if (MessageDisplay[4])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.SawToothB_Before;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[4] = false;
            }
        }

        if (Col.gameObject.name == "Nut")
        {
            if (MessageDisplay[5])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.ChainNut_Insert;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[5] = false;
            }
        }
        
        if (Col.gameObject.name == "HandleArea")
        {
            if (MessageDisplay[6])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.Handle_Before2;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[6] = false;
            }
        }

        if (Col.gameObject.name == "SawToothA")
        {
            if (MessageDisplay[7])
            {
                if (MainMenu.EffectValue)
                    EnterAreaSound.Play();
                EventType = MessageType.SawToothB_Before;
                EventMessage_Time.OnlyEventActivate = true;
                MessageDisplay[7] = false;
            }
        }

        if (Col.gameObject.tag == "Terrain")
        {
            if(!InLadder)
            {
                Laddering = false;
                OnGround = true;
                Character.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                Character.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }

        //for Climb Ladder
        if (Col.gameObject.tag == "Ladder")
        {
            Character.GetComponent<Rigidbody2D>().isKinematic = true;
            InLadder = true;
        }
    }
    public void OnTriggerStay2D(Collider2D Col) // On ladder
    {
        if (Col.gameObject.name == "OilSoundArea")
        {
            OilSoundArea = true;
        }


        if (Col.gameObject.tag == "LadderAdjust")
        {
            AdjustSector = true;
        }

        if (Col.gameObject.tag == "Terrain")
        {
            if(InLadder)
            {
                Character.GetComponent<Rigidbody2D>().isKinematic = true;
            }

            if(!InLadder)
            {
                Character.GetComponent<Rigidbody2D>().isKinematic = false;
            }

            OnGround = true;
        }
        if (Col.gameObject.tag == "Ladder")
        {
            if(!OnGround)
            {
                Laddering = true;
                Climbing = true;
            }
            InLadder = true;
        }
    }

    public void OnTriggerExit2D(Collider2D Col) 
    {


        if (Col.gameObject.name == "OilSoundArea")
        {
            OilSoundArea = false;
        }

        if (Col.gameObject.name == "Plier")
        {
            PlierArea = false;
        }

        if (Col.gameObject.tag == "LadderAdjust")
        {
            AdjustSector = false;
        }

        if (Col.gameObject.tag == "Terrain")
        {
            if(!InLadder)
                Character.GetComponent<Rigidbody2D>().isKinematic = false;
            OnGround = false;
        }
        if (Col.gameObject.tag == "Ladder")
        {
            Character.GetComponent<Rigidbody2D>().isKinematic = false;
            InLadder = false;
            Climbing = false;
            Laddering = false;
        }
        if (Col.gameObject.tag == "OilArea")
        {
            OilArea = false;
        }
    }

    void Jump()
    {
        //gameObject.transform.Translate(0, 0.7f, 0);
        if(OnGround)
        {
            gameObject.GetComponent<Animator>().SetBool("EnterJump", true);
            gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 250f));
        }
    }
 
}
