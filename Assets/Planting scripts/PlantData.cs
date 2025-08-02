using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    [Header("Plant Settings")]
    public string plantName = "Default Plant";

    [Header("Seed Settings")]
    public string seedName = "Default Seed";  // e.g. "Apple Seed"

    [Header("Harvest Settings")]
    public int minSeeds = 1;
    public int maxSeeds = 3;

    [Range(0f, 100f)]
    public float survivalChance = 25f; // % chance plant does NOT get destroyed after harvesting

    // Return a tuple with seed amount and seed name to update UI
    public (int, string) OnHarvest()
    {
        int seedsGiven = Random.Range(minSeeds, maxSeeds + 1);
        Debug.Log($"{plantName} gave {seedsGiven} {seedName}(s)!");

        float roll = Random.Range(0f, 100f);
        if (roll <= survivalChance)
        {
            Debug.Log($"{plantName} survived after harvesting!");
            // Optional: play regrow animation or leave as is.
        }
        else
        {
            Debug.Log($"{plantName} was destroyed after harvesting.");
            Destroy(gameObject);
        }

        return (seedsGiven, seedName);
    }
}
