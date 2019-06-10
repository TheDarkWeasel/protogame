using UnityEngine;
using System.Collections;

public class BuildUnitCommand : UnitCommand
{

    private UnitManager unitManager;

    public BuildUnitCommand(UnitManager unitManager, Vector3 positionForFinishedUnit) : base(positionForFinishedUnit)
    {
        this.unitManager = unitManager;
    }

    protected override IEnumerator CommandFunction()
    {
        Unit unit = unitManager.CreateUnit();
        unit.SetPosition(getPositionForFinishedUnit());
        //TODO we introduce build time later
        yield return null;
        finish();
    }
}
