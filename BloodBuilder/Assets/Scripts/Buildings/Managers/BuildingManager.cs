using UnityEngine;
using System.Collections;

public interface BuildingManager
{
    Building CreateBuilding();
    void ReleaseBuilding(Building building);
    KeyCode GetPlacementHotkey();
}
