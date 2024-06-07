
public class SettingsButton : ButtonUI {
    private SettingStates activeState;
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        uiManager.OnButtonStart += CloseButton;
        uiManager.OnButtonStop += OpenButton;
        uiManager.OnButtonSettings += CloseButton;
        uiManager.OnButtonApply += () => {
            activeState++;
            if (activeState == SettingStates.Break) return;
            activeState = SettingStates.Work;
            OpenButton();
        };
    }
}