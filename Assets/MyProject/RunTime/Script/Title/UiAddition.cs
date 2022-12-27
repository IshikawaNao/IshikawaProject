using UnityEngine;

public class UiAddition 
{
    const int inputZero = 0;

    public int Addition(int num, int minNum, int maxNum, float input)
    {
        if (input > inputZero) { num--; }
        else if (input < inputZero) { num++; }
        return Mathf.Clamp(num, minNum, maxNum);
    }
}
