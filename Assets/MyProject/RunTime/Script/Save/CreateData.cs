using System.IO;
using UnityEngine;

public class CreateData : MonoBehaviour
{
    public void SaveData(SaveData save)
    {
        StreamWriter writer;
        string json = JsonUtility.ToJson(save);
        writer = new StreamWriter(Application.dataPath + "/savedata.json", false);

        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public SaveData loadData()
    {
        string save = "";
        StreamReader reader;
        reader = new StreamReader(Application.dataPath + "/savedata.json");
        save = reader.ReadToEnd();
        reader.Close();

        return JsonUtility.FromJson<SaveData>(save);
    }
}
