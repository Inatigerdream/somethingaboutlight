using UnityEngine;
using UnityEngine.Events;
using BNG;

public class ChaseAI : MonoBehaviour
{
    public GameObject mainlight;
    public Transform player;
    public float chaseDistance = 10f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public float thresholdspeed = 3f;

    public Transform[] patrolPoints;
    // private UnityEngine.AI.NavMeshAgent agent;
    private int currentPatrolPoint;
    private Vector3 destination;
    private bool chasingPlayer;

    public UnityEvent ArchonKill;



    private Rigidbody rb; // rigidbody of the enemy
    private Rigidbody playerController; //rigidbody of the player
    private Vector3 lastPlayerPosition; // last position of the player
    private Vector3 spawnPosition; // spawn position of the enemy

    private void Start()
    {

        rb = GetComponent<Rigidbody>();
        playerController = player.GetComponent<Rigidbody>();
        lastPlayerPosition = player.position;
        spawnPosition = transform.position;
        // agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
        currentPatrolPoint = 0;
        SetNextPatrolPoint();
    }

    void SetNextPatrolPoint()
    {
        if (patrolPoints.Length == 0)
        {
            Debug.Log("No patrol points assigned to enemy");
            return;
        }

        destination = patrolPoints[currentPatrolPoint].position;
        currentPatrolPoint = (currentPatrolPoint + 1) % patrolPoints.Length;
    }


    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (mainlight.active == true) {
            transform.position = spawnPosition;
            chasingPlayer = false;
        }

        float currentSpeed = (player.position - lastPlayerPosition).magnitude / Time.deltaTime;

        if (!chasingPlayer)
        {
            // Debug.Log("Patrolling" + destination);
            if (Vector3.Distance(destination, transform.position) < 0.05f)
            {
                // Debug.Log("Reached patrol point");
                SetNextPatrolPoint();
            }
            Vector3 direction = (destination - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction)*Quaternion.Euler(90, 0, 0);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime));

            Vector3 moveDirection = direction * moveSpeed;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
        }

        if (distanceToPlayer < chaseDistance && chasingPlayer)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction)*Quaternion.Euler(90, 0, 0);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime));

            // float currentSpeed = playerController.velocity.magnitude;

            // if (currentSpeed > thresholdspeed)
            // {
            Vector3 moveDirection = direction * currentSpeed;
            rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            // }
        }
        if (distanceToPlayer < chaseDistance && currentSpeed > thresholdspeed && !chasingPlayer)
        {
            chasingPlayer = true;
        }

        lastPlayerPosition = player.position;
    }
    void OnTriggerEnter(Collider other) {
        
        if (other.gameObject.name == "PlayerController"){
            ArchonKill.Invoke();

        }

        }

    
}
