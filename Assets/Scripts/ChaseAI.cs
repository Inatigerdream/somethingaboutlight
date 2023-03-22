using UnityEngine;
using UnityEngine.Events;
using BNG;

public class ChaseAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;
    public float thresholdspeed = 3f;

    public UnityEvent ArchonKill;

    private Rigidbody rb; // rigidbody of the enemy
    private Rigidbody playerController; //rigidbody of the player
    private Vector3 lastPlayerPosition; // last position of the player

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerController = player.GetComponent<Rigidbody>();
        lastPlayerPosition = player.position;
    }

    private void FixedUpdate()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction)*Quaternion.Euler(90, 0, 0);
            rb.MoveRotation(Quaternion.Slerp(transform.rotation, lookRotation, rotateSpeed * Time.deltaTime));

            // float currentSpeed = playerController.velocity.magnitude;
            float currentSpeed = (player.position - lastPlayerPosition).magnitude / Time.deltaTime;

            Debug.Log(currentSpeed);

            if (currentSpeed > thresholdspeed)
            {
                Debug.Log("moving");
                Vector3 moveDirection = direction * currentSpeed;
                rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            }
        }

        lastPlayerPosition = player.position;
    }
    // void OnTriggerEnter(Collider other) {
    //     // Debug.Log(other);
    //     ArchonKill.Invoke();

    //     }

    
}
