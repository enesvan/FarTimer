using DG.Tweening;

public class HarvestButton : ButtonUI {
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        uiManager.OnButtonHarvest += CloseButton;
        uiManager.OnHarvestable += OpenButton;

        CloseButton();
    }

    protected override void OpenButton() {
        if (button.interactable) return;
        button.image.DOKill();
        button.image.DOFade(1f, .5f).OnComplete(() => button.interactable = true);
    }

    protected override void CloseButton() {
        button.interactable = false;
        button.image.DOKill();
        button.image.DOFade(0.5f, .5f);
    }
}