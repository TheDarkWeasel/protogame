using UnityEngine;
using System.Collections;

public interface ITheme
{
    string GetPlayerBasePrefabPath();
    string GetBarracksPrefabPath();
    string GetInfantryPrefabPath();
    string GetSelectionCirclePrefabPath();

    string GetInfantryActionsMenuSpritePath();
    string GetPlayerBaseActionsMenuSpritePath();
    string GetBarracksActionsMenuSpritePath();
}
