using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaController : MonoBehaviour
{
    static public AreaController GetInstance { get; private set; }
    public LadderController LadderController { get; private set; }
    public HandleRotation HandleRotation { get; private set; }
    public CuckooController CuckooController { get; private set; }
    public FinalDoor FinalDoor { get; private set; }
    public SwitchSaw SwitchSaw { get; private set; }
    public TerrainChange TerrainChange { get; private set; }

    private void Awake()
    {
        LadderController = GetComponentInChildren<LadderController>();
        HandleRotation = GetComponentInChildren<HandleRotation>();
        CuckooController = GetComponentInChildren<CuckooController>();
        FinalDoor = GetComponentInChildren<FinalDoor>();
        SwitchSaw = GetComponentInChildren<SwitchSaw>();
        TerrainChange = GetComponentInChildren<TerrainChange>();
        GetInstance = this;
    }
}
