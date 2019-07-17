using System.Collections.Generic;
using UnityEngine;

public abstract class AbsBuildingManager : BuildingManager
{
    protected KeyCode placementHotkey;
    protected ContextProvider context;

    private ObjectPool<Building> buildingPool;

    protected List<Building> placedBuildings = new List<Building>();

    public AbsBuildingManager(ContextProvider context)
    {
        this.context = context;
        this.buildingPool = new ObjectPool<Building>(() => InternalCreateBuilding(), (b) => InternalActivateBuilding(b), (b) => InternalDeactivateBuilding(b));
    }

    public Building CreateBuilding()
    {
        return buildingPool.GetObject();
    }

    private void InternalDeactivateBuilding(Building building)
    {
        building.GetGameObject().SetActive(false);
        building.DestroySelectionCircle();
        building.StopProductionQueue();
        building.Context = null;
    }

    private void InternalActivateBuilding(Building building)
    {
        building.Context = context;
        building.GetGameObject().SetActive(true);
    }

    protected abstract Building InternalCreateBuilding();

    public void ReleaseBuilding(Building building)
    {
        if (placedBuildings.Contains(building))
        {
            placedBuildings.Remove(building);
        }
        buildingPool.PutObject(building);
    }

    public void PlaceBuilding(Building building)
    {
        building.OnPlaced();
        placedBuildings.Add(building);
    }

    public KeyCode GetPlacementHotkey()
    {
        return placementHotkey;
    }

    public bool GetPlayerSelectableObjects(List<PlayerSelectableObject> outParam, SelectionState selectionState)
    {
        bool added = false;
        foreach (Building building in placedBuildings)
        {
            switch (selectionState)
            {
                case SelectionState.SELECTED:
                    if (building.IsSelected())
                    {
                        outParam.Add(building);
                    }
                    break;
                case SelectionState.UNSELECTED:
                    if (!building.IsSelected())
                    {
                        outParam.Add(building);
                    }
                    break;
                case SelectionState.ALL:
                    outParam.Add(building);
                    break;
            }
            added = true;
        }

        return added;
    }

    public bool GetSacrificableSelectableObjects(List<SacrificableSelectableObject> outParam, SelectionState selectionState)
    {
        return false;
    }

    public abstract int GetBuildCosts();
    public abstract Sprite GetUnitProductionSpriteForMenu();
    public abstract IBuildAction GetBuildAction(PlacementController placementController);
}
