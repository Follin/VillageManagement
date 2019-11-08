using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _houseName;
    [SerializeField] private Button _destroyButton;
    [SerializeField] private Button _upgradeBuilding;

    private BuildManager _build;
    private Building _selectedBuilding;
    private Resources _resource;

    void Awake()
    {
        _build = FindObjectOfType<BuildManager>();
        _resource = FindObjectOfType<Resources>();
    }
    
    void Update()
    {
        if (_build.CurrentSelectedElement != null && _build.CurrentSelectedElement.ConnectedBuilding != null)
        {
            _selectedBuilding = _build.CurrentSelectedElement.ConnectedBuilding;
            _houseName.text = _selectedBuilding.Name + "\nLevel: " + _selectedBuilding.Info.UpgradeLevel;
        }
        else
        {
            _houseName.text = "No building selected";
            _selectedBuilding = null;
        }

        _destroyButton.interactable = _selectedBuilding;

        bool canDestroy = false;
        if (_selectedBuilding)
        {
            if(_resource.WoodAmount >= _selectedBuilding._price.WoodPrice &&
               _resource.StoneAmount >= _selectedBuilding._price.StonePrice &&
               _resource.FoodAmount >= _selectedBuilding._price.FoodPrice)
            {
               canDestroy = true;
            }
            _destroyButton.interactable = canDestroy;
        }
        else
            _destroyButton.interactable = false;
        
    }

    public void UpgradeHouse()
    {
        if (_selectedBuilding)
            _selectedBuilding.UpgradeBuilding();
    }

    public void DestroyBuilding()
    {
        if (!_selectedBuilding) return;

        _build.CurrentSelectedElement.IsOccupied = false;
        _build.Buildings.BuiltObjects.Remove(_selectedBuilding.gameObject);
        Destroy(_selectedBuilding.gameObject);
    }
}
