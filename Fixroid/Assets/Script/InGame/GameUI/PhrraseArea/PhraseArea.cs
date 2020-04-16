using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhraseArea : MonoBehaviour
{
    private PhrasesController phrasesController;
    public PhrasesController.AreaNotify areaType;

	// Use this for initialization
	void Start ()
    {
        phrasesController = UIController.GetInstance.PhrasesController;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Character")
        {
            phrasesController.StartPhrase(areaType);
            Destroy(gameObject);
        }
    }
}
