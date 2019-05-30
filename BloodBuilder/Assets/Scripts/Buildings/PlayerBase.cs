using UnityEngine;
using System.Collections;

public class PlayerBase : Building
{
    public PlayerBase(ContextProvider context) : base(context)
    {
        prefabPath = GameController.GetGlobalTheme().GetPlayerBasePrefabPath();
    }

    public override void OnPlaced()
    {
        base.OnPlaced();
        AddUnitCommand(new BuildInfantryCommand(GetUnitCreationPosition()));
    }
}
