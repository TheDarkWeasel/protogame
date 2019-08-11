using UnityEngine;

public interface IUnitManager : ISelectableObjectContainer
{
    Unit CreateUnit();
    void ReleaseUnit(Unit unit);
    KeyCode GetBuildHotkey();
    int GetBuildTimeInSeconds();
    void FinishUnitConstruction(Unit unit);

    Sprite getUnitProductionSpriteForMenu();
}
