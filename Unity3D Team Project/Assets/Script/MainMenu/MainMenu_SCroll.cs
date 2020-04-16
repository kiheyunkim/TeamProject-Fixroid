using UnityEngine;
using System.Collections;


public class MainMenu_SCroll : MonoBehaviour
{ 
    static public int MidCard = 0;
    public GameObject Camera; // Move Camera
    public GameObject[] Card;

    void Start()
    {
        MidCard = 0 ;// Stating 
    }

    void Update()
    {
        LerpToBttn(Card[MidCard].transform.position.x);
    }

    void LerpToBttn(float position)
    {
        float newX = Mathf.Lerp(Camera.transform.position.x, position, Time.deltaTime * 3f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        Vector2 newPosition = new Vector2(newX, Camera.transform.position.y);
        Camera.transform.position = newPosition;
    }
}
