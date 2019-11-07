using UnityEngine;


public class Building : MonoBehaviour
{
    [SerializeField] private BuildingInfo _info;
    [SerializeField] private PriceTag _price;

    [SerializeField] private string _name;
    [SerializeField] private bool _isPlaced;

    [Tooltip("How much resources the building gives")]
    [SerializeField] private int _resourceProduction = 1;

    private Resources _resources;

    void Awake()
    {
        _resources = FindObjectOfType<Resources>();
    }
    
    void Update()
    {
        if(!_isPlaced) return;

        switch (_info.Id)
        {
            case 1:
                _resources.WoodAmount += (_resourceProduction * _info.UpgradeLevel) * Time.deltaTime;
                return;
            case 2:
                _resources.StoneAmount += (_resourceProduction * _info.UpgradeLevel) * Time.deltaTime;
                return;
            case 3:
                _resources.FoodAmount += (_resourceProduction * _info.UpgradeLevel) * Time.deltaTime;
                return;
        }
    }
}

public class PriceTag
{
    public float WoodPrice;
    public float FoodPrice;
    public float StonePrice;
}

public class BuildingInfo
{
    public int Id;
    public int ConnectedGridId;

    public float UpgradeLevel = 0;
    public float YRotation = 0;
}
