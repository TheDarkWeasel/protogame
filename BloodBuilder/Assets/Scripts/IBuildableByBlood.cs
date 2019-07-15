using UnityEngine;

public interface IBuildableByBlood
{
    int GetBuildCosts();
    Sprite GetUnitProductionSpriteForMenu();
    IBuildAction GetBuildAction(PlacementController placementController);
}
