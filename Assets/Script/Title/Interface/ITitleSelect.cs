using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface ITitleSelect
{
    void UISelect(int num);
    void SelecDecision(int num, Animator anim);
    void QuitDecision(int num, Animator anim);
    int QuitNum(float input);
    int SelectNum(float input, int num);
}
