using UnityEngine;

public class SoundButton : ButtonUI {
    [SerializeField] private Color inactiveColor;
    private bool isActive = true;
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();
        var soundManager = service.GetManager<SoundManager>();

        uiManager.OnButtonSound += () => {
            if (isActive) {
                CloseButton();
                soundManager.MuteMusic();
            } 
            else {
                OpenButton();
                soundManager.UnMuteMusic();
            }
        };
    }

    protected override void OpenButton() {
        isActive = true;
        button.image.color = Color.white;
    }

    protected override void CloseButton() {
        isActive = false;
        button.image.color = inactiveColor;
    }
}