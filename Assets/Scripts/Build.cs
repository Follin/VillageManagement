using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build : MonoBehaviour
{
    public GridElement CurrentSelectedElement;
    public GridElement CurrentHoveredElement;

    public GridElement[] GridElements;

    [Header("Colors")]
    public Color HoveredColor = Color.white;
    public Color OccupiedColor = Color.red;


    private Color _defaultColor;


    void Awake()
    {
        _defaultColor = GridElements[0].GetComponentInChildren<MeshRenderer>().material.color;
    }

    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {

            GridElement o = hit.transform.GetComponent<GridElement>();

            if (!o)
            {
                if (CurrentHoveredElement)
                {
                    CurrentHoveredElement.GetComponent<MeshRenderer>().material.color = _defaultColor;
                    return;
                }
            }

            if (Input.GetMouseButton(0))
                CurrentSelectedElement = o;

            if (o != CurrentHoveredElement)
                hit.transform.GetComponent<MeshRenderer>().material.color =
                    !o.IsOccupied ? HoveredColor : OccupiedColor;

            if (CurrentHoveredElement && CurrentHoveredElement != o)
                CurrentHoveredElement.GetComponent<MeshRenderer>().material.color = _defaultColor;

            CurrentHoveredElement = o;
        }
        else
        {
            if(CurrentHoveredElement)
                CurrentHoveredElement.GetComponent<MeshRenderer>().material.color = _defaultColor;
        }

    }
}
