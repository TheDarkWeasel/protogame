﻿using UnityEngine;
using System.Collections;
using System;

public class Infantry : Unit
{
    public Infantry(InfantryManager infantryManager, UnitBuildChoiceProvider unitBuildChoiceProvider) : base(infantryManager, unitBuildChoiceProvider)
    {
        prefabPath = GameController.GetGlobalTheme().GetInfantryPrefabPath();
    }

    public override int GetBloodAmount()
    {
        return 1;
    }
}
