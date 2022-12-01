using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public interface ITitleSelect
{
    void UISelect(int num);
    void SelecDecision(int num, GameObject[] ui);
    void QuitDecision(int num, GameObject[] ui);
    int QuitNum(float input);
    int SelectNum(float input, int num);
}
