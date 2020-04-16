using UnityEngine;
using System.Collections;

public class MiniGameNut : MonoBehaviour
{
    //for deactivate
    static public bool NutGameActivate;
    public bool MiniGameActivate;
    public GameObject JoyStick;

    //Background
    public GameObject MiniGameBack;
    public GameObject Camera;

    //rotate
    public GameObject Nut_Object;
    private bool FirstTouch;
    private float Angle;
    private float Speed;

    //Animation
    public GameObject Animation;
    public bool isFirstAnime;

    public Sprite Nut;
    public Sprite Nut_Blink;
    

    // Use this for initialization
    void Start ()
    {
        Angle = 0;
        NutGameActivate = false;
        isFirstAnime = true;
	}

    // Update is called once per frame
    void Update()
    {
        if (NutGameActivate)
        {
            MiniGameBack.transform.position = Camera.transform.position;
            if (MiniGameActivate)
            {
                MiniGameBack.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                Nut_Object.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
                VirtualJoyStick.JoystickEnable = false;
                //JumpButton.enabled = false;
                JoyStick.SetActive(false);
            }
            else
            {
                MiniGameBack.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                Nut_Object.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                VirtualJoyStick.JoystickEnable = true;
                //JumpButton.enabled = true;  
                JoyStick.SetActive(true);
            }
            if (FirstTouch)
            {
                Angle += Time.deltaTime * 79.60f;
                Speed = Time.deltaTime * 79.60f;
                Nut_Object.transform.gameObject.transform.Rotate(new Vector3(0, 0, Speed));
                if (Angle >= 87.74f)
                {
                    Angle = 0;
                    FirstTouch = false;
                    Nut_Object.GetComponent<SpriteRenderer>().sprite = Nut;
                    if (Nut_Object.transform.rotation.z >= 180.0f)
                    {
                        Nut_Object.transform.rotation = new Quaternion(0, 0, 0, 0);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && FirstTouch == false)
            {
                FirstTouch = true;
                Nut_Object.GetComponent<SpriteRenderer>().sprite = Nut_Blink;
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (isFirstAnime)
                {
                    NutGameActivate = true;
                    Animation.GetComponent<Animator>().SetBool("NutOn", true);
                    isFirstAnime = false;
                }
            }
        }
    }
}
