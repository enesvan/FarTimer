using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TimerUIHandler : MonoBehaviour {
    [Header("References")]
    [SerializeField] private Slider workSlider;
    [SerializeField] private Slider breakSlider;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private List<Image> workSliderImages;
    [SerializeField] private List<Image> breakSliderImages;

    private Slider activeSlider;

    private TimerManager timerManager;
    private ProgressManager progressManager;
    private ProgressUIHandler progressUIHandler;
    private SoundManager soundManager;

    private void Start() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        soundManager = service.GetManager<SoundManager>();
        progressUIHandler = uiManager.GetProgressUIHandler();
        uiManager.OnButtonStop += () => ChangeSlider(true);
    }

    public void OnStart() {
        var service = ServiceManager.Instance;
        timerManager = service.GetManager<TimerManager>();
        progressManager = service.GetManager<ProgressManager>();

        SetupSlider(breakSlider, timerManager.GetBreakMinutes());
        SetupSlider(null, 0);
        UpdateText();
    }

    private void BeginSlide() => timerManager.StartTimer();

    public void UpdateTimer() {
        activeSlider.value -= Time.deltaTime;
        if (activeSlider == workSlider) progressManager.AchieveTime();
        if (activeSlider.value == 0f) ChangeSlider();
        else UpdateText();
    }

    private void ChangeSlider(bool forceToWork = false) {
        progressUIHandler.UpdateTimeText();
        timerManager.StopTimer();
        if (activeSlider == workSlider && !forceToWork) {
            soundManager.PlayBreakSound();
            workSlider.DOValue(workSlider.maxValue, .5f);
            SetupSlider(breakSlider, timerManager.GetBreakMinutes());
            SetImagesAlpha(false);
        } else {
            if (forceToWork) soundManager.PlayResetSound();
            else soundManager.PlayWorkSound();
            breakSlider.DOValue(breakSlider.maxValue, .5f);
            SetupSlider(workSlider, timerManager.GetWorkMinutes());
            SetImagesAlpha(true);
        }
        UpdateText();
        if (!forceToWork) Invoke("BeginSlide", .5f);
    }

    public void SetupSlider(Slider slider, float minutes) {
        if (slider == null) {
            slider = workSlider;
            minutes = timerManager.GetWorkMinutes();
            SetImagesAlpha(true);
        }
        activeSlider = slider;
        activeSlider.minValue = 0;
        activeSlider.maxValue = minutes * 60;
        activeSlider.DOValue(activeSlider.maxValue, .5f);
        timerManager.SetRemainingTime(activeSlider.maxValue);
    }

    public void SetImagesAlpha(bool isWork) {
        if (isWork) {
            foreach (var image in workSliderImages) image.DOFade(1f, .5f);
            foreach (var image in breakSliderImages) image.DOFade(.4f, .5f);
        } else {
            foreach (var image in workSliderImages) image.DOFade(.4f, .5f);
            foreach (var image in breakSliderImages) image.DOFade(1f, .5f);
        }
    }

    public void UpdateText() => timerText.text = $"{GetMinutes()} : {GetSeconds()}";

    private string GetSeconds() {
        int seconds = (int)(timerManager.GetRemainingTime() % 60);
        if (seconds >= 10) return $"{seconds}";
        else return $"0{seconds}";
    }

    private string GetMinutes() {
        int minutes = (int)(timerManager.GetRemainingTime() / 60);
        if (minutes >= 10) return $"{minutes}";
        else return $"0{minutes}";
    }
}