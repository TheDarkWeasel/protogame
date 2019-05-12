using UnityEngine;
using System.Collections;

public class InfantryManager : UnitManager
{
    protected KeyCode placementHotkey;

    public InfantryManager()
    {
        this.placementHotkey = KeyCode.I;
    }

    public Unit CreateUnit()
    {
        Infantry infantry = new Infantry();
        infantry.CreatePlacebleModel();
        return infantry;
    }

    public KeyCode GetBuildHotkey()
    {
        return placementHotkey;
    }

    public void ReleaseUnit(Unit unit)
    {
        unit.Destroy();
    }
}
