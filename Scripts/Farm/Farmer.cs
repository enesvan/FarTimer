using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Farmer : MonoBehaviour {
    [SerializeField] private float waitTime;
    [SerializeField] private Animator animator;

    private const string IS_WALKING = "Is_Walking";
    private NavMeshAgent agent;
    private Transform activeDestination;
    private FarmerManager farmerManager;
    private IEnumerator activeWait = null;
    private float agentSpeed;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agentSpeed = agent.speed;
    }

    private void Update() {
        if (activeWait != null) return;
        if (agent.remainingDistance <= 0.1f) {
            activeWait = OnReachDestination();
            StartCoroutine(activeWait);
        }
    }

    public void OnStart() {
        var service = ServiceManager.Instance;
        farmerManager = service.GetManager<FarmerManager>();
        SetDestination();
    }

    private void SetDestination() {
        activeDestination = farmerManager.GetDestination();
        agent.SetDestination(activeDestination.position);
        StartWalk();
    }

    private IEnumerator OnReachDestination() {
        StopWalk();
        yield return new WaitForSeconds(waitTime);
        farmerManager.SetDestinationAvailable(activeDestination);
        SetDestination();
        activeWait = null;
    }

    private void StartWalk() {
        agent.speed = agentSpeed;
        animator.SetBool(IS_WALKING, true);
    }

    private void StopWalk() {
        agent.speed = 0f;
        animator.SetBool(IS_WALKING, false);
    }
}