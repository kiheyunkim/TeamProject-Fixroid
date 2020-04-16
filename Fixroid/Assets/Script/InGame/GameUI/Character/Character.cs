  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private AudioSource jumpSound;

    private JoyStick joyStick;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D bodyRigidbody;
    private Animator animator;

    private CameraControl cameraControl;
    private UIController uIController;
    private TimerController timerController;
    private BlurManager blurManager;
    private ProgressManager progressManager;
    private SaveManager saveManager;

    private bool inUp = false;
    private bool inDown = false;
    private bool inLadder = false;
    private bool controlEnable = true;



    private void Awake()
    {
        jumpSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/UI/GUI/JumpButtonSound");

        spriteRenderer = GetComponent<SpriteRenderer>();
        bodyRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        cameraControl = Camera.main.GetComponent<CameraControl>();
    }

    // Use this for initialization
    void Start ()
    {
        uIController = UIController.GetInstance;
        joyStick = uIController.JoyStick;
        timerController = uIController.TimerController;
        blurManager = uIController.BlurManager;

        progressManager = ProgressManager.GetInstance;
        saveManager = SaveManager.GetInstance;
    }

    // Update is called once per frame
    void Update()
    {
        if (!controlEnable) return;

        //Camera 
        cameraControl.Tracing(transform.position);

        spriteRenderer.flipX = joyStick.XValue > 0 ? true : (joyStick.XValue < 0 ? false : spriteRenderer.flipX);

        if (Mathf.Abs(joyStick.XValue) > 0)
        {
            animator.SetTrigger("Walk");
            animator.ResetTrigger("Idle");
            transform.Translate(new Vector3(joyStick.XValue , 0, 0), Space.World);
        }
        else
        {
            animator.ResetTrigger("Walk");
            animator.SetTrigger("Idle");
        }

        if(inLadder)
        {
            bodyRigidbody.bodyType = RigidbodyType2D.Kinematic;
            bodyRigidbody.velocity = Vector2.zero;          //Kinietic 상태에서 낙하 방지
            if (!inUp && !inDown)
                transform.Translate(new Vector3(0, joyStick.YValue, 0), Space.World);
            else
            {
                if (inUp)
                    transform.Translate(new Vector3(0, joyStick.YValue < 0 ? joyStick.YValue : 0, 0), Space.World);
                else if (inDown)
                    transform.Translate(new Vector3(0, joyStick.YValue > 0 ? joyStick.YValue : 0, 0), Space.World);
            }
        }
        else
            bodyRigidbody.bodyType = RigidbodyType2D.Dynamic;

    }

    public void PushJumpButton()
    {
        if (bodyRigidbody.velocity.y != 0) return;

        jumpSound.Play();

        animator.SetTrigger("Jump");
        bodyRigidbody.AddForce(Vector3.up * 5.0f, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("LadderUp"))
            inUp = true;
        if (collision.tag.Equals("LadderDown"))
            inDown = true;
        if (collision.tag.Equals("Ladder"))
            inLadder = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag.Equals("LadderUp"))
            inUp = false;
        if (collision.tag.Equals("LadderDown"))
            inDown = false;
        if (collision.tag.Equals("Ladder"))
            inLadder = false;
    }

    private IEnumerator FinalWalking()
    {
        float step = 0;
        GetComponent<SpriteRenderer>().sortingLayerName = "BCharacter";
        animator.SetTrigger("Walk");
        animator.SetBool("FinalWalking", true);
        timerController.StopTimer();
        
        while (step < 5)
        {
            transform.Translate(new Vector3(0.03f, 0, 0));
            step += 0.1f;
            yield return new WaitForEndOfFrame();
        }

        bool acheiveSucces = false;

        if (!saveManager.SaveTile.acheiveOpen[2] && progressManager.time < 2)
        {
            saveManager.SaveTile.acheiveOpen[2] = true;
            acheiveSucces = true;
        }
        if (!saveManager.SaveTile.acheiveOpen[1] && progressManager.time > 0)
        {
            saveManager.SaveTile.acheiveOpen[1] = true;
            acheiveSucces = true;
        }

        Camera.main.GetComponent<CameraControl>().StepTimeAttackBgm();

        if (acheiveSucces)
            blurManager.StartBlur(BlurManager.RequestType.GameSucessWIthAcheive);
        else
            blurManager.StartBlur(BlurManager.RequestType.GameSucess);
    }

    public void StartFinalWalking()
    {
        controlEnable = false;

        StartCoroutine(FinalWalking());
    }
}
