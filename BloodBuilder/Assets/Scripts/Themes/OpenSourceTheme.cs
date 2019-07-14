using UnityEngine;
using System.Collections;

public class OpenSourceTheme : Theme
{
    public string GetBarracksActionsMenuSpritePath()
    {
        return "Sprites/ActionsMenu/barracks";
    }

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

    public string GetPlayerBaseActionsMenuSpritePath()
    {
        return "Sprites/ActionsMenu/playerbase";
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
