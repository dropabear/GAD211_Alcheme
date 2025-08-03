using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    public bool isOccupied = false;
    public Transform seedlingSpawnPoint;
    public GameObject seedlingPrefab;

    private SeedInventoryManager seedInventory;

    void Start()
    {
        seedInventory = FindObjectOfType<SeedInventoryManager>();
    }

    private void OnMouseDown()
    {
        if (isOccupied)
        {
            Debug.Log("This plot is already occupied.");
            return;
        }

        if (!seedInventory.HasSeed(seedInventory.selectedSeed))
        {
            Debug.Log("No seeds available for planting!");
            return;
        }

        bool planted = seedInventory.UseSeed(seedInventory.selectedSeed);
        if (planted)
        {
            PlantSeed();
        }
    }

    private void PlantSeed()
    {
        Debug.Log($"Planting {seedInventory.selectedSeed} in plot at {transform.position}");

        GameObject seedling = Instantiate(seedlingPrefab, seedlingSpawnPoint.position, Quaternion.identity);

        Seedling seedlingScript = seedling.GetComponent<Seedling>();
        if (seedlingScript != null)
        {
            seedlingScript.seedType = seedInventory.selectedSeed;
        }

        isOccupied = true;
    }
}
