using UnityEngine;
using System.Collections;

public class StartMovement : MonoBehaviour
{
    //For Loading
    private int FirstSetting;

    //For Panel Moving
    public RectTransform Panel; 
    static public bool MoveComplete = false;

    //For Sound
    public AudioSource StartBGM;
    public AudioSource ClickSound;

    void Awake()
    {
        FirstSetting = PlayerPrefs.GetInt("First");//if it is first, load  First Setting
        if (FirstSetting == 1)
            LoadInformation.Load_First_Setting();
        else
            LoadInformation.Load_Previous_Setting();
    }
    void Start()
    {
        StartBGM.Play();   
    }
    void Update()
    {
        if (Panel.position.y < Screen.height*1.388f)//Condition for stop Moving
            LerpToBttn(Screen.height * 1.78f);
      
        if (Panel.position.y > 630)
            MoveComplete = true;    
    }
    void LerpToBttn(float position)
    {
        float newY = Mathf.Lerp(Panel.anchoredPosition.y, position, Time.deltaTime * 0.5f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        Vector2 newPosition = new Vector2( Panel.anchoredPosition.x, newY);
        Panel.anchoredPosition = newPosition;
    }
}
