using UnityEngine;
using System.Collections.Generic;

public class InfantryManager : UnitManager
{
    protected KeyCode placementHotkey;
    private Sprite unitProductionSprite;
    private UnitBuildChoiceProvider unitBuildChoiceProvider;

    private ObjectPool<Unit> infantryPool;

    protected List<Unit> builtUnits = new List<Unit>();

    public InfantryManager(UnitBuildChoiceProvider unitBuildChoiceProvider)
    {
        placementHotkey = GameController.GetHotkeys().GetInfantryBuildHotkey();
        unitProductionSprite = Resources.Load<Sprite>(GameController.GetGlobalTheme().GetInfantryActionsMenuSpritePath());
        this.unitBuildChoiceProvider = unitBuildChoiceProvider;
        this.infantryPool = new ObjectPool<Unit>(() => InternalCreateUnit(), (i) => InternalActivateUnit(i), (i) => InternalDeactivateUnit(i));
    }

    private Unit InternalCreateUnit()
    {
        Infantry infantry = new Infantry(this, unitBuildChoiceProvider);
        infantry.CreatePlacebleModel();
        return infantry;
    }

    private void InternalDeactivateUnit(Unit infantry)
    {
        infantry.GetGameObject().SetActive(false);
        infantry.DestroySelectionCircle();
        infantry.ParentManager = null;
        infantry.UnitBuildChoiceProvider = null;
    }

    private void InternalActivateUnit(Unit infantry)
    {
        infantry.ParentManager = this;
        infantry.UnitBuildChoiceProvider = unitBuildChoiceProvider;
        infantry.GetGameObject().SetActive(true);
    }

    public Unit CreateUnit()
    {
        return infantryPool.GetObject();
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
        infantryPool.PutObject(unit);
    }

    public Sprite getUnitProductionSpriteForMenu()
    {
        return unitProductionSprite;
    }
}
