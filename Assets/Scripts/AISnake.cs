using UnityEngine;
using UnityEngine.Events;
using System.Collections;

using BNG;

public class AISnake : MonoBehaviour
{
    public GameObject headPrefab;
    public GameObject segmentPrefab;
    public int numberOfSegments = 5;
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
    private Transform player;
    private GameObject head;


    Grabber currentGrabber;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
        Debug.Log(player);

        // Initialize segment array and positions array
        segments = new GameObject[numberOfSegments];
        segmentPositions = new Vector3[numberOfSegments];

        // Instantiate head object
        head = Instantiate(headPrefab, sphereTransform.position + new Vector3(sphereRadius, 0, 0), Quaternion.identity*Quaternion.Euler(0,90,0));
        GameObject empty = new GameObject("MyEmpty");
        segments[0] = empty;
        // segmentPositions[0] = head.transform.position;

        // Instantiate the rest of the segments
        for (int i = 1; i < numberOfSegments; i++)
        {
            GameObject segment = Instantiate(segmentPrefab, segmentPositions[i - 1] - transform.forward * segmentDistance, Quaternion.identity);
            segment.transform.localScale = segment.transform.localScale * Mathf.Pow(0.9f, i);
            segments[i] = segment;
            segmentPositions[i] = segment.transform.position;
        }
        //parent head to segments[0]
        head.transform.parent = segments[0].transform;
        head.transform.localPosition = new Vector3(0, 0, 0);

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
            float targetTurnAngleY = Random.Range(-turningRate, turningRate);
            float targetTurnAngleX = Random.Range(-turningRate, turningRate);

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
                targetHeadPosition = segments[0].transform.position + (Quaternion.Euler(targetTurnAngleX, targetTurnAngleY, 0) * (targetHeadPosition - segments[0].transform.position));

                // Calculate the rotation required for the head to face its forward direction
                Vector3 headDirection = (targetHeadPosition - segments[0].transform.position).normalized;
                Quaternion headRotation = Quaternion.LookRotation(headDirection, sphereTransform.up);

                // Apply the updated head position and rotation
                segmentPositions[0] = targetHeadPosition;
                segments[0].transform.rotation = headRotation;
                yield return null;
            }

            previousTargetTurnAngleY = currentTurnAngleY;
            previousTargetTurnAngleX = currentTurnAngleX;
            // head.transform.LookAt(player);
            // Debug.Log(player.position);
            Vector3 directionToPlayer = player.position - segmentPositions[0];
            Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
            targetRotation *= Quaternion.Euler(0, 90, 0); // Optional: rotate the object by 90 degrees to face the player properly
            head.transform.rotation = targetRotation;

        }
    }



    IEnumerator MoveSnake()
    {
        while (true)
        {
            // Project head position onto the sphere surface
            Vector3 sphereCenterToHead = segmentPositions[0] - sphereTransform.position;
            segmentPositions[0] = sphereTransform.position + sphereCenterToHead.normalized * sphereRadius;

            // Update the position and rotation of each segment to follow the previous one
            for (int i = 1; i < numberOfSegments; i++)
            {
                Vector3 direction = (segmentPositions[i - 1] - segmentPositions[i]).normalized;
                segmentPositions[i] = segmentPositions[i - 1] - direction * segmentDistance;

                // Calculate the rotation required for the segment to face its forward direction
                Quaternion segmentRotation = Quaternion.LookRotation(direction);

                // Apply the updated rotation
                segments[i].transform.rotation = segmentRotation;
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
