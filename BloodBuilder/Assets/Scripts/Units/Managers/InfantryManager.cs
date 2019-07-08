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
        Infantry infantry = new Infantry(this);
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
            bool addUnit = false;
            addUnit = ShallUnitBeAddedToSelectableObjectList(selectionState, unit, addUnit);
            if (addUnit)
            {
                outParam.Add(unit);
                added = true;
            }
        }

        return added;
    }

    public bool GetSacrificableSelectableObjects(List<SacrificableSelectableObject> outParam, SelectionState selectionState)
    {
        bool added = false;
        foreach (Unit unit in builtUnits)
        {
            bool addUnit = false;
            addUnit = ShallUnitBeAddedToSelectableObjectList(selectionState, unit, addUnit);
            if (addUnit)
            {
                outParam.Add(unit);
                added = true;
            }
        }

        return added;
    }

    private static bool ShallUnitBeAddedToSelectableObjectList(SelectionState selectionState, Unit unit, bool addUnit)
    {
        switch (selectionState)
        {
            case SelectionState.SELECTED:
                if (unit.IsSelected())
                {
                    addUnit = true;
                }
                break;
            case SelectionState.UNSELECTED:
                if (!unit.IsSelected())
                {
                    addUnit = true;
                }
                break;
            case SelectionState.ALL:
                addUnit = true;
                break;
        }

        return addUnit;
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
