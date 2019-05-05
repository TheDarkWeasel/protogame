using UnityEngine;
using System.Collections.Generic;

public class PlacementController
{
    private List<BuildingManager> buildingManagers; 
    private Building buildingToPlace;
    private Vector3 specificVector = new Vector3();

    public PlacementController()
    {
        buildingManagers = new List<BuildingManager>();
    }

    public void RegisterBuildingManager(BuildingManager manager)
    {
        buildingManagers.Add(manager);
    }

    public void UnregisterBuildingManager(BuildingManager manager)
    {
        buildingManagers.Remove(manager);
    }

    public void Update()
    {
        bool newBuilding = HandleNewObjectHotkey();

        if (buildingToPlace != null)
        {
            MoveCurrentObjectToMouse(newBuilding);
            ReleaseIfClicked();
        }
    }

    /**
     * Returns true, if a new building was created
     */
    private bool HandleNewObjectHotkey()
    {
        foreach(BuildingManager manager in buildingManagers)
        {
            if (Input.GetKeyDown(manager.GetPlacementHotkey()))
            {
                if(buildingToPlace == null)
                {
                    buildingToPlace = manager.CreateBuilding();
                    return true;
                } else
                {
                    manager.ReleaseBuilding(buildingToPlace);
                    buildingToPlace = null;
                }
                //Duplicate keycodes are not supported
                break;
            }
        }

        return false;
    }

    private void MoveCurrentObjectToMouse(bool forceMove)
    {
        if(forceMove || HasMouseMoved())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                specificVector.Set(hitInfo.point.x, hitInfo.collider.transform.position.y, hitInfo.point.z);
                buildingToPlace.SetPosition(specificVector);
            }
        }
    }

    private bool HasMouseMoved()
    {
        return (Input.GetAxis("Mouse X") != 0) || (Input.GetAxis("Mouse Y") != 0);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            buildingToPlace = null;
        }
    }
}
