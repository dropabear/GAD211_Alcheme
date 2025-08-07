using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [SerializeField] public GameObject plantingMenuUI;

    public bool isPlanted = false;
    public PlotPlantingUI plantingUI;

    public void Setup(GameObject menuUI)
    {
        if (menuUI == null)
        {
            Debug.LogError("Menu UI reference is null! Cannot initialize Plot.");
            return;
        }

        plantingMenuUI = menuUI;
        plantingUI = plantingMenuUI.GetComponent<PlotPlantingUI>();
        if (plantingUI == null)
        {
            Debug.LogError("PlotPlantingUI component missing on provided menuUI GameObject!");
            return;
        }

        plantingMenuUI.SetActive(false);
    }

    public void PlantingMenu()
    {
        Debug.Log("Test");

        if (isPlanted || plantingMenuUI == null || plantingUI == null)
        {
            Debug.Log("Conditions failed: isPlanted = " + isPlanted + ", plantingMenuUI = " + plantingMenuUI + ", plantingUI = " + plantingUI);
            return;
        }

        if (plantingMenuUI.activeSelf)
        {
            Debug.Log("Menu is already open.");
            return;
        }

        Debug.Log("Activating menu...");
        plantingMenuUI.SetActive(true);
        plantingUI.Initialize(this);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void PlantSeed(SeedType seedType)
    {
        if (isPlanted) return;

        var seedInventory = SeedInventoryManager.Instance ?? FindObjectOfType<SeedInventoryManager>();
        if (seedInventory == null)
        {
            Debug.LogError("SeedInventoryManager not found!");
            return;
        }

        GameObject prefab = seedInventory.GetSeedlingPrefab(seedType);
        if (prefab == null)
        {
            Debug.LogWarning($"No seedling prefab found for seed type {seedType}");
            return;
        }

        Instantiate(prefab, transform.position + Vector3.up * 0.1f, Quaternion.identity);
        isPlanted = true;

        plantingMenuUI.SetActive(false);
    }

    public void CloseMenu()
    {
        if (plantingMenuUI != null && plantingMenuUI.activeSelf)
        {
            plantingMenuUI.SetActive(false);
        }
    }

    public void SetPlantingMenuUI(GameObject ui)
    {
        if (ui == null)
        {
            Debug.LogError("SetPlantingMenuUI: Provided UI is null.");
            return;
        }

        plantingMenuUI = ui;
        plantingUI = ui.GetComponent<PlotPlantingUI>();

        if (plantingUI == null)
        {
            Debug.LogError("SetPlantingMenuUI: PlotPlantingUI component missing on UI GameObject.");
        }

        plantingMenuUI.SetActive(false);
    }
}
