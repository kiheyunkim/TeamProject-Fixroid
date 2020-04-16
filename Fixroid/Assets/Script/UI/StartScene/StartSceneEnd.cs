﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneEnd : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("End", true);
    }
}
