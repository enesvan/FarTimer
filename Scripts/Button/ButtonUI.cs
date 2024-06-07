using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonUI : MonoBehaviour {
    [SerializeField] protected Button button;
    protected GameObject buttonObj;
    protected Transform buttonTf;

    private void Awake() {
        buttonObj = button.gameObject;
        buttonTf = button.transform;
    }

    private void Start() {
        OnStart();
    }

    protected virtual void OnStart() {
        buttonObj.SetActive(false);
        buttonTf.localScale = Vector3.zero;
    }

    protected virtual void OpenButton() {
        button.enabled = false;
        buttonObj.SetActive(true);
        buttonTf.DOScale(Vector3.one, .5f).OnComplete(() => {
            button.enabled = true;
        });
    }

    protected virtual void CloseButton() {
        button.enabled = false;
        buttonTf.DOScale(Vector3.zero, .5f).OnComplete(() => {
            buttonObj.gameObject.SetActive(false);
        });
    }
}