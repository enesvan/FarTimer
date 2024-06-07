using UnityEngine;

public class MinutesUpButton : ButtonUI {
    [SerializeField] private Color breakColor;
    private SettingStates activeState;
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();
        var timerManager = service.GetManager<TimerManager>();

        uiManager.OnButtonSettings += OpenButton;
        uiManager.OnButtonMinutesUp += () => {
            if (activeState == SettingStates.Work) timerManager.SetWorkMinutes(1);
            else timerManager.SetBreakMinutes(1);
        };
        uiManager.OnButtonApply += () => {
            activeState++;
            if (activeState == SettingStates.Empty) {
                activeState = SettingStates.Work;
                button.image.color = Color.white;
                CloseButton();
            } else if (activeState == SettingStates.Break) {
                button.image.color = breakColor;
                buttonTf.localScale = Vector3.zero;
                OpenButton();
            }
        };

        base.OnStart();
    }
}