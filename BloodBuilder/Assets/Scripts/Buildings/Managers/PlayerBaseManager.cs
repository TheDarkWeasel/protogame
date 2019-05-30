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

    public List<PlayerSelectableObject> GetPlayerSelectableObjects()
    {
        List<PlayerSelectableObject> result = new List<PlayerSelectableObject>();
        foreach (var selectableObject in placedBuildings)
        {
            result.Add(selectableObject);
        }
        return result;
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
}
