using UnityEngine;
using System.Collections;

public class BuildInfantryCommand : UnitCommand
{

    private InfantryManager infantryManager = new InfantryManager();

    public BuildInfantryCommand(Vector3 positionForFinishedUnit) : base(positionForFinishedUnit)
    {
    }

    protected override IEnumerator CommandFunction()
    {
        Unit unit = infantryManager.CreateUnit();
        unit.SetPosition(getPositionForFinishedUnit());
        finish();
        //TODO we introduce build time later
        yield return null;
    }
}
