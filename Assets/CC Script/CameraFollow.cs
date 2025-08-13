using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;        // The object to follow (your player)
    public Vector3 offset = new Vector3(0, 10, -10); // Camera position relative to player
    public float followSpeed = 5f;  // How quickly the camera follows

    public float zoomSpeed = 2f;    // How fast to zoom with scroll wheel
    public float minZoom = 5f;      // Closest zoom distance
    public float maxZoom = 20f;     // Furthest zoom distance
    public float currentZoom = 10f; // Current zoom distance (public so you can tinker)

    void LateUpdate()
    {
        if (target == null) return;

        // Adjust zoom with mouse scroll wheel
        currentZoom -= Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        currentZoom = Mathf.Clamp(currentZoom, minZoom, maxZoom);

        // Smoothly move the camera towards the desired position
        Vector3 desiredPosition = target.position + offset.normalized * currentZoom;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, followSpeed * Time.deltaTime);

        // Always look at the target
        transform.LookAt(target.position + Vector3.up * 1.5f); // Look at player's upper body
    }
}
