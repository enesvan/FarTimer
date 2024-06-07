using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FieldTile : MonoBehaviour {
    [SerializeField] private List<Transform> tomatoObjects;
    [SerializeField] private Transform rootObject;
    [SerializeField] private Vector3 rootScale;

    private void Awake() {
        rootObject.localScale = Vector3.zero;
        foreach (var tf in tomatoObjects) tf.localScale = Vector3.zero;
    }

    public void OpenCloseToHarvest(bool state) {
        if (state) {
            rootObject.DOScale(rootScale, .5f).OnComplete(() => {
                foreach (var tf in tomatoObjects) tf.DOScale(.75f, .5f);
            });
            } else {
            rootObject.DOScale(0f, .5f);
            foreach (var tf in tomatoObjects) tf.DOScale(0f, .5f);
        }
    }
}