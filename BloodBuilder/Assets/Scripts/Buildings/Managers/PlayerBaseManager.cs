using UnityEngine;

public class PlayerBaseManager : BuildingManager
{

    protected KeyCode placementHotkey;
    protected ContextProvider context;

    public PlayerBaseManager(ContextProvider context)
    {
        placementHotkey = KeyCode.B;
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
        building.Destroy();
    }

    public KeyCode GetPlacementHotkey()
    {
        return placementHotkey;
    }
}
