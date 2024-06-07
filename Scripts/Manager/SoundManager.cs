using UnityEngine;

public class SoundManager : Manager {
    [SerializeField] private AudioSource workSource;
    [SerializeField] private AudioSource breakSource;
    [SerializeField] private AudioSource resetSource;
    [SerializeField] private AudioSource backloopSource;
    [SerializeField] private AudioSource coreButtonSource;
    [SerializeField] private AudioSource subButtonSource;

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<SoundManager>(this);
    }

    private void Start() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        uiManager.OnButtonStart += PlayCoreButtonSound;
        uiManager.OnButtonPause += PlayCoreButtonSound;
        uiManager.OnButtonStop += PlayCoreButtonSound;
        uiManager.OnButtonSound += PlayCoreButtonSound;
        uiManager.OnButtonApply += PlayCoreButtonSound;
        uiManager.OnButtonSettings += PlayCoreButtonSound;
        uiManager.OnButtonHarvest += PlayCoreButtonSound;

        uiManager.OnButtonMinutesUp += PlaySubButtonSound;
        uiManager.OnButtonMinutesDown += PlaySubButtonSound;
        uiManager.OnButtonSecondsUp += PlaySubButtonSound;
        uiManager.OnButtonSecondsDown += PlaySubButtonSound;
    }

    public void PlayWorkSound() => workSource.Play();
    public void PlayBreakSound() => breakSource.Play();
    public void PlayResetSound() => resetSource.Play();
    public void PlayBackloopSound() => backloopSource.Play();
    public void PlayCoreButtonSound() => coreButtonSource.Play();
    public void PlaySubButtonSound() => subButtonSource.Play();
    public void UnMuteMusic() => backloopSource.mute = false;
    public void MuteMusic() => backloopSource.mute = true;
}