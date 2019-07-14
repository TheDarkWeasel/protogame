using System.Collections.Generic;
using UnityEngine;

public interface BuildingManager : SelectableObjectContainer, IBuildableByBlood
{
    Building CreateBuilding();
    void ReleaseBuilding(Building building);
    KeyCode GetPlacementHotkey();
    void PlaceBuilding(Building building);
}
