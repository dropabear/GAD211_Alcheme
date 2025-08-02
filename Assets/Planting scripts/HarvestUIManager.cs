using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HarvestUIManager : MonoBehaviour
{
    [System.Serializable]
    public class SeedUIEntry
    {
        public SeedType seedType;
        public Text seedAmountText;
        public int currentAmount = 0;
    }

    public List<SeedUIEntry> seedEntries;

    public void UpdateHarvestText(int amount, SeedType seedType)
    {
        SeedUIEntry entry = seedEntries.Find(e => e.seedType == seedType);
        if (entry != null)
        {
            entry.currentAmount += amount;

            // Build the full text every time (Label + Amount)
            entry.seedAmountText.text = $"{seedType} Seeds: {entry.currentAmount}";
        }
        else
        {
            Debug.LogWarning($"No UI entry found for seed type '{seedType}'");
        }
    }
}

