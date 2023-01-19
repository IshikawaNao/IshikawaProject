using System;

[Serializable]
public class SaveData 
{
    // ミキサー初期値
    public float masterVol;
    public float bgmVol;
    public float seVol;

    public float sensitivity;

    public float[] ClearTime;
    public string[] ClearRank;
}
