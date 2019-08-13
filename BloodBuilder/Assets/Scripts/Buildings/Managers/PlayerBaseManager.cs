using System.Collections.Generic;
using UnityEngine;

/**
 * Manager handling the creation of the player base.
 **/
public class PlayerBaseManager : AbsBuildingManager
{
    private Sprite buildingProductionSprite;

    public PlayerBaseManager(ContextProvider context) : base(context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildPlayerBaseHotkey();
        buildingProductionSprite = Resources.Load<Sprite>(GameController.GetGlobalTheme().GetPlayerBaseActionsMenuSpritePath());
    }


    protected override Building InternalCreateBuilding()
    {
        PlayerBase playerBase = new PlayerBase(context);
        playerBase.CreatePlacebleModel();
        return playerBase;
    }

    public override IBuildAction GetBuildAction(PlacementController placementController)
    {
        return new BuildBuildingAction(placementController, this);
    }

    public override int GetBuildCosts()
    {
        //For now ;)
        return 0;
    }

    public override Sprite GetUnitProductionSpriteForMenu()
    {
        return buildingProductionSprite;
    }
}
