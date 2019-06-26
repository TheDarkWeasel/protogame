using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseManager : BuildingManager
{
    protected KeyCode placementHotkey;
    protected ContextProvider context;

    protected List<Building> placedBuildings = new List<Building>();

    public PlayerBaseManager(ContextProvider context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildPlayerBaseHotkey();
        this.context = context;
    }

    Building BuildingManager.CreateBuilding()
    {
        PlayerBase playerBase = new PlayerBase(context);
        playerBase.CreatePlacebleModel();
        return playerBase;
    }

    public void ReleaseBuilding(Building building)
    {
        if(placedBuildings.Contains(building))
        {
            placedBuildings.Remove(building);
        }
        building.Destroy();
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
        foreach(Building building in placedBuildings)
        {
            switch (selectionState)
            {
                case SelectionState.SELECTED:
                    if(building.IsSelected())
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

    public int GetBuildCosts()
    {
        //For now ;)
        return 0;
    }
}
