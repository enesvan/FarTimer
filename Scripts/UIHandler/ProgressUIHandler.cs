using TMPro;
using UnityEngine;

public class ProgressUIHandler : MonoBehaviour {
    [Header("References")]
    [SerializeField] private TextMeshProUGUI clockText;
    [SerializeField] private TextMeshProUGUI tomatoText;

    private ProgressManager progressManager;

    private void Start() {
        var service = ServiceManager.Instance;
        progressManager = service.GetManager<ProgressManager>();
    }

    public void OnStart() {
        UpdateTimeText();
        UpdateHarvestText();
    }

    public void UpdateTimeText() => clockText.text = GetClockText();
    public void UpdateHarvestText() => tomatoText.text = GetTomatoText();

    private string GetClockText() {
        if (progressManager == null) return $"{0}h {0}m";
        float minutes = progressManager.GetAchievedTime() / 60;
        float hour = minutes / 60;
        return $"{(int)hour}h {(int)(minutes % 60)}m";
    }

    private string GetTomatoText() {
        if (progressManager == null) return $"{0}";
        int total = progressManager.GetAchievedHarvest();
        if (total < 1000) return $"{total}";
        else return $"{total / 1000}.{(total % 1000) / 100}k";
    }
}