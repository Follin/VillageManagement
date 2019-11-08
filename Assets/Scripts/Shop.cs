using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int ConnectedBuildingId;

    [HideInInspector] public Building ConnectedBuilding;
    public TextMeshProUGUI ResourceText;

    private Button _button;
    private Resources _resource;

    void Awake()
    {
        _button = GetComponent<Button>();
        _resource = FindObjectOfType<Resources>();

        Buildings buildings = FindObjectOfType<Buildings>();

        foreach (GameObject go in buildings.BuildingsToBuy)
        {
            Building b = go.GetComponent<Building>();
            if (b.Info.Id == ConnectedBuildingId)
            {
                ConnectedBuilding = b;
                break;
            }
        }
        ResourceText.text = ConnectedBuilding._price.WoodPrice + " Wo | " + ConnectedBuilding._price.StonePrice +
        " St | " + ConnectedBuilding._price.FoodPrice + " Fo";
    }
    
    void Update()
    {
        bool canBuy = _resource.WoodAmount >= ConnectedBuilding._price.WoodPrice &&
            _resource.StoneAmount >= ConnectedBuilding._price.StonePrice &&
            _resource.FoodAmount >= ConnectedBuilding._price.FoodPrice;

        _button.interactable = canBuy;
    }
}
