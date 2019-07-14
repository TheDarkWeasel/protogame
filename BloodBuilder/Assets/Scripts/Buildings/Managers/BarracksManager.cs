using System.Collections.Generic;
using UnityEngine;

public class BarracksManager : AbsBuildingManager
{
    private Sprite buildingProductionSprite;

    public BarracksManager(ContextProvider context) : base(context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildBarracksHotkey();
        buildingProductionSprite = Resources.Load<Sprite>(GameController.GetGlobalTheme().GetBarracksActionsMenuSpritePath());
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

    public override Sprite getUnitProductionSpriteForMenu()
    {
        return buildingProductionSprite;
    }
}
