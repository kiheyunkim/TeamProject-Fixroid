using UnityEngine;
using System.Collections;

public class StartScript : MonoBehaviour
{
    //For Loading
    private string FirstSetting;

    //For ButtonSwitching
    static public bool isTouched = false;
    public float time = 2.0f;
    public GameObject Button;

    //For Quad Movement
    public float speed;
    static public bool isEnd = false;

    //for Sound
    public AudioSource BGM;

    //Movie or Start
    static public bool Movie;
    public bool isMovieEnd;

    //For Skip
    public bool isSkip;
    public bool MovieEnd;

    //For Quad
    public GameObject Quad;
    void Awake()
    {
        if (FirstSetting == "Team6")// 10000 : not first , Anything : First
        {
            LoadInformation.Load_Previous_Setting();
        }
        else
        {
            LoadInformation.Load_First_Setting();
        }
    }

    void Start()
    {
        isMovieEnd = true;
        StartCoroutine(CoroutinePlayMovie());
        MovieEnd = true;
        Movie = false;
    }

    protected IEnumerator CoroutinePlayMovie()
    {
        Handheld.PlayFullScreenMovie("Intro.mp4", Color.black, FullScreenMovieControlMode.CancelOnInput);
        yield return new WaitForSeconds(2.0f); //Allow time for Unity to pause execution while the movie plays.
        isMovieEnd = false;
        BGM.Play(); 
    }


    void Update()
    {
        if(!isMovieEnd)
        {
            if(MovieEnd)
            {
                MovieEnd = false;
                Quad.SetActive(true);
            }
            if (!Movie)
            {
                if (time >= 0)                                                                          //wait for time (sec)
                {
                    time -= Time.deltaTime;
                    return;
                }
                else
                {
                    if (Quad.gameObject.transform.position.y < 51.3f)                                   //Quad Movement
                    {
                        Vector2 offset = new Vector2(0, speed); 
                        Quad.gameObject.transform.Translate(offset);
                    }
                    else
                        isEnd = true;
                }
                if (isTouched)                                                                          //Start Button Switching
                    Destroy(Button);


                if(isSkip)                                                                              //Skip Condition
                {
                    isEnd = true;
                    this.transform.position = new Vector3(0, 51.4f, 0);
                }
            }
        }
    }
  void OnMouseUp()
  {
        if (FirstSetting == "Team6" && MovieEnd)                                                                 //if Not First Time, User can Skip Start Quad Movement
        {
            isSkip = true;
        }
  }
}
