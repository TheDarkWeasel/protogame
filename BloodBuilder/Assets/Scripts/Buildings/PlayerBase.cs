
using System.Collections.Generic;
using UnityEngine;

/**
 * Building class of the PlayerBase, offering specific hotkeys and build choices.
 **/ 
public class PlayerBase : Building
{
    List<IUnitManager> registeredUnitManagers = new List<IUnitManager>();

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
        List<BuildChoice> result = new List<BuildChoice>();
        foreach (IUnitManager manager in registeredUnitManagers)
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
