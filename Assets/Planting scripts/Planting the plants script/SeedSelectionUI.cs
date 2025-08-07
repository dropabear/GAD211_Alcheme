using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SeedSelectionUI : MonoBehaviour
{
    public TextMeshProUGUI seedNameText; // Updated to TMP
    private SeedType seedType;
    private PlotPlantingUI ui;

    public void Initialize(SeedType type, PlotPlantingUI plantingUI)
    {
        seedType = type;
        ui = plantingUI;
        seedNameText.text = type.ToString();
    }

    public void OnClick()
    {
        ui.OnSeedButtonClicked(seedType);
    }
}
