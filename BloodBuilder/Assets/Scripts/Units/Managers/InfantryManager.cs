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

    public bool GetPlayerSelectableObjects(List<PlayerSelectableObject> outParam, SelectionState selectionState)
    {
        bool added = false;
        foreach (Unit unit in builtUnits)
        {
            switch (selectionState)
            {
                case SelectionState.SELECTED:
                    if (unit.IsSelected())
                    {
                        outParam.Add(unit);
                    }
                    break;
                case SelectionState.UNSELECTED:
                    if (!unit.IsSelected())
                    {
                        outParam.Add(unit);
                    }
                    break;
                case SelectionState.ALL:
                    outParam.Add(unit);
                    break;
            }
            added = true;
        }

        return added;
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
