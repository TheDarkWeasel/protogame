
using System.Collections.Generic;
using UnityEngine;

public class Barracks : Building
{
    List<IUnitManager> registeredUnitManagers = new List<IUnitManager>();

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
        foreach (IUnitManager manager in registeredUnitManagers)
        {
            if (Input.GetKeyDown(manager.GetBuildHotkey()))
            {
                AddUnitCommand(new BuildUnitCommand(manager, GetUnitCreationPosition(), GetUnitAssemblyPoint()));
            }
        }
    }

    public override List<BuildChoice> GetBuildChoices()
    {
        return new List<BuildChoice>();
    }
}
