using UnityEngine;

public class ProgressManager : Manager {
    private float achievedTime;
    private int achievedHarvest;
    private float actualTime;

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<ProgressManager>(this);
    }

    private void Start() {
        var service = ServiceManager.Instance;
        var saveManager = service.GetManager<SaveManager>();
        var uiManager = service.GetManager<UIManager>();
        var uiHandler = uiManager.GetProgressUIHandler();

        uiHandler.OnStart();

        saveManager.OnSave += () => {
            saveManager.SaveData.ProgressData.AchievedTime = achievedTime;
            saveManager.SaveData.ProgressData.AchievedHarvest = achievedHarvest;
        };
        saveManager.OnLoad += () => {
            achievedTime = saveManager.SaveData.ProgressData.AchievedTime;
            achievedHarvest = saveManager.SaveData.ProgressData.AchievedHarvest;
            uiHandler.OnStart();
        };
    }

    public void AchieveTime() {
        achievedTime += Time.deltaTime;
        actualTime += Time.deltaTime;
    }
    public void AchieveHarvest(int amount) => achievedHarvest += amount;
    public float GetAchievedTime() => achievedTime;
    public int GetAchievedHarvest() => achievedHarvest;
    public void ResetActualTime() => actualTime = 0f;
    public float GetActualTime() => actualTime;
}