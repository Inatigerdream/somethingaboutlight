using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using BNG;

public class AISnake : MonoBehaviour
{
    public int numberOfSegments = 5;
    public GameObject segmentPrefab;
    public float segmentDistance = 0.5f;
    public float movementSpeed = 5.0f;
    public float turningRate = 45.0f;
    public float angleInterpolationSpeed = 5.0f;
    public float movementInterpolationSpeed = 5.0f;
    public float minAngleChangeInterval = 0.5f;
    public float maxAngleChangeInterval = 2.0f;


    public Transform sphereTransform;
    public float sphereRadius;
    public UnityEvent onPlayerGrab;

    private GameObject[] segments;
    private Vector3[] segmentPositions;
    private float currentTurningAngle;

    Grabber currentGrabber;

    void Start()
    {
        segments = new GameObject[numberOfSegments];
        segmentPositions = new Vector3[numberOfSegments];

        for (int i = 0; i < numberOfSegments; i++)
        {
            segments[i] = Instantiate(segmentPrefab, transform.position - (Vector3.forward * segmentDistance * i), Quaternion.identity, transform);
            segmentPositions[i] = segments[i].transform.position;
        }
        StartCoroutine(ChangeAngles());
        StartCoroutine(MoveSnake());

    }

    IEnumerator ChangeAngles()
    {
        float previousTargetTurnAngleY = 0;
        float previousTargetTurnAngleX = 0;
        float currentTurnAngleY = 0;
        float currentTurnAngleX = 0;

        while (true)
        {
            // Calculate target turning angles randomly
            float targetTurnAngleY = GaussianRandom(0, turningRate);
            float targetTurnAngleX = GaussianRandom(0, turningRate);

            // Interpolate turning angles for smoother movement
            float angleChangeDuration = Random.Range(minAngleChangeInterval, maxAngleChangeInterval);
            float elapsedTime = 0;

            while (elapsedTime < angleChangeDuration)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / angleChangeDuration;

                currentTurnAngleY = Mathf.Lerp(previousTargetTurnAngleY, targetTurnAngleY, t);
                currentTurnAngleX = Mathf.Lerp(previousTargetTurnAngleX, targetTurnAngleX, t);

                // Apply turning angles to the head position
                Vector3 targetHeadPosition = segments[0].transform.position + segments[0].transform.forward * movementSpeed * Time.deltaTime;
                targetHeadPosition = segments[0].transform.position + (Quaternion.Euler(currentTurnAngleX, currentTurnAngleY, 0) * (targetHeadPosition - segments[0].transform.position));

                segmentPositions[0] = targetHeadPosition;
                yield return null;
            }

            previousTargetTurnAngleY = currentTurnAngleY;
            previousTargetTurnAngleX = currentTurnAngleX;

            // yield return new WaitForSeconds(Random.Range(minAngleChangeInterval, maxAngleChangeInterval));
        }
    }


    IEnumerator MoveSnake()
    {
        while (true)
        {
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

            yield return null;
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

        public float GaussianRandom(float mean, float standardDeviation)
    {
        float u1 = Random.value; // Uniform(0,1) random numbers
        float u2 = Random.value;

        // Box-Muller transform
        float z0 = Mathf.Sqrt(-2.0f * Mathf.Log(u1)) * Mathf.Cos(2.0f * Mathf.PI * u2);
        return z0 * standardDeviation + mean;
    }




}
