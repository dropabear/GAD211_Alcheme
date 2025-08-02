using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class HarvestUIManager : MonoBehaviour
{
    [System.Serializable]
    public class SeedUIEntry
    {
        public string seedName;   // e.g. "Apple Seed"
        public Text seedAmountText;  // The UI Text to show amount for this seed
        public int currentAmount = 0; // Track the total amount harvested for this seed
    }

    [Tooltip("Assign each seed type and its corresponding UI Text")]
    public List<SeedUIEntry> seedEntries;

    /// <summary>
    /// Call this when a plant is harvested to update the correct seed text
    /// </summary>
    /// <param name="amount">Number of seeds given</param>
    /// <param name="seedName">Seed type name (must match seedEntries.seedName exactly)</param>
    public void UpdateHarvestText(int amount, string seedName)
    {
        SeedUIEntry entry = seedEntries.Find(e => e.seedName == seedName);
        if (entry != null)
        {
            entry.currentAmount += amount;
            // Show: "Apple Seeds: 4"
            entry.seedAmountText.text = $"{entry.seedName}: {entry.currentAmount}";
        }
        else
        {
            Debug.LogWarning($"HarvestUIManager: No UI entry found for seed name '{seedName}'");
        }
    }
}

