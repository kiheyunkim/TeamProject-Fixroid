using UnityEngine;
using System.Collections;

public class TerrainMovement : MonoBehaviour
{
    static public bool OilMiniGameSuccess;
    public GameObject Terrain;

    // Update is called once per frame
    void Start()
    {
        OilMiniGameSuccess = false;
    }
    void Update()
    {
        if (OilMiniGameSuccess)
        {
            if (Terrain.transform.position.x < 6.53f)
            {
                OilMiniGameSuccess = false;
            }
            else
            {
                Terrain.transform.Translate(-1.0f * Time.deltaTime, 0, 0);
            }
        }
    }
}
