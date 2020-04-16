using UnityEngine;
using System.Collections;

public class TouchArea : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.tag == "Player")
            Debug.Log("Enter");
    }
}
