using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDigPlot : MonoBehaviour
{
    [Header("Digging Settings")]
    public float digHoldTime = 3f;
    public float digDistance = 2.5f; // Max distance from player to dig
    public GameObject plotPrefab;

    public float holdTimer = 0f;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("No Main Camera found in the scene!");
        }
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left Click Held
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Debug.DrawLine(ray.origin, hit.point, Color.green);

                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                {
                    float distanceToPlayer = Vector3.Distance(transform.position, hit.point);
                    Debug.Log($"Aiming at Ground at {hit.point}, Distance to player: {distanceToPlayer}");

                    if (distanceToPlayer <= digDistance)
                    {
                        holdTimer += Time.deltaTime;
                        Debug.Log($"Holding... {holdTimer:F2}/{digHoldTime} seconds");

                        if (holdTimer >= digHoldTime)
                        {
                            Debug.Log("Digging Successful! Creating Plot.");
                            Instantiate(plotPrefab, hit.point, Quaternion.identity);
                            holdTimer = 0f; // Reset after dig
                        }
                    }
                    else
                    {
                        Debug.Log($"Too far to dig! Must be within {digDistance} meters.");
                        holdTimer = 0f;
                    }
                }
                else
                {
                    Debug.Log("Not aiming at Ground layer.");
                    holdTimer = 0f;
                }
            }
            else
            {
                Debug.Log("Raycast did not hit anything.");
                holdTimer = 0f;
            }
        }
        else
        {
            if (holdTimer > 0f)
            {
                Debug.Log("Dig cancelled, mouse released.");
            }
            holdTimer = 0f;
        }
    }
}
