using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHarvest : MonoBehaviour
{
    private bool playerInRange = false;
    private float harvestHoldTime = 2f;
    private float holdTimer = 0f;

    private PlantData plantData;
    private HarvestUIManager uiManager;

    void Start()
    {
        plantData = GetComponent<PlantData>();
        uiManager = FindObjectOfType<HarvestUIManager>();

        if (uiManager == null)
        {
            Debug.LogError("No HarvestUIManager found in scene!");
        }

        if (plantData == null)
        {
            Debug.LogWarning("PlantData component missing on plant.");
        }
    }

    void Update()
    {
        if (playerInRange && plantData != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                holdTimer += Time.deltaTime;
                if (holdTimer >= harvestHoldTime)
                {
                    HarvestPlant();
                    holdTimer = 0f;
                }
            }
            else
            {
                holdTimer = 0f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range of plant.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            holdTimer = 0f;
            Debug.Log("Player left plant range.");
        }
    }

    private void HarvestPlant()
    {
        Debug.Log("Plant harvested!");

        var (seedAmount, seedType) = plantData.OnHarvest();

        if (uiManager != null)
        {
            uiManager.UpdateHarvestText(seedAmount, seedType);
        }
    }
}