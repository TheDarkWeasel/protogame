using UnityEngine;

public interface UnitManager : ISelectableObjectContainer
{
    Unit CreateUnit();
    void ReleaseUnit(Unit unit);
    KeyCode GetBuildHotkey();
    int GetBuildTimeInSeconds();
    void FinishUnitConstruction(Unit unit);

    Sprite getUnitProductionSpriteForMenu();
}
