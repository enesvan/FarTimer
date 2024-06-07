
public class StartButton : ButtonUI {
    private int flag = 0;
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        uiManager.OnButtonStart += CloseButton;
        uiManager.OnButtonPause += OpenButton;
        uiManager.OnButtonStop += OpenButton;
        uiManager.OnButtonSettings += CloseButton;
        uiManager.OnButtonApply += () => {
            flag++;
            if (flag == 1) return;
            flag = 0;
            OpenButton();
        };
    }
}
