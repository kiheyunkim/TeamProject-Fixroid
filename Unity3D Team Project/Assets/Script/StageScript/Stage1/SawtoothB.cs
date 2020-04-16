using UnityEngine;
using System.Collections;

public class SawtoothB : MonoBehaviour
{
    //for Sound
    public AudioSource NoItemSound;

    public GameObject TargetPin;
    public GameObject TargetSawTooth;
    public Sprite Sawtooh;
    private bool Change;
    private bool Set;
    private bool Rotation;

    void Start()
    {
        Change = false;
        Set = false;
    }
    void Update()
    {
        if(Change)
        {
            CuckooControl.SawB = true;
            TargetSawTooth.GetComponent<SpriteRenderer>().sprite = Sawtooh;
            Rotation = true;
            Change = false;
        }
        if(Rotation)
        {
            TargetSawTooth.transform.Rotate(0, 0, Time.deltaTime * 40f);
        }
    }
    void OnMouseDown()
    {
        if(!Set)
        {
            if(Stage1UI.FindItem(Stage1UI.Item.ChainSaw2))
            {
                if(MainMenu.EffectValue)
                    NoItemSound.Play();
                Stage1UI.DeleteItem(Stage1UI.Item.ChainSaw2);
                Change = true;
                Set = true;
            }
        }
    }
}
