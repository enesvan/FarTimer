
public class PauseButton : ButtonUI {
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        uiManager.OnButtonStart += OpenButton;
        uiManager.OnButtonPause += CloseButton;
        uiManager.OnButtonStop += CloseButton;

        base.OnStart();
    }
}
