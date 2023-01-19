using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;

// �Z�[�u�f�[�^�Ǘ��N���X
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

    // �t�@�C���X�V����
    void InitFileSave()
    {
        bf = new BinaryFormatter();
        file = File.Create(filePath);
    }

    // �t�@�C�����[�h����
    void InitFileLoad()
    {
        bf = new BinaryFormatter();
        file = File.Open(filePath, FileMode.Open);
    }

    // �t�@�C���N���[�Y
    void CloseFile()
    {
        file.Close();
        file = null;
    }

    // �t�@�C�����݃`�F�b�N
    public bool SaveDataCheck()
    {
        if (File.Exists(filePath)) { return true; }
        return false;
    }

    // �V�K�f�[�^�쐬
    public void CreateSaveData()
    {
        try
        {
            InitFileSave();

            // �Z�[�u�f�[�^�쐬(��X�ǋL)
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
            // FileStream���g�p������Ō��Close����
            if(file != null) { CloseFile(); }
        }
    }

    // �f�[�^�Z�[�u(�ꊇ)
    // ���f�[�^�ǉ����邽�ђǋL
    public void Save(float vm, float vb, float vs)
    {
        try
        {
            InitFileSave();

            // �Z�[�u�f�[�^�쐬(��X�ǋL)
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // �S�f�[�^���[�h
    // ���f�[�^�ǉ����邽�ђǋL
    public void LoadData(ref float vm, ref float vb, ref float vs)
    {
        try
        {
            InitFileLoad();

            // �Z�[�u�f�[�^�ǂݍ���
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // ���ʂ̃Z�[�u
    public void VolumeSave(float vm, float vb, float vs)
    {
        try
        {
            InitFileSave();

            // �Z�[�u
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // �J�������x�Z�[�u
    public void SensitivitySave(float sen)
    {
        try
        {
            InitFileSave();

            // �Z�[�u
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // �N���A���Z�[�u
    public void SaveClearData(float time, string rank, int num)
    {
        try
        {
            InitFileSave();

            // �Z�[�u
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // �}�X�^�[���ʂ̃��[�h
    public void LoadVol(ref float vm, ref float vb, ref float vs)
    {
        try
        {
            InitFileLoad();

            // �Z�[�u�f�[�^�ǂݍ���
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // �J�������x
    public void LoadSensitivity(ref float sen)
    {
        try
        {
            InitFileLoad();

            // �Z�[�u�f�[�^�ǂݍ���
            SaveData data = bf.Deserialize(file) as SaveData;
            sen = data.sensitivity;
        }
        catch (IOException)
        {
            Debug.LogError("failed to open file");
        }
        finally
        {
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }

    // �N���A��񃍁[�h
    public void LoadClearData(ref float time, ref string rank, int num)
    {
        try
        {
            InitFileLoad();

            // �Z�[�u�f�[�^�ǂݍ���
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
            // FileStream���g�p������Ō��Close����
            if (file != null) { CloseFile(); }
        }
    }
}
