using UnityEngine;
using System.Collections;

public class ToonTheme : Theme
{
    public string GetInfantryPrefabPath()
    {
        return "ToonyTinyPeople/TT_RTS/TT_RTS_Standard/prefabs/TT_Light_Infantry";
    }

    public string GetPlayerBasePrefabPath()
    {
        return "ToonyTinyPeople/TT_RTS/TT_RTS_Standard/models/buildings/Castle";
    }

    public string GetSelectionCirclePrefabPath()
    {
        return "Models/SelectionCirclePrefab";
    }
}
