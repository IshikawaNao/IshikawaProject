using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

// セーブデータ管理クラス
public class CreateData : MonoBehaviour
{
    const string FileName = "/savedata.dat";

    const float DefaultMasterVol = 1;
    const float DefaultBGMVol = 0.5f;
    const float DefaultSEVol = 0.5f;

    const float DefaultSensitivity = 0.1f;

    const float DefaultClearTime = 0;

    const string DefaultRank = "";

    FileStream file;
    BinaryFormatter bf;
    string filePath;

    public static CreateData Instance { get; private set; }
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
            return;
        }

        filePath = Application.dataPath + FileName;
    }

    // ファイル更新準備
    void InitFileSave()
    {
        bf = new BinaryFormatter();
        file = File.Create(filePath);
    }

    // ファイルロード準備
    void InitFileLoad()
    {
        bf = new BinaryFormatter();
        file = File.Open(filePath, FileMode.Open);
    }

    // ファイルクローズ
    void CloseFile()
    {
        file.Close();
        file = null;
    }

    // ファイル存在チェック
    public bool SaveDataCheck()
    {
        if (File.Exists(filePath)) { return true; }
        return false;
    }

    // 新規データ作成
    public void CreateSaveData()
    {
        try
        {
            InitFileSave();

            // セーブデータ作成(後々追記)
            SaveData data = new SaveData();
            data.masterVol = DefaultMasterVol;
            data.bgmVol = DefaultBGMVol;
            data.seVol = DefaultSEVol;
            data.sensitivity = DefaultSensitivity;
            data.ClearTime[0] = DefaultClearTime; 
            data.ClearTime[1] = DefaultClearTime; 
            data.ClearRank[0] = DefaultRank; 
            data.ClearRank[1] = DefaultRank; 
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if(file != null) { CloseFile(); }
        }
    }

    // データセーブ(一括)
    // ＊データ追加するたび追記
    public void Save(float vm, float vb, float vs)
    {
        try
        {
            InitFileSave();

            // セーブデータ作成(後々追記)
            SaveData data = new SaveData();
            data.masterVol = vm;
            data.bgmVol = vb;
            data.seVol = vs;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // 全データロード
    // ＊データ追加するたび追記
    public void LoadData(ref float vm, ref float vb, ref float vs)
    {
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            vm = data.masterVol;
            vb = data.bgmVol;
            vs = data.seVol;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // 音量のセーブ
    public void VolumeSave(float vm, float vb, float vs)
    {
        try
        {
            InitFileSave();

            // セーブ
            SaveData data = new SaveData();
            data.masterVol = vm;
            data.bgmVol = vb;
            data.seVol = vs;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // カメラ感度セーブ
    public void SensitivitySave(float sen)
    {
        try
        {
            InitFileSave();

            // セーブ
            SaveData data = new SaveData();
            data.sensitivity = sen;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // クリア情報セーブ
    public void SaveClearData(float time, string rank, int num)
    {
        try
        {
            InitFileSave();

            // セーブ
            SaveData data = new SaveData();
            data.ClearTime[num] = time;
            data.ClearRank[num] = rank;
            bf.Serialize(file, data);
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // マスター音量のロード
    public void LoadVol(ref float vm, ref float vb, ref float vs)
    {
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            vm = data.masterVol;
            vb = data.bgmVol;
            vs = data.seVol;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // カメラ感度
    public void LoadSensitivity(ref float sen)
    {
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            sen = data.sensitivity;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }

    // クリア情報ロード
    public void LoadClearData(ref float time, ref string rank, int num)
    {
        try
        {
            InitFileLoad();

            // セーブデータ読み込み
            SaveData data = bf.Deserialize(file) as SaveData;
            time = data.ClearTime[num];
            rank = data.ClearRank[num];
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStreamを使用したら最後にCloseする
            if (file != null) { CloseFile(); }
        }
    }
}
