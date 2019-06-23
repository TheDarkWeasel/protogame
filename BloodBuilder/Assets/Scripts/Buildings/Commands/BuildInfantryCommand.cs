using UnityEngine;
using System.Collections;

public class BuildUnitCommand : UnitCommand
{

    private UnitManager unitManager;

    public BuildUnitCommand(UnitManager unitManager, Vector3 positionForFinishedUnit, Vector3 assemblyPoint) : base(positionForFinishedUnit, assemblyPoint)
    {
        this.unitManager = unitManager;
    }

    protected override IEnumerator CommandFunction()
    {
        //TODO make HUD updates, when we have one
        yield return new WaitForSeconds(unitManager.GetBuildTimeInSeconds());
        Unit unit = unitManager.CreateUnit();
        unit.SetPosition(getPositionForFinishedUnit());
        unit.MoveToPosition(getAssemblyPoint());
        unitManager.FinishUnitConstruction(unit);
        finish();
    }
}
