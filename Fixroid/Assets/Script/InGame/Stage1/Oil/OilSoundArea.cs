using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OilSoundArea : MonoBehaviour
{
    public bool IsOilSoudArea { get; private set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character")
            IsOilSoudArea = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsOilSoudArea = false;
    }
}
