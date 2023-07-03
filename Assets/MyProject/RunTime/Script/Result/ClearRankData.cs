using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class ClearRankData : ScriptableObject
{
    public List<Rank> Ranks;

    [Serializable]
    public class Rank
    {
        public float STime;
        public float ATime;
        public float BTime;
        public float CTime;
        public string ClearRank(float time)
        {
            if (STime > time)
            {
                return "S";
            }
            else if (ATime > time)
            {
                return "A";
            }
            else if (BTime > time)
            {
                return "B";
            }
            else
            {
                return "C";
            }
        }
    }
}
