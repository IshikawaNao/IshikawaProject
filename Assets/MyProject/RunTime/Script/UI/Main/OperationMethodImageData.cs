using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class OperationMethodImageData : ScriptableObject
{
    public List<DataList> Data;

    [Serializable]
    public class DataList
    {
        [Header("操作タイプ")]
        public OperationType operationType;
        [Header("操作説明UI")]
        public Sprite[] sprite;
    }
}
public enum OperationType
{
    Move,
    Decision,
    Return,
    PushAction,
    ClimbAction,
    SonarKey,
    CameraReset,
}
