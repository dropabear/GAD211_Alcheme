using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static HarvestUIManager;

public class SeedInventoryManager : MonoBehaviour
{
    public static SeedInventoryManager Instance { get; private set; }

    public List<SeedEntry> seedInventory = new List<SeedEntry>();
    public List<SeedTypeToPrefab> seedPrefabs;

    void Awake()
    {
        Instance = this;
    }

    public bool UseSeed(SeedType type)
    {
        var entry = seedInventory.Find(e => e.seedType == type);
        if (entry != null && entry.count > 0)
        {
            entry.count--;
            return true;
        }
        return false;
    }

    public GameObject GetSeedlingPrefab(SeedType type)
    {
        return seedPrefabs.FirstOrDefault(p => p.seedType == type)?.prefab;
    }
}

[System.Serializable]
public class SeedEntry
{
    public SeedType seedType;
    public int count;
}

[System.Serializable]
public class SeedTypeToPrefab
{
    public SeedType seedType;
    public GameObject prefab;
}