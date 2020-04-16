using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringGame : MonoBehaviour
{
    private List<Sprite> plierSprites = new List<Sprite>();
    private GameObject brokenObject;
    private GameObject fixedObject;

    public GameObject rightSpringPivot;
    public GameObject leftSpringPivot;

    private Vector3 plierOrigin = new Vector3(400f, 0, 0);
    private GameObject plierPrefab;
    private GameObject plier;

    private Vector3 rotateSpeed = new Vector3(0, 0, 10f);
    private Vector3 movingSpeed = new Vector3(0.1f, 0, 0);

    private BlurManager blurManager;
    private CuckooController cuckooController;

    private bool leftEnd = false;
    private bool rightEnd = false;

    private void Awake()
    {
        Canvas canvas = GetComponent<Canvas>();
        canvas.worldCamera = Camera.main.GetComponent<Camera>();

        Sprite[] sprites = Resources.LoadAll<Sprite>("Stage/Stage1/MiniGame/SpringGame");
        for (int i = 4; i < sprites.Length; i++)
            plierSprites.Add(sprites[i]);

        brokenObject = transform.GetChild(0).gameObject;
        fixedObject = transform.GetChild(1).gameObject;
        fixedObject.SetActive(false);

        rightSpringPivot = transform.GetChild(2).gameObject;
        leftSpringPivot = transform.GetChild(3).gameObject;

        plierPrefab = Resources.Load<GameObject>("Prefab/MiniGame/SpringPlier");
        plier = Instantiate(plierPrefab, transform, false);
        plier.transform.localPosition = plierOrigin;
    }

    private void Start()
    {
        blurManager = UIController.GetInstance.BlurManager;
        cuckooController = AreaController.GetInstance.CuckooController;
    }

    private IEnumerator Cutting(bool isLeft)
    {
        UnityEngine.UI.Image plierImage = plier.GetComponent<UnityEngine.UI.Image>();

        for (int i = 0; i < plierSprites.Count; i++)
        {
            plierImage.sprite = plierSprites[i];
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(2f);
        StartCoroutine(Disappear());
        StartCoroutine(ThrowAway(isLeft));
    }

    private IEnumerator ThrowAway(bool isLeft)
    {
        GameObject target = isLeft ? leftSpringPivot : rightSpringPivot;
        UnityEngine.UI.Image image = target.GetComponentInChildren<UnityEngine.UI.Image>();

        float step = 1.0f;

        while (step > 0.0f)
        {
            image.color = new Color(1, 1, 1, step);
            step -= 0.02f;
            image.transform.Rotate(isLeft ? rotateSpeed : -rotateSpeed);
            target.transform.Translate(isLeft ? -movingSpeed : movingSpeed);

            yield return new WaitForSeconds(0.02f);
        }

        Destroy(isLeft ? leftSpringPivot : rightSpringPivot);
        Destroy(plier);

        if (isLeft ? IsEnd(true, false) : IsEnd(false, true))
        {
            brokenObject.SetActive(false);
            fixedObject.SetActive(true);
            cuckooController.ChangeFixState();
            yield return new WaitForSeconds(1f);

            blurManager.CloseBlur(BlurManager.RequestType.Spring, BlurManager.EndType.MinigameSuccess);
            Destroy(gameObject);
        }
        else
        {
            plier = Instantiate(plierPrefab, transform, false);
            plier.transform.localPosition = plierOrigin;
        }

        yield break;
    }

    private IEnumerator Disappear()
    {
        UnityEngine.UI.Image plierImage = plier.GetComponent<UnityEngine.UI.Image>();

        float step = 1.0f;
        while (step > 0)
        {
            step -= 0.1f;
            plierImage.color = new Color(1, 1, 1, step);
            yield return new WaitForEndOfFrame();
        }

        yield break;
    }

    public void IsCollision(bool isLeft)
    {
        StartCoroutine(Cutting(isLeft)); 
    }

    public bool IsEnd(bool left,bool right)
    {
        if (left)
            leftEnd = true;
        if (right)
            rightEnd = true;

        if(leftEnd && rightEnd)
            return true;

        return false;
    }
}
