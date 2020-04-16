using UnityEngine;
using System.Collections;

public class OilGet : MonoBehaviour
{
    public GameObject Oil_Prefab;
    public GameObject OilStart;
    public GameObject DestoryTarget;
    public float time;
    public bool Clicked;

    //For Sound 
    public AudioSource NoItemSound;
	// Use this for initialization
	void Start ()
    {
        Clicked = false;
        time = 3.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(time>0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            Instantiate<GameObject>(Oil_Prefab).transform.position = OilStart.transform.position;
            time = 3.0f;
        }
	}
    void OnMouseDown()
    {
        if (!Stage1UI.ItemOpen && !Stage1UI.PauseOpen)
        {
            if (!Clicked)
            {
                if (CharacterMoving.OilArea)
                {
                    if (HandScript.EquippedItem != Stage1UI.Item.OilSpray)
                    {
                        HandScript.isNotMatch = true;
                        HandScript.Blink = true;
                    }
                    else
                    {
                        if (MainMenu.EffectValue)
                            NoItemSound.Play();
                        CharacterMoving.EventType = CharacterMoving.MessageType.GetOil;
                        EventMessage_Time.OnlyEventActivate = true;
                        HandScript.EquippedItem = Stage1UI.Item.Basic;
                        Destroy(DestoryTarget);
                        Clicked = true;
                        Stage1UI.OilComplete = true;
                    }
                }
            }

        }
    }
}
