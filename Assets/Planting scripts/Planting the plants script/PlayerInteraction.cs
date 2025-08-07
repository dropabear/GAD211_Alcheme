using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactDistance = 10f;

    private PlantHarvest currentPlant;

    void Update()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            // Check if the hit object has the "Plant" tag
            if (hit.collider.CompareTag("Plant"))
            {
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
