using UnityEngine;
using System.Collections;
using System;

public class Infantry : Unit
{
    public Infantry() : base()
    {
        prefabPath = GameController.getGlobalTheme().getInfantryPrefabPath();
    }
}
