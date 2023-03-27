using UnityEngine;
using UnityEngine.Events;

public class AISnake : MonoBehaviour
{
    public int numberOfSegments = 5;
    public GameObject segmentPrefab;
    public float segmentDistance = 0.5f;
    public float movementSpeed = 5.0f;
    public float turningRate = 45.0f;
    public Transform sphereTransform;
    public float sphereRadius;
    public UnityEvent onPlayerGrab;

    private GameObject[] segments;
    private Vector3[] segmentPositions;
    private float currentTurningAngle;

    void Start()
    {
        segments = new GameObject[numberOfSegments];
        segmentPositions = new Vector3[numberOfSegments];

        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i] = Instantiate(segmentPrefab, transform.position - (Vector3.forward * segmentDistance * i), Quaternion.identity, transform);
            segmentPositions[i] = segments[i].transform.position;
        }
    }

    void Update()
    {
        // Change turning angles randomly
        float turnAngleY = Random.Range(-turningRate, turningRate) * Time.deltaTime;
        float turnAngleX = Random.Range(-turningRate, turningRate) * Time.deltaTime;

        // Calculate new head position based on movement speed
        Vector3 newHeadPosition = segments[0].transform.position + segments[0].transform.forward * movementSpeed * Time.deltaTime;

        // Apply turning angles to the head position
        newHeadPosition = segments[0].transform.position + (Quaternion.Euler(turnAngleX, turnAngleY, 0) * (newHeadPosition - segments[0].transform.position));

        segmentPositions[0] = newHeadPosition;

        // Project head position onto the sphere surface
        Vector3 sphereCenterToHead = segmentPositions[0] - sphereTransform.position;
        segmentPositions[0] = sphereTransform.position + sphereCenterToHead.normalized * sphereRadius;

        // Update the position of each segment to follow the previous one
        for (int i = 1; i < numberOfSegments; i++)
        {
            Vector3 direction = (segmentPositions[i - 1] - segmentPositions[i]).normalized;
            segmentPositions[i] = segmentPositions[i - 1] - direction * segmentDistance;
        }

        // Apply the updated positions
        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i].transform.position = segmentPositions[i];
        }
    }



    public void OnPlayerGrab()
    {
        // Reset the head position
        Vector3 randomDirection = Random.onUnitSphere;
        segmentPositions[0] = sphereTransform.position + randomDirection * sphereRadius;

        // Reset the position of each segment
        for (int i = 1; i < numberOfSegments; i++)
        {
            segmentPositions[i] = segmentPositions[i - 1] - randomDirection * segmentDistance;
        }

        // Apply the updated positions
        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i].transform.position = segmentPositions[i];
        }

        // Invoke the grab event
        onPlayerGrab.Invoke();
    }



}
