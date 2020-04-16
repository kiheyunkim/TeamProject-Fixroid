using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawController : MonoBehaviour
{
    static public SawController GetInstance { get; private set; }
    public UpperSaw UpperSaw { get; private set; }
    public MiddleSaw MiddleSaw { get; private set; }
    public DownerSaw DownerSaw { get; private set; }

    private void Awake()
    {
        UpperSaw = GetComponentInChildren<UpperSaw>();
        DownerSaw = GetComponentInChildren<DownerSaw>();
        MiddleSaw = GetComponentInChildren<MiddleSaw>();
        GetInstance = this;
    }
}
