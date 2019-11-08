using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public GridElement CurrentSelectedElement;
    private GridElement _currentHoveredElement;
    [HideInInspector] public Buildings Buildings;

    [Header("Colors")]
    [SerializeField] Color _hoveredColor = Color.white;
    [SerializeField] Color _occupiedColor = Color.red;

    [Header("Settings")]
    [SerializeField] private GameObject _gridElementParent;

    private Color _defaultColor;
    private bool _buildInProgress;
    private GameObject _currentBuilding = null;

    [HideInInspector] public List<GridElement> GridElements;

    void Awake()
    {
        Buildings = GetComponent<Buildings>();

        for (int i = 0; i < _gridElementParent.transform.childCount; i++)
            GridElements.Add(_gridElementParent.transform.GetChild(i).GetComponent<GridElement>());

        _defaultColor = GridElements[0].GetComponentInChildren<MeshRenderer>().material.color;
    }

    void Update()
    {
        HoverFunction();
        MoveBuilding();
        PlaceBuilding();
    }

    public void CreateBuilding(int id)
    {
        if(_buildInProgress) return;

        GameObject go = null;

        foreach (GameObject building in Buildings.BuildingsToBuy)
        {
            Building b = building.GetComponent<Building>();
            if (b.Info.Id == id)
                go = b.gameObject;
        }

        _currentBuilding = Instantiate(go);
        _currentBuilding.transform.rotation = Quaternion.Euler(0, transform.rotation.y - 225, 0);
        _buildInProgress = true;
    }

    void HoverFunction()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            GridElement elementPlacement = hit.transform.GetComponent<GridElement>();

            if (!elementPlacement)
            {
                if (_currentHoveredElement)
                {
                    _currentHoveredElement.GetComponent<MeshRenderer>().material.color = _defaultColor;
                    return;
                }
            }

            if (Input.GetMouseButton(0))
                CurrentSelectedElement = elementPlacement;

            if (elementPlacement != _currentHoveredElement)
                hit.transform.GetComponent<MeshRenderer>().material.color =
                    !elementPlacement.IsOccupied ? _hoveredColor : _occupiedColor;

            if (_currentHoveredElement && _currentHoveredElement != elementPlacement)
                _currentHoveredElement.GetComponent<MeshRenderer>().material.color = _defaultColor;

            _currentHoveredElement = elementPlacement;
        }
        else
        {
            if (_currentHoveredElement)
                _currentHoveredElement.GetComponent<MeshRenderer>().material.color = _defaultColor;
        }

    }

    void MoveBuilding()
    {
        if(!_currentBuilding) return;

        _currentBuilding.gameObject.layer = 2;

        if (_currentHoveredElement)
            _currentBuilding.transform.position = _currentHoveredElement.transform.position;

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(_currentBuilding);
            _currentBuilding = null;
            _buildInProgress = false;
        }

        if (Input.GetMouseButton(2) && _currentBuilding != null)
            _currentBuilding.transform.Rotate(transform.up * 70 * Time.deltaTime);
    }

    void PlaceBuilding()
    {
        if(!_currentBuilding || _currentHoveredElement.IsOccupied)
            return;

        if (Input.GetMouseButtonDown(0))
        {
            Buildings.BuiltObjects.Add(_currentBuilding);
            _currentHoveredElement.IsOccupied = true;

            Building building = _currentBuilding.GetComponent<Building>();

            _currentHoveredElement.ConnectedBuilding = building;
            building.IsPlaced = true;

            building.Info.ConnectedGridId = _currentHoveredElement.GridId;
            building.Info.YRotation = building.transform.localEulerAngles.y;

            building.UpgradeBuilding();

            _currentBuilding = null;
            _buildInProgress = false;
        }
    }
}
