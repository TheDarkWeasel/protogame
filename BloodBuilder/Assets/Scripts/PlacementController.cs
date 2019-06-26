using UnityEngine;
using System.Collections.Generic;

public class PlacementController
{
    private List<BuildingManager> buildingManagers;
    private Building buildingToPlace;
    private Vector3 specificVector = new Vector3();
    private PlayerObjectPool playerObjectPool;
    private BuildingManager activeManager;
    private RaycastHit hitInfo;

    private Camera mainCamera;

    public PlacementController(ContextProvider contextProvider)
    {
        buildingManagers = new List<BuildingManager>();
        this.playerObjectPool = contextProvider.GetPlayerObjectPool();
        mainCamera = Camera.main;
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

    public bool IsActive()
    {
        return buildingToPlace != null;
    }

    /**
     * Returns true, if a new building was created
     */
    private bool HandleNewObjectHotkey()
    {
        foreach (BuildingManager manager in buildingManagers)
        {
            if (Input.GetKeyDown(manager.GetPlacementHotkey()))
            {
                //TODO check efficiency
                if (manager.GetBuildCosts() <= playerObjectPool.GetBloodAmountOfObjects(playerObjectPool.GetSacrificableSelectedObjects()))
                {
                    if (buildingToPlace == null)
                    {
                        buildingToPlace = manager.CreateBuilding();
                        activeManager = manager;
                        return true;
                    }
                    else
                    {
                        manager.ReleaseBuilding(buildingToPlace);
                        activeManager = null;
                        buildingToPlace = null;
                    }
                }
                //Duplicate keycodes are not supported
                break;
            }
        }

        return false;
    }

    private void MoveCurrentObjectToMouse(bool forceMove)
    {
        if (forceMove || HasMouseMoved())
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

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
        if (Input.GetMouseButtonUp(0))
        {
            //TODO check efficiency
            List<SacrificableSelectableObject> sacrificableSelectableObjects = playerObjectPool.GetSacrificableSelectedObjects();
            if (activeManager.GetBuildCosts() <= playerObjectPool.GetBloodAmountOfObjects(sacrificableSelectableObjects))
            {
                int sacrificedBlood = 0;
                foreach (SacrificableSelectableObject sacrificable in sacrificableSelectableObjects)
                {
                    if (sacrificedBlood < activeManager.GetBuildCosts())
                    {
                        sacrificable.Sacrifice();
                        sacrificedBlood += sacrificable.GetBloodAmount();
                    }
                }
                activeManager.PlaceBuilding(buildingToPlace);
                activeManager = null;
                buildingToPlace = null;
            }
        }
    }
}
