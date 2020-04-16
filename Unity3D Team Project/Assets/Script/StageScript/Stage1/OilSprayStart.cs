using UnityEngine;
using System.Collections;

public class OilSprayStart : MonoBehaviour
{
    //Global Trigger boolean
    static public bool Change;//true -> change, false -> No Change


    //For Sound
    public AudioSource NoItemSound;

    private bool ChangeEnd;
    private bool StartRotation;
    public Sprite OiledChainSaw;
    public GameObject TargethainSaw;

	void Start ()
    {
        Change = false;
        ChangeEnd = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if(Change)
        {
            ChangeEnd = true;
            TargethainSaw.GetComponent<SpriteRenderer>().sprite = OiledChainSaw;
            TargethainSaw.GetComponent<BoxCollider2D>().enabled = false;
            HandScript.EquippedItem = Stage1UI.Item.Basic;
            StartRotation = true;
            Change = false;
        }
            

        if(StartRotation)
        {
            TerrainMovement.OilMiniGameSuccess = true;
            TargethainSaw.transform.Rotate(new Vector3(0, 0, -9.666666666666666f * Time.deltaTime * 300 / 20));
        }
	}
    void OnMouseDown()
    {
        if(!ChangeEnd)
        {
            if(HandScript.EquippedItem==Stage1UI.Item.Oil_Full)
            {
                if (MainMenu.EffectValue)
                    NoItemSound.Play();
                Change = true;
            }
            else
            {
                HandScript.isNotMatch = true;
                HandScript.Blink = true;
            }
        }
    }
}
