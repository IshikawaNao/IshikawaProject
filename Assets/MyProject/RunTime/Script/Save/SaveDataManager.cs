using System.IO;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager Instance { get; private set; }

    float masterVol = 0.5f;
    float bgmVol = 0.5f;
    float seVol = 0.5f;
    float sensitivity = 0.5f;
    float clearTime1;
    float clearTime2;
    string rank1;
    string rank2;
    bool screenSize;

    public float MasterVol { get=>masterVol; }
    public float BGMVol { get=>bgmVol; }
    public float SEVol { get=>seVol; }
    public float Sensitivity { get=>sensitivity; }
    public float ClearTime1 { get=>clearTime1; }
    public float ClearTime2 { get=>clearTime2; }
    public string Rank1 { get=>rank1;  }
    public string Rank2 { get=>rank2;  }
    public bool ScreenSize { get=>screenSize; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    /// <summary> マスターボリューム値変更</summary>
    public void MasterVolSave(float _masterVol)
    {
        masterVol = _masterVol;
    }
    /// <summary> BGMボリューム値変更</summary>
    public void BGMVolSave(float _bgmVol)
    {
        bgmVol = _bgmVol;
    }
    /// <summary> SEボリューム値変更</summary>
    public void SEVolSave(float _seVol)
    {
        seVol = _seVol;
    }
    /// <summary> カメラ感度値変更</summary>
    public void SensitivitySave(float _sensitivity)
    {
        sensitivity = _sensitivity;
    }
    /// <summary> クリアタイム値変更</summary>
    public void ClearTime1Save(float _clearTime1)
    {
        clearTime1 = _clearTime1;
    }
    /// <summary> クリアタイム値変更</summary>
    public void ClearTime2Save(float _clearTime2)
    {
        clearTime1 = _clearTime2;
    }
    /// <summary> クリアランク値変更</summary>
    public void Rank1Save(string _rank1)
    {
        rank1 = _rank1;
    }
    /// <summary> クリアランク値変更</summary>
    public void Rank2Save(string _rank2)
    {
        rank1 = _rank2;
    }
    /// <summary> スクリーンサイズ変更</summary>
    public void ScreenSize2Save(bool _screenSize)
    {
        screenSize = _screenSize;
    }

    public void Save()
    {
        string path = Application.dataPath + "/save.txt";
        string data = $"{MasterVol},{BGMVol},{SEVol},{Sensitivity},{ClearTime1},{ClearTime2},{Rank1},{Rank2},{ScreenSize}";
        File.WriteAllText(path, data);
    }

    public void Load()
    {
        string path = Application.dataPath + "/save.txt";
        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            string[] values = data.Split(',');

            masterVol = float.Parse(values[0]);
            bgmVol = float.Parse(values[1]);
            seVol = float.Parse(values[2]);
            sensitivity = float.Parse(values[3]);
            clearTime1 = float.Parse(values[4]);
            clearTime2 = float.Parse(values[5]);
            rank1 = values[6];
            rank2 = values[7];
            screenSize = bool.Parse(values[8]);
        }
    }

    public void DeleteSave()
    {
        string path = Application.dataPath + "/save.txt";
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }
}
