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

    private static CreateData instance;
    public static CreateData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (CreateData)FindObjectOfType(typeof(CreateData));

                if (instance == null)
                {
                    Debug.LogError(typeof(CreateData) + "is nothing");
                }
            }
            return instance;
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        filePath = Application.dataPath + FileName;

        if(!SaveDataCheck())
        {
            CreateSaveData();
        }

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
            data.ClearTime1 = DefaultClearTime; 
            data.ClearTime2 = DefaultClearTime; 
            data.ClearRank1 = DefaultRank; 
            data.ClearRank2 = DefaultRank; 
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
    public void Save(float vm, float vb, float vs, float sn, float ct1, float ct2, string cr1, string cr2)
    {
        try
        {
            InitFileSave();

            // セーブデータ作成(後々追記)
            SaveData data = new SaveData();
            data.masterVol = vm;
            data.bgmVol = vb;
            data.seVol = vs;
            data.sensitivity = sn;
            data.ClearTime1 = ct1;
            data.ClearTime2 = ct2;
            data.ClearRank1 = cr1;
            data.ClearRank2 = cr2;
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
            if (num == 0)
            {
                data.ClearTime1 = time;
                data.ClearRank1 = rank;
            }
            else if (num == 1)
            {
                data.ClearTime2 = time;
                data.ClearRank2 = rank;
            }
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
            if (num == 0)
            {
                time = data.ClearTime1;
                rank = data.ClearRank1;
            }
            else if(num == 1)
            {
                time = data.ClearTime2;
                rank = data.ClearRank2;
            }
            
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
