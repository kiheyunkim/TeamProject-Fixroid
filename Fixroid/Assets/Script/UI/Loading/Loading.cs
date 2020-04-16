using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Loading : MonoBehaviour
{
    private string[] LoadingStr = new string[4] { "Loading", "Loading.", "Loading..", "Loading..." };
    private UnityEngine.UI.Text text;

    protected IEnumerator LoadingText()
    {
        int strIndex = 0;

        while(true)
        {
            text.text = LoadingStr[strIndex];
            strIndex = strIndex + 1 > 3 ? 0 : strIndex + 1;
            yield return new WaitForSeconds(0.05f);
        }
    }
	// Use this for initialization
	void Start ()
    {
        text = GetComponent<UnityEngine.UI.Text>();
        StartCoroutine(LoadingText());
	}
}
