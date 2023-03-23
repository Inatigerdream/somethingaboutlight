using UnityEngine;

public class FootstepController : MonoBehaviour
{
    public AudioClip[] footstepSounds; // an array of audio clips for footstep sounds
    public float minSpeedForFootstep = 0.1f; // minimum speed for playing footstep sounds
    public float footstepSoundDelay = 0.3f; // time delay between footstep sounds

    private AudioSource audioSource;
    private float lastFootstepTime = 0f;
    private float currentSpeed = 0f;
    private Vector3 lastPosition;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        Vector3 currentPosition = transform.position;
        float distance = Vector3.Distance(currentPosition, lastPosition);
        currentSpeed = distance / Time.deltaTime;

        if (currentSpeed > minSpeedForFootstep && Time.time > lastFootstepTime + footstepSoundDelay)
        {
            PlayFootstepSound();
            lastFootstepTime = Time.time;
        }

        lastPosition = currentPosition;
    }

    private void PlayFootstepSound()
    {
        int soundIndex = Random.Range(0, footstepSounds.Length);
        audioSource.clip = footstepSounds[soundIndex];
        audioSource.Play();
    }
    void OnTriggerExit(Collider other)
    {
       audioSource.Play();
    }
}
