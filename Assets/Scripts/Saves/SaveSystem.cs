using UnityEngine;

public class SaveSystem
{
    private SavesData savesData;
    private const string SAVES_DATA_PATH = "/SavesData.json";

    public SavesData SavesData => savesData;

    public SaveSystem()
    {
        savesData = new SavesData();

        LoadFromJson();
    }

    public void SaveToJson()
    {
        string data = JsonUtility.ToJson(savesData);
        string path = Application.persistentDataPath + SAVES_DATA_PATH;
        System.IO.File.WriteAllText(path, data);
    }

    public void LoadFromJson()
    {
        string path = Application.persistentDataPath + SAVES_DATA_PATH;
        string data = "";

        try
        {
            data = System.IO.File.ReadAllText(path);
        }
        catch
        {
            NewProgress();
            return;
        }

        savesData = JsonUtility.FromJson<SavesData>(data);
    }

    public void NewProgress()
    {
        savesData = new SavesData();
        SaveToJson();
    }
}