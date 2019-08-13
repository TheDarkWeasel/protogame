using System.Collections.Generic;
using UnityEngine;

/**
 * Manager handling the creation of barracks.
 **/ 
public class BarracksManager : AbsBuildingManager
{
    private Sprite buildingProductionSprite;

    public BarracksManager(ContextProvider context) : base(context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildBarracksHotkey();
        buildingProductionSprite = Resources.Load<Sprite>(GameController.GetGlobalTheme().GetBarracksActionsMenuSpritePath());
    }


    protected override Building InternalCreateBuilding()
    {
        Barracks barracks = new Barracks(context);
        barracks.CreatePlacebleModel();
        return barracks;
    }

    public override IBuildAction GetBuildAction(PlacementController placementController)
    {
        return new BuildBuildingAction(placementController, this);
    }

    public override int GetBuildCosts()
    {
        return 3;
    }

    public override Sprite GetUnitProductionSpriteForMenu()
    {
        return buildingProductionSprite;
    }
}
