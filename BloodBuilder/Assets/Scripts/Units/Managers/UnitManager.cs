using UnityEngine;
using System.Collections;

public interface UnitManager
{
    Unit CreateUnit();
    void ReleaseUnit(Unit unit);
    KeyCode GetBuildHotkey();
    int GetBuildTimeInSeconds();
}
