using UnityEngine;
using System.Collections;

public class Star : MonoBehaviour
{
    public GameObject StarAnimation;//Animation

    public int NumberOfStar = 10;
    // Use this for initialization
    void Start ()
    {
        /*
        for (int i = 0; i < NumberOfStar; i++)//for Start Animation Position(Random)
        {
            //Vector2 position = new Vector2(Random.Range(0f, 1280.0f), Random.Range(-360.0f, 360.0f));//좌표 설정.
            Vector3 position = new Vector3(Random.Range(Screen.width * 0.1f, Screen.width*0.9f), Random.Range(Screen.height*0.1f,Screen.height*0.9f ),0);
            Instantiate(StarAnimation, position, transform.rotation);
        }
        */
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
