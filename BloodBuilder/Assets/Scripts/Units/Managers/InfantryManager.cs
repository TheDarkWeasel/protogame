using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InfantryManager : UnitManager
{
    private static InfantryManager instance;
    protected KeyCode placementHotkey;

    protected List<Unit> builtUnits = new List<Unit>();

    private InfantryManager()
    {
        this.placementHotkey = GameController.GetHotkeys().GetInfantryBuildHotkey();
    }

    public static InfantryManager GetInstance()
    {
        if(instance == null)
        {
            instance = new InfantryManager();
        }

        return instance;
    }

    public Unit CreateUnit()
    {
        Infantry infantry = new Infantry();
        infantry.CreatePlacebleModel();
        return infantry;
    }

    public void FinishUnitConstruction(Unit unit)
    {
        builtUnits.Add(unit);
    }

    public KeyCode GetBuildHotkey()
    {
        return placementHotkey;
    }

    public int GetBuildTimeInSeconds()
    {
        return 2;
    }

    public List<PlayerSelectableObject> GetPlayerSelectableObjects()
    {
        List<PlayerSelectableObject> result = new List<PlayerSelectableObject>();
        result.AddRange(builtUnits);
        return result;
    }

    public void ReleaseUnit(Unit unit)
    {
        if (builtUnits.Contains(unit))
        {
            builtUnits.Remove(unit);
        }
        unit.Destroy();
    }
}
