using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BNG {

    /// <summary>
    /// Advanced Gravity Controls for a CharacterController or RigidBody
    /// </summary>
    public class AdvPlayerGravity : MonoBehaviour {

        [Tooltip("If true, will apply gravity to the CharacterController component, or RigidBody if no CC is present.")]

        public bool GravityEnabled = true;

        [Tooltip("Amount of Gravity to apply to the CharacterController or Rigidbody. Default is 'Physics.gravity'.")]
        public Vector3 Gravity = Physics.gravity;

        CharacterController characterController;
        SmoothLocomotion smoothLocomotion;

        Rigidbody playerRigidbody;

        private Vector3 _movement;
        private Vector3 _initialGravityModifier;
        private Quaternion targetRotation;

        // Save us a null check in FixedUpdate
        private bool _validRigidBody = false;

        void Start() {
            characterController = GetComponent<CharacterController>();
            smoothLocomotion = GetComponentInChildren<SmoothLocomotion>();
            playerRigidbody = GetComponent<Rigidbody>();

            _validRigidBody = playerRigidbody != null;

            _initialGravityModifier = Gravity;
        }

        // Apply Gravity in LateUpdate to ensure it gets applied after any character movement is applied in Update
        void LateUpdate() {

            // Apply Gravity to Character Controller
            if (GravityEnabled && characterController != null && characterController.enabled) {
                _movement += Gravity * Time.deltaTime;

                // Default to smooth locomotion
                if(smoothLocomotion) {
                    smoothLocomotion.MoveCharacter(_movement * Time.deltaTime);
                }
                // Fallback to character controller
                else if(characterController) {
                    characterController.Move(_movement * Time.deltaTime);
                    //rotate character controller to make direction of movement below it
                    // targetRotation = Quaternion.FromToRotation(Vector3.down, _movement);
                    // Vector3 direction = _movement.normalized;
                    Quaternion targetRotation = Quaternion.FromToRotation(Vector3.forward, _movement);
                    characterController.transform.rotation = Quaternion.Slerp(characterController.transform.rotation, targetRotation, Time.deltaTime * 100f);

                }
                
                // Reset Y movement if we are grounded
                if (characterController.isGrounded) {
                    _movement = Vector3.zero;
                }
            }
        }

        void FixedUpdate() {
            // Apply Gravity to Rigidbody Controller
            if (_validRigidBody && GravityEnabled) {
                //playerRigidbody.AddRelativeForce(Gravity, ForceMode.VelocityChange);
                //playerRigidbody.AddForce(new Vector3(0, -Gravity.y * playerRigidbody.mass, 0));

                if(smoothLocomotion && smoothLocomotion.ControllerType == PlayerControllerType.Rigidbody && smoothLocomotion.GroundContacts < 1) {
                    
                }

                playerRigidbody.AddForce(Gravity * playerRigidbody.mass);
            }
        }

        public void ToggleGravity(bool gravityOn) {

            GravityEnabled = gravityOn;

            if (gravityOn) {
                Gravity = _initialGravityModifier;
            }
            else {
                Gravity = Vector3.zero;
            }
        }
    }
}

