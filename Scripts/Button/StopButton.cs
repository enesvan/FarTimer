using DG.Tweening;

public class StopButton : ButtonUI {
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        uiManager.OnButtonStart += CloseButton;
        uiManager.OnButtonPause += OpenButton;
        uiManager.OnButtonStop += CloseButton;

        base.OnStart();
    }
    protected override void OpenButton() {
        base.OpenButton();
        buttonTf.DOLocalMoveX(-50f, .5f);
    }

    protected override void CloseButton() {
        base.CloseButton();
        buttonTf.DOLocalMoveX(0f, .5f);
    }
}
