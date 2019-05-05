using UnityEngine;
using System.Collections;

public class PlayerBase : Building
{
    public PlayerBase() : base()
    {
        prefabPath = GameController.getGlobalTheme().getPlayerBasePrefabPath();
    }
}
