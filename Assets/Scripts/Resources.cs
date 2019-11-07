using TMPro;
using UnityEngine;

public class Resources : MonoBehaviour
{
    [Header("Resource Values")]
    public float WoodAmount;
    public float StoneAmount;
    public float FoodAmount;

    [Header("UI References")]
    [SerializeField] private TextMeshProUGUI _resourcesText;
    
    void FixedUpdate()
    {
        _resourcesText.text = "Wood: " + WoodAmount.ToString("F0") + " | Stone: " + StoneAmount.ToString("F0") + " | Food: " + FoodAmount.ToString("F0");
    }
}
