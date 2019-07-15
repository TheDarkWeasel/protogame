using UnityEngine;
using UnityEngine.EventSystems;
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

    public PlacementController(PlayerObjectPool playerObjectPool)
    {
        buildingManagers = new List<BuildingManager>();
        this.playerObjectPool = playerObjectPool;
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
        HandleNewObjectHotkey();

        if (buildingToPlace != null)
        {
            MoveCurrentObjectToMouse(false);
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
    private void HandleNewObjectHotkey()
    {
        foreach (BuildingManager manager in buildingManagers)
        {
            if (Input.GetKeyDown(manager.GetPlacementHotkey()))
            {
                if (buildingToPlace == null)
                {
                    BuildBuilding(manager);
                }
                else
                {
                    manager.ReleaseBuilding(buildingToPlace);
                    activeManager = null;
                    buildingToPlace = null;
                }
                //Duplicate keycodes are not supported
                break;
            }
        }
    }

    public void BuildBuilding(BuildingManager manager)
    {
        if (manager.GetBuildCosts() <= PlayerResources.GetInstance().GetResourceCount(PlayerResources.PlayerResource.SELECTED_BLOOD))
        {
            if (buildingToPlace != null)
            {
                activeManager.ReleaseBuilding(buildingToPlace);
            }
            buildingToPlace = manager.CreateBuilding();
            activeManager = manager;
            MoveCurrentObjectToMouse(true);
        }
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
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Build building
            //TODO check efficiency
            List<SacrificableSelectableObject> sacrificableSelectableObjects = playerObjectPool.GetSacrificableSelectedObjects();
            if (activeManager.GetBuildCosts() <= PlayerResources.GetInstance().GetResourceCount(PlayerResources.PlayerResource.SELECTED_BLOOD))
            {
                int sacrificedBlood = 0;
                foreach (SacrificableSelectableObject sacrificable in sacrificableSelectableObjects)
                {
                    sacrificable.Select(false);
                    sacrificable.DestroySelectionCircle();
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
        else if (Input.GetMouseButtonUp(1))
        {
            //Cancel building
            activeManager.ReleaseBuilding(buildingToPlace);
            activeManager = null;
            buildingToPlace = null;
        }
    }
}
