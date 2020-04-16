using UnityEngine;
using System.Collections;

public class Oil : MonoBehaviour
{
    public GameObject OilObject;
    // Use this for initialization
    void Start()
    {
        Stage1UI.OilDrop = true;
    }

    void Update()
    {
        if (Stage1UI.OilDrop)
            Stage1UI.OilDrop = false;
    }
    public void OnTriggerStay2D(Collider2D Col)
    {
        if(Col.tag=="Terrain")
        {
            
            Destroy(OilObject);
        }
    }
	
}
