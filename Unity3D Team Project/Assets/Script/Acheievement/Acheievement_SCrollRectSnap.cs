using UnityEngine;
using System.Collections;


public class Acheievement_SCrollRectSnap : MonoBehaviour
{
    static public int MidCard;
    public GameObject Quad; // to hold the ScrollPanel
    public GameObject[] Card;

    private int bttnDistance;   //willhold the distance between the buttons

    
    void Start()
    {
        MidCard = 0;//첫 시작 위치.
        //GetComponent distance betwwen buttons
        bttnDistance = (int)Mathf.Abs(Card[1].transform.position.x - Card[0].transform.position.x);//bttnDistance변수에 첫번째와 두번째로 넣은 배열에 버튼 x좌표를 빼서 저장
    }

    void Update()
    {
        LerpToBttn(MidCard * -bttnDistance);
    }

    void LerpToBttn(float position)
    {
        float newX = Mathf.Lerp(Quad.transform.position.x, position, Time.deltaTime * 5f);//선형 보간을 통해서 부드럽게 이동 할 수 있도록 처리한다.
        Vector2 newPosition = new Vector2(newX, Quad.transform.position.y);
        Quad.transform.position = newPosition;
    }
}
