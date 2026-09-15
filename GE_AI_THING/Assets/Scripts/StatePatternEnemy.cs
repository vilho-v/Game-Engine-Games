using UnityEngine;
using UnityEngine.AI;

public class StatePatternEnemy : MonoBehaviour
{

    // how long critter searches for player in alert mode
    public float searchDuration = 5;

    public float searchRotationSpeed;

    public float chaseDuration = 10, chaseSpeedIncrease = 1;

    public float sightRange;

    // patrols these
    public Transform[] waypoints;

    // debug things
    public Transform evil_eye;
    public MeshRenderer indicator;

    public IEnemyState currentState;

    [HideInInspector] public NavMeshAgent navMeshAgent;
    public PatrolState patrolState;
    public AlertState alertState;
    public ChaseState chaseState;

    [Header("set in runtime")]
    public Transform target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        patrolState = new PatrolState(this);
        alertState = new AlertState(this);
        chaseState = new ChaseState(this);
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentState = patrolState;
    }


    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    private void OnTriggerEnter(Collider other)
    {
        currentState.StateTriggerEnter(other);
    }
}
