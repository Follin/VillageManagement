using System;
using UnityEngine;


public class Building : MonoBehaviour
{
    public BuildingInfo Info;
    public PriceTag _price;

    public string Name;
    [HideInInspector] public bool IsPlaced;

    [Tooltip("How much resources the building gives")]
    [SerializeField] private int _resourceProduction = 1;

    private Resources _resources;

    void Awake()
    {
        _resources = FindObjectOfType<Resources>();
    }
    
    void Update()
    {
        if(!IsPlaced) return;

        switch (Info.Id)
        {
            case 1:
                _resources.WoodAmount += (_resourceProduction * Info.UpgradeLevel) * Time.deltaTime;
                return;
            case 2:
                _resources.StoneAmount += (_resourceProduction * Info.UpgradeLevel) * Time.deltaTime;
                return;
            case 3:
                _resources.FoodAmount += (_resourceProduction * Info.UpgradeLevel) * Time.deltaTime;
                return;
        }
    }

    public void UpgradeBuilding()
    {
        Info.UpgradeLevel++;

        _resources.WoodAmount -= _price.WoodPrice;
        _resources.StoneAmount -= _price.StonePrice;
        _resources.FoodAmount -= _price.FoodPrice;
    }
}

[Serializable]
public class PriceTag
{
    public float WoodPrice;
    public float FoodPrice;
    public float StonePrice;
}

[Serializable]
public class BuildingInfo
{
    public int Id;
    public int ConnectedGridId;

    public float UpgradeLevel = 0;
    public float YRotation = 0;
}
