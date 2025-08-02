using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantData : MonoBehaviour
{
    [Header("Plant Settings")]
    public string plantName = "Default Plant";

    [Header("Seed Settings")]
    public SeedType seedType;

    [Header("Harvest Settings")]
    public int minSeeds = 1;
    public int maxSeeds = 3;

    [Range(0f, 100f)]
    public float survivalChance = 25f;

    public (int, SeedType) OnHarvest()
    {
        int seedsGiven = Random.Range(minSeeds, maxSeeds + 1);
        Debug.Log($"{plantName} gave {seedsGiven} {seedType} seeds!");

        float roll = Random.Range(0f, 100f);
        if (roll <= survivalChance)
        {
            Debug.Log($"{plantName} survived after harvesting!");
        }
        else
        {
            Debug.Log($"{plantName} was destroyed after harvesting.");
            Destroy(gameObject);
        }

        return (seedsGiven, seedType);
    }
}
