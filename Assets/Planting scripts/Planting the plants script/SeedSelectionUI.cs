using UnityEngine;
using UnityEngine.UI;

public class SeedSelectionUI : MonoBehaviour
{
    public SeedType seedType; // This button represents this seed type
    public Image highlightImage; // UI Image that acts as a highlight/border

    private SeedInventoryManager inventoryManager;

    void Start()
    {
        inventoryManager = FindObjectOfType<SeedInventoryManager>();

        if (highlightImage != null)
            highlightImage.enabled = false; // Turn off highlight by default
    }

    public void OnSelectSeed()
    {
        if (inventoryManager != null)
        {
            inventoryManager.SelectSeed(seedType);
            inventoryManager.UpdateSelectionUI(this);
        }
    }

    // This will be called by the manager to turn highlight on/off
    public void SetHighlight(bool active)
    {
        if (highlightImage != null)
            highlightImage.enabled = active;
    }
}
