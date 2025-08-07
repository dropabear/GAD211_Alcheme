using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlotPlantingUI : MonoBehaviour
{
    public Transform buttonParent;
    public GameObject seedButtonPrefab;
    private Plot currentPlot;

    public void Initialize(Plot plot)
    {
        currentPlot = plot;
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        Debug.Log($"Seed inventory count: {SeedInventoryManager.Instance.seedInventory.Count}");

        foreach (SeedEntry entry in SeedInventoryManager.Instance.seedInventory)
        {
            Debug.Log($"Checking seed {entry.seedType} with count {entry.count}");
            if (entry.count <= 0) continue;

            GameObject buttonObj = Instantiate(seedButtonPrefab, buttonParent);
            SeedSelectionUI button = buttonObj.GetComponent<SeedSelectionUI>();
            button.Initialize(entry.seedType, this);
        }
    }

    public void OnSeedButtonClicked(SeedType seedType)
    {
        bool planted = SeedInventoryManager.Instance.UseSeed(seedType);
        if (planted)
        {
            currentPlot.PlantSeed(seedType);
            currentPlot.CloseMenu();
            Destroy(gameObject);
        }
    }
}
