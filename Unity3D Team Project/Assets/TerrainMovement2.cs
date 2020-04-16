using UnityEngine;
using System.Collections;

public class TerrainMovement2 : MonoBehaviour
{
    private bool Direction;
    public GameObject Terrain;
	// Use this for initialization
	void Start ()
    {
        Direction = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Direction)
        {
            Terrain.transform.Translate(new Vector3(1f*Time.deltaTime, 0, 0));
        }

        if (!Direction)
        {
            Terrain.transform.Translate(new Vector3(-1f * Time.deltaTime, 0, 0));
        }

        if (Terrain.transform.position.x < 3.5f)
        {
            Direction = true;
        }
        if (Terrain.transform.position.x > 5.5f)
        {
            Direction = false;
        }
	}
}
