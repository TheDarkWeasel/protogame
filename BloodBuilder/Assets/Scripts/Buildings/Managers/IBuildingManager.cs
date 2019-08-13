using System.Collections.Generic;
using UnityEngine;

/**
 * API to interact with BuildingManagers. Used by the PlacementController.
 **/
public interface IBuildingManager : ISelectableObjectContainer, IBuildableByBlood
{
    Building CreateBuilding();
    void ReleaseBuilding(Building building);
    KeyCode GetPlacementHotkey();
    void PlaceBuilding(Building building);
}
