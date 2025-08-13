using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    public float speed = 12f;
    public float speedH = 2.0f; // (Unused now for yaw rotation, but left in case you re-add mouse X/Y rotation later)
    public float speedV = 2.0f; // (Unused now for yaw rotation, but left in case you re-add mouse X/Y rotation later)
    public float yaw = 0.0f;    // (Unused now for yaw rotation)
    public float pitch = 0.0f;  // (Unused now for yaw rotation)

    public CharacterController controller;
    private Vector3 velocity;

    public float gravity = -20f;
    public float groundDistance = 0.4f;
    private bool isGrounded;
    public float jumpHeight = 2f;

    // New variables for mouse look at cursor
    public Camera mainCamera; // Drag your camera here in the Inspector

    void Start()
    {
        // If the variable "controller" is empty...
        if (controller == null)
        {
            // ...then this searches the components on the gameobject and gets a reference to the CharacterController class
            controller = GetComponent<CharacterController>();
        }

        // If the variable "mainCamera" is empty...
        if (mainCamera == null)
        {
            // ...then this searches for the main camera in the scene
            mainCamera = Camera.main;
        }
    }

    void Update()
    {
        // Get the Left/Right and Forward/Back values of the input being used (WASD, Joystick etc.)
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Let the player jump if they are on the ground and they press the jump button
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }

        // --- New: Rotate the player to look at the mouse position ---
        // Cast a ray from the mouse position into the world
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        // Create a plane at y = 0 (ground level) for the ray to intersect
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        // If the ray hits the plane...
        if (groundPlane.Raycast(ray, out rayLength))
        {
            // Get the point where the ray hit the plane
            Vector3 pointToLook = ray.GetPoint(rayLength);

            // Find the direction from the player to that point
            Vector3 direction = pointToLook - transform.position;

            // Keep only the horizontal direction (no vertical tilt)
            direction.y = 0;

            // Apply the rotation if the direction is valid
            if (direction != Vector3.zero)
            {
                transform.rotation = Quaternion.LookRotation(direction);
            }
        }
        // ------------------------------------------------------------

        // This is stealing the data about the player being on the ground from the character controller
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        // This fakes gravity!
        velocity.y += gravity * Time.deltaTime;

        // This takes the Left/Right and Forward/Back values to build a vector
        Vector3 move = transform.right * x + transform.forward * z;

        // Finally, it applies that vector it just made to the character
        controller.Move(move * speed * Time.deltaTime + velocity * Time.deltaTime);
    }
}

