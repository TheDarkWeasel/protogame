
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Building
{
    List<UnitManager> registeredUnitManagers = new List<UnitManager>();

    public PlayerBase(ContextProvider context) : base(context)
    {
        prefabPath = GameController.GetGlobalTheme().GetPlayerBasePrefabPath();
        registeredUnitManagers.Add(InfantryManager.GetInstance());
    }

    public override void OnPlaced()
    {
        base.OnPlaced();
    }

    public override void Update()
    {
        base.Update();
        foreach(UnitManager manager in registeredUnitManagers)
        {
            if(Input.GetKeyDown(manager.GetBuildHotkey())) {
                AddUnitCommand(new BuildUnitCommand(manager, GetUnitCreationPosition(), GetUnitAssemblyPoint()));
            }
        }
    }
}
