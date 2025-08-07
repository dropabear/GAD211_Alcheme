using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantHarvest : MonoBehaviour
{
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

    // Called externally when looking at this plant and holding E
    public bool TryHarvesting()
    {
        if (plantData == null)
            return false;

        if (Input.GetKey(KeyCode.E))
        {
            holdTimer += Time.deltaTime;


            if (holdTimer >= harvestHoldTime)
            {
                Debug.Log("harvesting");
                HarvestPlant();
                holdTimer = 0f;
                return true;
            }
        }
        else
        {
            holdTimer = 0f;
        }

        return false;
    }

    private void HarvestPlant()
    {
        Debug.Log("Plant harvested!");

        var (seedAmount, seedType) = plantData.OnHarvest();

        // 1. Update UI
        if (uiManager != null)
        {
            uiManager.UpdateHarvestText(seedAmount, seedType);
        }

        // 2. Add seeds to inventory
        var inventory = SeedInventoryManager.Instance;
        if (inventory != null)
        {
            var entry = inventory.seedInventory.Find(e => e.seedType == seedType);
            if (entry != null)
            {
                entry.count += seedAmount;
            }
            else
            {
                // First time collecting this seed type
                inventory.seedInventory.Add(new SeedEntry
                {
                    seedType = seedType,
                    count = seedAmount
                });
            }

            Debug.Log($"Added {seedAmount} {seedType} seeds to inventory.");
        }
        else
        {
            Debug.LogError("SeedInventoryManager.Instance is null!");
        }
    }
}