using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPauseSelect
{
    void StageDecision(int num, Animator anim,GameObject option, GameObject Pause);
    int SutageNum(float input, int num);
}
