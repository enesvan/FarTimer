using UnityEngine;
using System;
using System.IO;

public class SaveManager : Manager {
    public Action OnSave;
    public Action OnLoad;

    public SaveData SaveData;
    [SerializeField] private string dataPath = "/SaveData";

    [SerializeField] private bool useEncryption = false;
    private const string encryptKeyword = "111111111";

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<SaveManager>(this);

        dataPath = Application.persistentDataPath + dataPath + ".json";
    }

    private void Start() {
        Invoke("Load", 1f);
    }

    [ContextMenu("Save")]
    private void Save() {
        OnSave?.Invoke();
        SaveToFile();
    }

    [ContextMenu("Load")]
    private void Load() {
        if (!File.Exists(dataPath)) return;
        LoadFromFile();
        OnLoad?.Invoke();
    }

    [ContextMenu("Delete")]
    private void Delete() {
        if (File.Exists(dataPath)) File.Delete(dataPath);
    }

    private void SaveToFile() {
        string json = JsonUtility.ToJson(SaveData, true);
        if (useEncryption) json = EncryptOrDecrypt(json);
        File.WriteAllText(dataPath, json);
    }

    private void LoadFromFile() {
        string json = File.ReadAllText(dataPath);
        if (useEncryption) json = EncryptOrDecrypt(json);
        SaveData = JsonUtility.FromJson<SaveData>(json);
    }

    private string EncryptOrDecrypt(string json) {
        string result = "";
        for (int i = 0; i < json.Length; i++) {
            result += (char)(json[i] ^ encryptKeyword[i % encryptKeyword.Length]);
        }
        return result;
    }

    private void OnApplicationQuit() {
        Save();
    }
}

[Serializable]
public class SaveData {
    public TimerData TimerData;
    public ProgressData ProgressData;
}

[Serializable]
public class TimerData {
    public float WorkMinutes;
    public float BreakMinutes;
}

[Serializable]
public class ProgressData {
    public float AchievedTime;
    public int AchievedHarvest;
}