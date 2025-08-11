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

    [Header("Audio Settings")]
    public AudioClip harvestSuccessSound;
    public AudioClip plantDestroyedSound;

    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource missing on Plant object!");
        }
    }

    public (int, SeedType) OnHarvest()
    {
        int seedsGiven = Random.Range(minSeeds, maxSeeds + 1);
        Debug.Log($"{plantName} gave {seedsGiven} {seedType} seeds!");

        // Play harvest sound
        if (harvestSuccessSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(harvestSuccessSound);
        }

        float roll = Random.Range(0f, 100f);
        if (roll <= survivalChance)
        {
            Debug.Log($"{plantName} survived after harvesting!");
        }
        else
        {
            Debug.Log($"{plantName} was destroyed after harvesting.");

            // Play destroyed sound
            if (plantDestroyedSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(plantDestroyedSound);
            }

            Destroy(gameObject, plantDestroyedSound != null ? plantDestroyedSound.length : 0f); // Optional: Delay destroy till sound plays
        }

        return (seedsGiven, seedType);
    }
}
