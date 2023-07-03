using UnityEngine;

public class TitleStateView 
{
    // View•ÏX
    public void ChangeState(GameObject[] obj, int stateNum)
    {
        for (int i = 0; i < obj.Length; i++)
        {
            obj[i].SetActive(false);
            if (i == stateNum)
            {
                obj[stateNum].SetActive(true);
            }
        }
    }
}
