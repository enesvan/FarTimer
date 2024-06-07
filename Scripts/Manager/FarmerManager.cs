using System.Collections.Generic;
using UnityEngine;

public class FarmerManager : Manager {
    [SerializeField] private List<Farmer> farmers;
    [SerializeField] private List<Transform> destinations;
    private Dictionary<Transform, bool> destinationsDic; // [destination,isAvailable]

    public override void AwakeManager() {
        base.AwakeManager();
        var service = ServiceManager.Instance;
        service.RegisterManager<FarmerManager>(this);

        destinationsDic = new Dictionary<Transform, bool>();
        foreach (Transform t in destinations) destinationsDic.Add(t, true);
    }

    private void Start() {
        foreach (var farmer in farmers) farmer.OnStart();
    }

    public Transform GetDestination() {
        Transform destination;
        var random = Random.Range(0, destinations.Count);
        destination = destinations[random];
        while (!destinationsDic[destination]) {
            random = Random.Range(0, destinations.Count);
            destination = destinations[random];
        }
        destinationsDic[destination] = false;
        return destination;
    }

    public void SetDestinationAvailable(Transform tf) => destinationsDic[tf] = true;
}