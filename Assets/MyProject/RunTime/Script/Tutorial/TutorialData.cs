using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.Video;

[CreateAssetMenu]
public class TutorialData : ScriptableObject
{
    public List<DataList> Data;

    [Serializable]
    public class DataList
    {
        [Header("説明文")]
        public string explanatory;
        [Header("チュートリアル画像")]
        public Sprite sprite;
        [Header("チュートリアル動画")]
        public VideoClip clip;
    }
}
