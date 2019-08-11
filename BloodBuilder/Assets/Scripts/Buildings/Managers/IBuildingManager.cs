using System.Collections.Generic;
using UnityEngine;

public interface IBuildingManager : ISelectableObjectContainer, IBuildableByBlood
{
    Building CreateBuilding();
    void ReleaseBuilding(Building building);
    KeyCode GetPlacementHotkey();
    void PlaceBuilding(Building building);
}
