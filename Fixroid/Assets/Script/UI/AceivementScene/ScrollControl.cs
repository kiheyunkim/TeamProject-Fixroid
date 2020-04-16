using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollControl : MonoBehaviour
{
    public int StandardIndex { get; set; }
    private GameObject quad;
    private GameObject[] cards = new GameObject[6];

    private int bttnDist;
    
	void Awake()
    {
        StandardIndex = 0;
    }

    void Start()
    {
        quad = gameObject;
        for (int i = 0; i < 6; i++)
            cards[i] = transform.GetChild(i).gameObject;

        bttnDist = (int)Mathf.Abs(cards[1].transform.position.x - cards[0].transform.position.x);
    }
	
	void Update ()
    {
        Lerp2Bttn(StandardIndex * -bttnDist);
	}

    void Lerp2Bttn(float position)
    {
        float newX = Mathf.Lerp(quad.transform.position.x, position, Time.deltaTime * 5f);
        quad.transform.position = new Vector2(newX, quad.transform.position.y);
    }
}
