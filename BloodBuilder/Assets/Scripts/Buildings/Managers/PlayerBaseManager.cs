using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseManager : AbsBuildingManager
{
    public PlayerBaseManager(ContextProvider context) : base(context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildPlayerBaseHotkey();
    }


    public override Building CreateBuilding()
    {
        PlayerBase playerBase = new PlayerBase(context);
        playerBase.CreatePlacebleModel();
        return playerBase;
    }

    public override int GetBuildCosts()
    {
        //For now ;)
        return 0;
    }
}
