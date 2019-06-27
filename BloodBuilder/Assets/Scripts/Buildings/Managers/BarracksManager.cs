using System.Collections.Generic;
using UnityEngine;

public class BarracksManager : AbsBuildingManager
{

    public BarracksManager(ContextProvider context) : base(context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildBarracksHotkey();
    }


    public override Building CreateBuilding()
    {
        Barracks barracks = new Barracks(context);
        barracks.CreatePlacebleModel();
        return barracks;
    }

    public override int GetBuildCosts()
    {
        return 3;
    }
}
