using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{
    private AudioSource doorOpenSound;
    private List<GameObject> doorObject = new List<GameObject>();
    private bool isOpen = false;

    private void Awake()
    {
        doorOpenSound = AudioSetter.SetEffect(gameObject, "Sound/Stage1/TimeAttack/DoorOpenSound");
        foreach (Transform transform in transform)
            doorObject.Add(transform.gameObject);
    }

    private IEnumerator Opening(bool up, GameObject target)
    {
        Vector3 speed = new Vector3(0, 0.01f, 0);
        int count = 0;
        while (count<100)
        {
            target.transform.Translate(up ? speed : -speed);
            count++;
            yield return new WaitForSeconds(0.01f);
        }
        isOpen = true;

        yield break;
    }

    public void OpenDoor()
    {
        doorOpenSound.Play();
        StartCoroutine(Opening(true, doorObject[0]));
        StartCoroutine(Opening(false, doorObject[1]));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isOpen)
        {
            if(collision.name == "Character")
                collision.GetComponent<Character>().StartFinalWalking();
        }
    }
}
