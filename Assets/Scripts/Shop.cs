using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public int ConnectedBuildingId;

    public Building ConnectedBuilding;
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
                ConnectedBuilding = b;
        }
    }
    
    void Update()
    {
        
    }
}
