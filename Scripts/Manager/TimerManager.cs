using UnityEngine;

public class TimerManager : Manager {
    [Header("Values")]
    [SerializeField] private float workMinutes; // 1.5 = 1 minute and 30 seconds
    [SerializeField] private float breakMinutes; // 1.5 = 1 minute and 30 seconds

    private TimerUIHandler uiHandler;

    private bool isStart = false;
    private float remainingTime;

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<TimerManager>(this);
    }

    private void Start() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();
        var saveManager = service.GetManager<SaveManager>();
        uiHandler = uiManager.GetTimerUIHandler();

        uiManager.OnButtonStart += StartTimer;
        uiManager.OnButtonPause += StopTimer;

        uiHandler.OnStart();

        saveManager.OnSave += () => {
            saveManager.SaveData.TimerData.WorkMinutes = workMinutes;
            saveManager.SaveData.TimerData.BreakMinutes = breakMinutes;
        };
        saveManager.OnLoad += () => {
            workMinutes = saveManager.SaveData.TimerData.WorkMinutes;
            breakMinutes = saveManager.SaveData.TimerData.BreakMinutes;
            uiHandler.OnStart();
        };
    }

    private void Update() {
        if (!isStart) return;
        remainingTime -= Time.deltaTime;
        uiHandler.UpdateTimer();
    }

    public void StartTimer() => isStart = true;
    public void StopTimer() => isStart = false;
    public float GetWorkMinutes() => workMinutes;
    public float GetBreakMinutes() => breakMinutes;
    public float GetRemainingTime() => remainingTime;
    public void SetRemainingTime(float time) => remainingTime = time;

    public void SetWorkMinutes(float minutes) {
        workMinutes += minutes;
        if (workMinutes <= 0f) workMinutes = .5f;
        else if (workMinutes > 60f) workMinutes = 60f;
        remainingTime = workMinutes * 60;
        uiHandler.UpdateText();
    }

    public void SetBreakMinutes(float minutes) {
        breakMinutes += minutes;
        if (breakMinutes <= 0f) breakMinutes = .5f;
        else if (breakMinutes > 60f) breakMinutes = 60f;
        remainingTime = breakMinutes * 60;
        uiHandler.UpdateText();
    }
}