    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

class Star
{
    public GameObject startObject;
    public bool Appear { get; set; }
    public bool IsDead { get; set; }

    public Star(Sprite starImage, Transform parent)
    {
        startObject = new GameObject();
        startObject.transform.parent = parent;
        startObject.transform.localPosition = new Vector3(Random.Range(-6.40f, 6.40f), Random.Range(-3.60f, 3.60f), 0);

        SpriteRenderer renderer = startObject.AddComponent<SpriteRenderer>();
        renderer.sortingOrder = 1;
        renderer.sprite = starImage;
        renderer.color = new Color(1, 1, 1, Random.Range(0, 0.5f));
        Appear = true;
        IsDead = false;
    }
}

public class StarController : MonoBehaviour
{
    private List<Star> stars = new List<Star>();
    private Sprite starImage;

    private const int maxStarCount = 50;

    private void Awake()
    {
        starImage = Resources.LoadAll<Sprite>("Start/StartIntegrate")[3];

        for (int i = 0; i < maxStarCount; i++)
            stars.Add(new Star(starImage, transform));
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var star in stars)
        {
            SpriteRenderer renderer = star.startObject.GetComponent<SpriteRenderer>();
            float alpha = renderer.color.a;

            if (star.Appear)
            {
                if (alpha < 0.99f)
                    renderer.color = new Color(1, 1, 1, alpha + 0.01f);
                else
                    star.Appear = false;
            }
            else
            {
                if (alpha > 0.01f)
                    renderer.color = new Color(1, 1, 1, alpha - 0.01f);
                else
                {
                    star.IsDead = true;
                    Destroy(star.startObject);
                }
            }
        }

        stars.RemoveAll(x => x.IsDead == true);

        for (int i = 0; i < maxStarCount - stars.Count; i++)
            stars.Add(new Star(starImage, transform));
    }
}
