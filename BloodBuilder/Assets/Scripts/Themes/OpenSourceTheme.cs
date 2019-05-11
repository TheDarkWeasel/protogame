using UnityEngine;
using System.Collections;

public class OpenSourceTheme : Theme
{
    public string getInfantryPrefabPath()
    {
        return "Models/Infantry";
    }

    string Theme.getPlayerBasePrefabPath()
    {
        return "Models/PlayerBase";
    }
}
