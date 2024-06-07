using System.Collections;
using UnityEngine;

public class ApplyButton : ButtonUI {
    [SerializeField] private Color breakColor;
    private SettingStates activeState;
    protected override void OnStart() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();
        var uiHandler = uiManager.GetTimerUIHandler();
        var timerManager = service.GetManager<TimerManager>();

        uiManager.OnButtonSettings += OpenButton;
        uiManager.OnButtonApply += () => {
            activeState++;
            if (activeState == SettingStates.Break) {
                button.image.color = breakColor;
                buttonTf.localScale = Vector3.zero;
                OpenButton();
                uiHandler.SetImagesAlpha(false);
                return;
            }
            activeState = SettingStates.Work;
            button.image.color = Color.white;
            CloseButton();
        };
        StartCoroutine(DelayEvent(uiManager, uiHandler, timerManager));
    }

    private IEnumerator DelayEvent(UIManager uiManager, TimerUIHandler uiHandler, TimerManager timerManager) { // to add functions end of event
        yield return new WaitForSeconds(.25f);
        uiManager.OnButtonApply += () => timerManager.SetBreakMinutes(0);
        yield return new WaitForSeconds(.25f);

        uiManager.OnButtonApply += () => {
            if (activeState == SettingStates.Break) return;
            uiHandler.SetupSlider(null, 0);
            uiHandler.UpdateText();
        };

        base.OnStart();
    }
}