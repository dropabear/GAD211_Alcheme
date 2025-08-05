using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactDistance = 3f;

    private PlantHarvest currentPlant;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Check if the hit object has the "Plant" tag
            if (hit.collider.CompareTag("Plant"))
            {
                //Debug.Log("plant found");
                // Try to get the PlantHarvest component on this plant
                PlantHarvest plant = hit.collider.GetComponent<PlantHarvest>();

                if (plant != null)
                {
                    currentPlant = plant;
                    currentPlant.TryHarvesting();
                    return;
                }
            }
        }


        currentPlant = null;
    }
}
