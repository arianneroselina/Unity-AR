using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : MonoBehaviour
{
    [SerializeField] private float speed;

    private FixedJoystick fixedJoystick;
    private Rigidbody rigidBody;

    private void OnEnable() {
        fixedJoystick = FindObjectOfType<FixedJoystick>();
        rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float xVal = fixedJoystick.Horizontal;
        float yVal = fixedJoystick.Vertical;

        // Get the camera's forward and right directions
        Vector3 cameraForward = Camera.main.transform.forward;
        Vector3 cameraRight = Camera.main.transform.right;

        // Flatten the vectors to ignore the y-axis (we only want horizontal movement)
        cameraForward.y = 0;
        cameraRight.y = 0;

        // Normalize the vectors to prevent diagonal movement from being faster
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calculate the movement direction relative to the camera
        Vector3 movement = (cameraForward * yVal + cameraRight * xVal).normalized;

        // Apply movement to the dragon
        rigidBody.velocity = movement * speed;

        // Rotate the dragon to face the direction of movement
        if (movement != Vector3.zero) {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}
