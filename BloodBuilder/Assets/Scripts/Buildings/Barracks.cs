
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    List<UnitManager> registeredUnitManagers = new List<UnitManager>();

    public Barracks(ContextProvider context) : base(context)
    {
        prefabPath = GameController.GetGlobalTheme().GetBarracksPrefabPath();
    }

    public override void OnPlaced()
    {
        base.OnPlaced();
    }

    public override void Update()
    {
        base.Update();
        foreach (UnitManager manager in registeredUnitManagers)
        {
            if (Input.GetKeyDown(manager.GetBuildHotkey()))
            {
                AddUnitCommand(new BuildUnitCommand(manager, GetUnitCreationPosition(), GetUnitAssemblyPoint()));
            }
        }
    }
}
