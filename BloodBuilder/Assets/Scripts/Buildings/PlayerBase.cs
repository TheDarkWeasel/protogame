using UnityEngine;
using System.Collections;

public class PlayerBase : Building
{
    public PlayerBase(ContextProvider context) : base(context)
    {
        prefabPath = GameController.getGlobalTheme().getPlayerBasePrefabPath();
    }

    public override void OnPlaced()
    {
        base.OnPlaced();
        AddUnitCommand(new BuildInfantryCommand(GetUnitCreationPosition()));
    }
}
