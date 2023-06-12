using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu]
public class TutorialData : ScriptableObject
{
    public List<DataList> Data;

    [Serializable]
    public class DataList
    {
        [Header("������")]
        public string explanatory;
        [Header("�`���[�g���A���摜")]
        public Sprite sprite;
    }
}
