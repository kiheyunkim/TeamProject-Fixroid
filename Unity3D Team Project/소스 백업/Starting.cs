//완성
using UnityEngine;
using System.Collections;

public class Starting : MonoBehaviour
{
    public Texture BackGroundTexture;

    private int FirstSetting;
    public float AnyKeyPosX;
    public float AnyKeyPosY;

    void Start()
    {
        Screen.SetResolution(1280, 720, true);//for resoulution
        FirstSetting = PlayerPrefs.GetInt("First");//if it is first, load  First Setting
        if (FirstSetting == 0)
            LoadInformation.Load_First_Setting();
        else
            LoadInformation.Load_Previous_Setting();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))//press any key 
        {
            print(" Enter MainMenu");
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }
    }
    void OnGUI()
    {
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), BackGroundTexture);//for Background
    }
	
}
