using System.Collections.Generic;
using UnityEngine;
using System;

public class ServiceManager : MonoBehaviour {
    [SerializeField] private List<Manager> managerList;
    private Dictionary<Type, object> managerDic = new Dictionary<Type, object>();

    public static ServiceManager Instance;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else Destroy(this);

        Application.targetFrameRate = 60;
        AwakeManagers();
    }

    public void AwakeManagers() {
        foreach (var manager in managerList) manager.AwakeManager();
    }

    public void RegisterManager<T>(T manager) => managerDic[typeof(T)] = manager;
    public T GetManager<T>() => (T)managerDic[typeof(T)];
}
