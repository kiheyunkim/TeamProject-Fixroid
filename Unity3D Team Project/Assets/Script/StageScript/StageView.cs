using UnityEngine;
using System.Collections;

public class StageView : MonoBehaviour
{
    static public bool ViewActivate;
    static public bool ViewDeActivate;
	// Use this for initialization
	void Start ()
    {
        ViewActivate = false;
        ViewDeActivate = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(ViewActivate)
        {
            gameObject.GetComponent<Animator>().SetBool("Play Animation", true);
            ViewActivate = false;
        }
        if(ViewDeActivate)
        {
            gameObject.GetComponent<Animator>().SetBool("Play Animation", false);
            gameObject.GetComponent<Animator>().SetTrigger("Play Reverse Anime");
            ViewDeActivate = false;
        }
	}
}
