using UnityEngine;
using System.Collections;

public class Choo : MonoBehaviour
{
    //global trigger boolean
    static public bool StartChoo;
    public bool Initialization;
    public bool Circle;

    //Changing
    public Sprite OilChain;
    public Sprite Chain;
    public GameObject[] BeforeChainGroup = new GameObject[4];
    public GameObject[] AfterChainGroup = new GameObject[4];

    private bool[] up = new bool[4];
    public float Speed;
    public GameObject[] Chooo;

    void Start()
    {
        StartChoo = false;
        Circle = false;
        Initialization = false;
        for (int i = 0; i < up.Length; ++i)
            up[i] = false;
    }

    void Update()
    {
        if(StartChoo)
        {
            Rotation.SmallSawTooth_Activate = true;
            Initialization = true;
            StartChoo = false;
        }
        if(Initialization)
        {
            for (int i = 0; i < Chooo.Length - 1; i++)
            {
                Chooo[i].transform.Translate(0, -Time.deltaTime * Speed * (i / 3 + 1), 0);
            }
            if (Chooo[0].transform.position.y < -22.5f)
            {
                for (int i = 0; i < 3; i++)
                {
                    Destroy(BeforeChainGroup[i]);
                    AfterChainGroup[i].SetActive(true);
                }
                Circle = true;
                Initialization = false;
            }
        }
        if(Circle)
        {
            //for first ~ third Choo
            for (int i = 0; i < Chooo.Length-1; i++)
            {
                if (Chooo[i].transform.position.y < -24.6f)
                {
                    up[i] = true;
                }
                else
                if (Chooo[i].transform.position.y > -22.55f)
                {
                    up[i] = false;
                }
            }

            //for Fourth Choo
            if (Chooo[3].transform.position.y < -24.0f)
            {
                up[3] = true;
            }
            else
            if (Chooo[3].transform.position.y > -21f)
            {
                up[3] = false;
            }

            if (up[3] && Chooo[3].transform.position.y > -22.3f)
            {
                BeforeChainGroup[3].SetActive(true);
                AfterChainGroup[3].SetActive(false);
                //짧은거 켜고 긴거 끄그
            }
            else
            if (!up[3] && Chooo[3].transform.position.y < -22.5f)
            {
                BeforeChainGroup[3].SetActive(false);
                AfterChainGroup[3].SetActive(true);
                //반대로
            }



            //Choo Movement
            for (int i = 0; i < Chooo.Length; i++)
            {
                if (up[i])
                {
                    if (i == 3)
                    {
                        Chooo[i].transform.Translate(0, Time.deltaTime * Speed * 2, 0);
                    }
                    else
                    {
                        Chooo[i].transform.Translate(0, Time.deltaTime * Speed * (i + 2.5f), 0);
                    }
                }
                else
                if (up[i] == false)
                {
                    if (i == 3)
                    {
                        Chooo[i].transform.Translate(0, -Time.deltaTime * Speed * 2, 0);
                    }
                    else
                    {
                        Chooo[i].transform.Translate(0, -Time.deltaTime * Speed * (i + 2.5f), 0);
                    }
                }
            }

        }
    }
}