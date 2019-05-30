using UnityEngine;
using System.Collections;

public class OpenSourceTheme : Theme
{
    public string GetInfantryPrefabPath()
    {
        return "Models/Infantry";
    }

    public string GetSelectionCirclePrefabPath()
    {
        return "Models/SelectionCirclePrefab";
    }

    string Theme.GetPlayerBasePrefabPath()
    {
        return "Models/PlayerBase";
    }
}
