using System;

[Serializable]
public class SaveData 
{
    // ミキサー初期値
    public float masterVol;
    public float bgmVol;
    public float seVol;

    public float sensitivity;

    public float ClearTime1;
    public float ClearTime2;
    public string ClearRank1;
    public string ClearRank2;

    public bool FullScreen;
}
