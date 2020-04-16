using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldItemController : MonoBehaviour
{
    public enum FieldItemType { SawTeeth, Nut, Spray };
    public FieldItemType itemType;

    private SpriteRenderer spriteRenderer;
    private PhrasesController phrasesController;
    private ProgressManager progressManager;

    private Vector3 stdPos;
    private readonly float hightPoint = 0.1f;
    private float currentPoint = 0.0f;
    private bool isUp = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        progressManager = ProgressManager.GetInstance;

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/FieldItem");
        switch (itemType)
        {
            case FieldItemType.SawTeeth:
                spriteRenderer.sprite = sprites[0];
                break;
            case FieldItemType.Nut:
                spriteRenderer.sprite = sprites[1];
                break;
            case FieldItemType.Spray:
                spriteRenderer.sprite = sprites[2];
                break;
        }

        BoxCollider2D collider = gameObject.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;

        stdPos = transform.position;
        currentPoint = stdPos.y;
    }

    private void Start()
    {
        phrasesController = UIController.GetInstance.PhrasesController;
        StartCoroutine(Floating());
    }

    protected IEnumerator Floating()
    {
        while (true)
        {
            currentPoint = isUp ? currentPoint + 0.005f : currentPoint - 0.005f;

            transform.position = new Vector3(stdPos.x, currentPoint, stdPos.z);

            if (transform.position.y > stdPos.y + hightPoint)
                isUp = false;
            else if (transform.position.y < stdPos.y)
                isUp = true;

            yield return new WaitForSecondsRealtime(0.1f);
        }
    }

    private void OnMouseUp()
    {
        if (progressManager.IsGamePlaying) return;

        if (!progressManager.IsReady) return;

        if (progressManager.IsItemWindowOpen) return;

        switch (itemType)
        {
            case FieldItemType.SawTeeth:
                progressManager.AddItem(HandButton.ItemType.SawA);
                phrasesController.StartPhrase(PhrasesController.ItemGetNotify.SawA);
                break;
            case FieldItemType.Nut:
                progressManager.AddItem(HandButton.ItemType.Nut);
                phrasesController.StartPhrase(PhrasesController.ItemGetNotify.Nut);
                break;
            case FieldItemType.Spray:
                progressManager.AddItem(HandButton.ItemType.Spray);
                phrasesController.StartPhrase(PhrasesController.ItemGetNotify.Spray);
                break;
        }

        Destroy(gameObject);
    }

}
