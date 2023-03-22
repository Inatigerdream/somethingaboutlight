using UnityEngine;
using BNG;

public class ChaseAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 10f;
    public float moveSpeed = 5f;
    public float rotateSpeed = 5f;

    private Rigidbody rb;
    private Rigidbody playerController;
    private Vector3 lastPlayerPosition;


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

            if (currentSpeed > 0f)
            {
                Debug.Log("moving");
                Vector3 moveDirection = direction * currentSpeed;
                rb.MovePosition(transform.position + moveDirection * moveSpeed * Time.deltaTime);
            }
        }

        // // change shader characteristics
        // if (distanceToPlayer < 5f)
        // {
        //     GetComponent<Renderer>().material.SetFloat("Alpha_select", Mathf.Sin(Time.time * 0.03f)+2f);
        // }

        lastPlayerPosition = player.position;

    }
}
