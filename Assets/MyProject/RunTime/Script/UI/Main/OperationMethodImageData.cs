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
        [Header("‘€ìƒ^ƒCƒv")]
        public OperationType operationType;
        [Header("‘€ìà–¾UI")]
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
