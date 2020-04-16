using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkController : MonoBehaviour
{
    public enum Status { Standby, Start };
    public Status status;

    private SpriteRenderer sprite;
    private bool start = false;
    private bool appear = true;

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.color = new Color(1, 1, 1, 0);
    }

    private void Start()
    {
        switch (status)
        {
            case Status.Start:
                BlinkStart();
                break;
            case Status.Standby:
            default:
                break;
        }
    }

    void Update()
    {
        if (!start) return;

        float alpha = sprite.color.a;

        if (appear)
        {
            if (sprite.color.a < 0.99f)
                alpha += 0.4f * Time.deltaTime;
            else
            {
                alpha = 1.0f;
                appear = false;
            }
        }
        else
        {
            if (sprite.color.a > 0.01f)
                alpha -= 0.4f * Time.deltaTime;
            else
            {
                alpha = 0.0f;
                appear = true;
            }
        }

        sprite.color = new Color(1, 1, 1, alpha);
    }

    public void BlinkStart()
    {
        start = true;
    }

    public void BlinkEnd()
    {
        start = false;
        sprite.color = new Color(1, 1, 1, 1);
    }
}