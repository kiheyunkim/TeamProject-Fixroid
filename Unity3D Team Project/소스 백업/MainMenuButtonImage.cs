using UnityEngine;
using System.Collections;

public class MainMenuButtonImage : MonoBehaviour
{


    static public int ImageLength;       //count for locked
    void Awake()
    {
        
    }
    void Start()
    {
        

    }
    void Update()
    {
       
    }
    public void Click1Stage()
    {
        if(Input.GetMouseButtonDown(0))
        UnityEngine.SceneManagement.SceneManager.LoadScene("Stage1");
    }
}
