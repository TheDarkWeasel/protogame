
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : Building
{
    List<UnitManager> registeredUnitManagers = new List<UnitManager>();

    public PlayerBase(ContextProvider context) : base(context)
    {
        prefabPath = GameController.GetGlobalTheme().GetPlayerBasePrefabPath();
        registeredUnitManagers.Add(context.GetInfantryManager());
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

    public override List<BuildChoice> GetBuildChoices()
    {
        List<BuildChoice> result = new List<BuildChoice>();
        foreach (UnitManager manager in registeredUnitManagers)
        {
            BuildChoice choice = new BuildChoice
            {
                menuSprite = manager.getUnitProductionSpriteForMenu(),
                buildAction = new AddUnitBuildAction(new BuildUnitCommand(manager, GetUnitCreationPosition(), GetUnitAssemblyPoint()), this),
                canCurrentlyBeBuild = true
            };
            result.Add(choice);
        }
        return result;
    }
}
