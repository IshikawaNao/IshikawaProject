using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISelectManager
{
    void StageDecision(int num, Animator anim);
    int SutageNum(float input, int num);
}