using System;
using UnityEngine;

public class UIManager : Manager {

    #region Event Handler
    // Control Events
    public Action OnButtonStart;
    public Action OnButtonPause;
    public Action OnButtonStop;
    public Action OnButtonSound;

    // Settings Events
    public Action OnButtonSettings;
    public Action OnButtonApply;
    public Action OnButtonMinutesUp;
    public Action OnButtonMinutesDown;
    public Action OnButtonSecondsUp;
    public Action OnButtonSecondsDown;

    // Harvest Events
    public Action OnButtonHarvest;
    public Action OnHarvestable;

    public void OnClickStartButton() => OnButtonStart?.Invoke();
    public void OnClickPauseButton() => OnButtonPause?.Invoke();
    public void OnClickStopButton() => OnButtonStop?.Invoke();
    public void OnClickSoundButton() => OnButtonSound?.Invoke();
    public void OnClickSettingsButton() => OnButtonSettings?.Invoke();
    public void OnClickApplyButton() => OnButtonApply?.Invoke();
    public void OnClickMinutesUpButton() => OnButtonMinutesUp?.Invoke();
    public void OnClickMinutesDownButton() => OnButtonMinutesDown?.Invoke();
    public void OnClickSecondsUpButton() => OnButtonSecondsUp?.Invoke();
    public void OnClickSecondsDownButton() => OnButtonSecondsDown?.Invoke();
    public void OnClickHarvestButton() => OnButtonHarvest?.Invoke();
    #endregion

    [SerializeField] private int resolutionX, resolutionY;
    [SerializeField] private bool isFullscreen;

    private TimerUIHandler timerUIHandler;
    private ProgressUIHandler progressUIHandler;

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<UIManager>(this);

        timerUIHandler = GetComponent<TimerUIHandler>();
        progressUIHandler = GetComponent<ProgressUIHandler>();
    }

    private void Start() {
        Screen.SetResolution(resolutionX, resolutionY, isFullscreen);
    }

    public TimerUIHandler GetTimerUIHandler() => timerUIHandler;
    public ProgressUIHandler GetProgressUIHandler() => progressUIHandler;
}

public enum SettingStates {
    Work,
    Break,
    Empty
}