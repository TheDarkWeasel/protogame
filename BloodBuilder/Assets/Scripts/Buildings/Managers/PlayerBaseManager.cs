using UnityEngine;

public class PlayerBaseManager : BuildingManager
{

    protected KeyCode placementHotkey;

    public PlayerBaseManager()
    {
        placementHotkey = KeyCode.B;
    }

    Building BuildingManager.CreateBuilding()
    {
        PlayerBase playerBase = new PlayerBase();
        playerBase.CreatePlacebleModel();
        return playerBase;
    }

    public void ReleaseBuilding(Building building)
    {
        building.Destroy();
    }

    public KeyCode GetPlacementHotkey()
    {
        return placementHotkey;
    }
}
