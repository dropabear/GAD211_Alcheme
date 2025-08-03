using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedInventoryManager : MonoBehaviour
{
    public List<SeedEntry> seeds = new List<SeedEntry>();
    public SeedType selectedSeed;

    private HarvestUIManager uiManager;
    private SeedSelectionUI currentSelectedUI;

    void Start()
    {
        uiManager = FindObjectOfType<HarvestUIManager>();
    }

    public void SelectSeed(SeedType seed)
    {
        selectedSeed = seed;
        Debug.Log($"Selected seed: {selectedSeed}");
    }

    public void UpdateSelectionUI(SeedSelectionUI newSelection)
    {
        if (currentSelectedUI != null)
        {
            currentSelectedUI.SetHighlight(false);
        }

        currentSelectedUI = newSelection;
        currentSelectedUI.SetHighlight(true);
    }

    public bool HasSeed(SeedType seed)
    {
        SeedEntry entry = seeds.Find(e => e.seedType == seed);
        return entry != null && entry.seedCount > 0;
    }

    public bool UseSeed(SeedType seed)
    {
        SeedEntry entry = seeds.Find(e => e.seedType == seed);
        if (entry != null && entry.seedCount > 0)
        {
            entry.seedCount--;
            Debug.Log($"{seed} seed used. Remaining: {entry.seedCount}");

            if (uiManager != null)
            {
                uiManager.UpdateHarvestText(-1, seed);
            }
            return true;
        }
        Debug.Log($"No {seed} seeds left to plant!");
        return false;
    }
}
