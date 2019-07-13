using UnityEngine;
using System.Collections.Generic;

public class InfantryManager : UnitManager
{
    private static InfantryManager instance;
    protected KeyCode placementHotkey;
    private Sprite unitProductionSprite;

    protected List<Unit> builtUnits = new List<Unit>();

    private InfantryManager()
    {
        placementHotkey = GameController.GetHotkeys().GetInfantryBuildHotkey();
        unitProductionSprite = Resources.Load<Sprite>(GameController.GetGlobalTheme().GetInfantryActionsMenuSpritePath());
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
        PlayerResources.GetInstance().IncreaseResource(unit.GetBloodAmount(), PlayerResources.PlayerResource.OVERALL_BLOOD);
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
        PlayerResources.GetInstance().DecreaseResource(unit.GetBloodAmount(), PlayerResources.PlayerResource.OVERALL_BLOOD);
        unit.Destroy();
    }

    public Sprite getUnitProductionSpriteForMenu()
    {
        return unitProductionSprite;
    }
}
