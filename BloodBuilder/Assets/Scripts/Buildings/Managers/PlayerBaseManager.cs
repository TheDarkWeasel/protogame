using System.Collections.Generic;
using UnityEngine;

public class PlayerBaseManager : AbsBuildingManager
{
    private Sprite buildingProductionSprite;

    public PlayerBaseManager(ContextProvider context) : base(context)
    {
        placementHotkey = GameController.GetHotkeys().GetBuildPlayerBaseHotkey();
        buildingProductionSprite = Resources.Load<Sprite>(GameController.GetGlobalTheme().GetPlayerBaseActionsMenuSpritePath());
    }


    public override Building CreateBuilding()
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
