using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector3 direction = Vector3.forward;

    private List<CharacterController> characterControllers = new List<CharacterController>();

    private void OnTriggerEnter(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null)
        {
            characterControllers.Add(controller);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CharacterController controller = other.GetComponent<CharacterController>();
        if (controller != null)
        {
            characterControllers.Remove(controller);
        }
    }

    private void Update()
    {
        Vector3 conveyorForce = transform.TransformDirection(direction) * speed * Time.deltaTime;
        foreach (CharacterController controller in characterControllers)
        {
            controller.transform.position += conveyorForce;
        }
    }
}
