using UnityEngine;
using System.Collections;

public class ThridSectorEnter : MonoBehaviour
{
    //Global Trigger Boolean
    static public bool ThirdSector;

    //For Sound
    public AudioSource ChainDrop;

    public GameObject Expression;
    public GameObject SubExpression;
    public Sprite Normal;

    public GameObject ItemNut;
	// Use this for initialization
	void Start ()
    {
        SubExpression.GetComponent<SpriteRenderer>().sprite = null;
        ThirdSector = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(ThirdSector)
        {
            if (MainMenu.EffectValue)
                ChainDrop.Play();
            ItemNut.SetActive(true);
            Expression.GetComponent<SpriteRenderer>().sprite = Normal;
            Expression.GetComponent<BoxCollider2D>().enabled = true;
            SubExpression.GetComponent<SpriteRenderer>().sprite = Normal;
            SubExpression.SetActive(true);
            ThirdSector = false;
        }
	}
}
