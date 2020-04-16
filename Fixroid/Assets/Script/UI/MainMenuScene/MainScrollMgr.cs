using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScrollMgr : MonoBehaviour
{
    public int StdIndex { get; set; }
    private List<GameObject> cards = new List<GameObject>();
    
    void Awake()
    {
        StdIndex = 0;
        foreach(Transform card in transform)
            cards.Add(card.gameObject);
    }

    void Update()
    {
        Lerp2Bttn(cards[StdIndex].transform.position.x);
    }

    void Lerp2Bttn(float position)
    {
        float newX = Mathf.Lerp(Camera.main.transform.position.x, position, Time.deltaTime * 3f);
        Camera.main.transform.position = new Vector2(newX, Camera.main.transform.position.y);
    }
}
