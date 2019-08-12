using UnityEngine;
using System.Collections;

/**
 * Specific UnitCommand for building infantry units.
 **/
public class BuildUnitCommand : UnitCommand
{

    private IUnitManager unitManager;

    public BuildUnitCommand(IUnitManager unitManager, Vector3 positionForFinishedUnit, Vector3 assemblyPoint) : base(positionForFinishedUnit, assemblyPoint)
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
