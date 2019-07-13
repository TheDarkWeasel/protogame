using UnityEngine;
using System.Collections;

public class OpenSourceTheme : Theme
{
    public string GetBarracksPrefabPath()
    {
        return "Models/Barracks";
    }

    public string GetInfantryActionsMenuSpritePath()
    {
        return "Sprites/ActionsMenu/infantry";
    }

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
