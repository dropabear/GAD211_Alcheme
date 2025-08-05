using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlotInputManager : MonoBehaviour
{
    public GameObject plantingMenuPanelInScene; // assign in inspector
    private Plot selectedPlot;

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Debug.Log("Casting ray from mouse position.");

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");
                Plot plot = hit.collider.GetComponent<Plot>();
                if (plot != null)
                {
                    Debug.Log("Plot found!");
                    selectedPlot = plot;

                    if (selectedPlot.plantingMenuUI == null)
                    {
                        selectedPlot.Setup(plantingMenuPanelInScene);
                    }

                    selectedPlot.PlantingMenu();
                }
                else
                {
                    Debug.Log("Hit object does not have Plot component.");
                }
            }
            else
            {
                Debug.Log("Raycast hit nothing.");
            }
        }
    }
}
