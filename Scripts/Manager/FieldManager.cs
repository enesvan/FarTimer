using System.Collections.Generic;
using UnityEngine;

public class FieldManager : Manager {
    [SerializeField] private float fieldFrequency = 60f; // seconds
    [SerializeField] private List<FieldTile> fieldTiles;
    private Dictionary<FieldTile, bool> fieldTileDic; // [tile,isAvailable]

    private ProgressManager progressManager;

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<FieldManager>(this);

        fieldTileDic = new Dictionary<FieldTile, bool>();
        foreach (var tile in fieldTiles) fieldTileDic.Add(tile, true);
    }

    private void Start() {
        var service = ServiceManager.Instance;
        var uiManager = service.GetManager<UIManager>();

        progressManager = service.GetManager<ProgressManager>();
        uiManager.OnButtonHarvest += Harvest;
    }

    private void Update() {
        if(progressManager.GetActualTime() >= fieldFrequency) {
            progressManager.ResetActualTime();
            OpenAField();
            ServiceManager.Instance.GetManager<UIManager>().OnHarvestable?.Invoke(); // Open Harvest Button
        }
    }

    public void Harvest() {
        int count = 0;
        var copyDic = new Dictionary<FieldTile, bool>(fieldTileDic);
        foreach (var tile in fieldTileDic) {
            if (!tile.Value) {
                count++;
                tile.Key.OpenCloseToHarvest(false);
                copyDic[tile.Key] = true;
            }
        }
        fieldTileDic = copyDic;
        progressManager.AchieveHarvest(count * 4);
        ServiceManager.Instance.GetManager<UIManager>().GetProgressUIHandler().UpdateHarvestText();
    }

    public void OpenAField() {
        if (IsFieldsFull()) {
            return;
        }
        var random = Random.Range(0, fieldTiles.Count);
        var field = fieldTiles[random];
        int iteration = 0;
        while (!fieldTileDic[field]) {
            if (iteration == 5) {
                field = GetNextField();
                break;
            }
            random = Random.Range(0, fieldTiles.Count);
            field = fieldTiles[random];
            iteration++;
        }
        fieldTileDic[field] = false;
        field.OpenCloseToHarvest(true);
    }

    private bool IsFieldsFull() {
        foreach(var tile in fieldTileDic) {
            if (tile.Value) return false;
        }
        return true;
    }

    private FieldTile GetNextField() {
        foreach(var tile in fieldTileDic) {
            if (tile.Value) return tile.Key;
        }
        return null;
    }
}