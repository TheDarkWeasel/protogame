using UnityEngine;
using System.Collections;

public class PlayerBase : Building
{
    public PlayerBase(ContextProvider context) : base(context)
    {
        prefabPath = GameController.getGlobalTheme().getPlayerBasePrefabPath();
    }
}
