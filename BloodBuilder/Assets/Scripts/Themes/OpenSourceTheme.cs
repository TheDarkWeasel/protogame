using UnityEngine;
using System.Collections;

public class OpenSourceTheme : Theme
{
    public string getInfantryPrefabPath()
    {
        return "Models/Infantry";
    }

    public string getSelectionCirclePrefabPath()
    {
        return "Models/SelectionCirclePrefab";
    }

    string Theme.getPlayerBasePrefabPath()
    {
        return "Models/PlayerBase";
    }
}
