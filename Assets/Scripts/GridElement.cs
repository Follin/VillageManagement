using UnityEngine;

public class GridElement : MonoBehaviour
{
    public int GridId;
    public bool IsOccupied = false;
    public Building ConnectedBuilding;

    void Awake()
    {
        BuildManager building = FindObjectOfType<BuildManager>();

        for (int i = 0; i < building.GridElements.Count; i++)
        {
            if (building.GridElements[i].transform != transform) continue;
            GridId = i;
            break;
        }
    }
}
