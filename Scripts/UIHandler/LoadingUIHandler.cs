using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingUIHandler : MonoBehaviour {
    [Header("Values")]
    [SerializeField] private float loadingTime;
    [SerializeField] private float fadeMultiplier;
    [SerializeField] private float turnMultiplier;

    [Header("References")]
    [SerializeField] private GameObject loadingObj;
    [SerializeField] private Transform loadingImagesParent;
    [SerializeField] private List<Image> loadingImages;

    private CanvasGroup canvasGroup;
    private int imageIndex = 0;
    private bool isActive = false;

    private void Awake() {
        canvasGroup = loadingObj.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1f;
        foreach (var image in loadingImages) image.gameObject.SetActive(false);
    }

    private void Start() {
        InvokeRepeating("OpenImage", 0, .1f);
        StartCoroutine(LoadingSequence());
    }

    private void Update() {
        if (!isActive) return;
        loadingImagesParent.eulerAngles += Vector3.forward * turnMultiplier * Time.deltaTime;
    }

    private void OpenImage() {
        loadingImages[imageIndex].gameObject.SetActive(false);
        imageIndex++;
        imageIndex %= loadingImages.Count;
        loadingImages[imageIndex].gameObject.SetActive(true);
    }

    private IEnumerator LoadingSequence() {
        isActive = true;
        yield return new WaitForSeconds(loadingTime);

        var service = ServiceManager.Instance;
        var soundManager = service.GetManager<SoundManager>();
        soundManager.PlayBackloopSound();

        while (canvasGroup.alpha > 0f) {
            canvasGroup.alpha -= Time.deltaTime * fadeMultiplier;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        CancelInvoke();
        isActive = false;
        Destroy(loadingObj);
    }
}