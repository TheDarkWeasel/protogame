using UnityEngine;
using System.Collections;

public interface Theme
{
    string GetPlayerBasePrefabPath();
    string GetBarracksPrefabPath();
    string GetInfantryPrefabPath();
    string GetSelectionCirclePrefabPath();

    string GetInfantryActionsMenuSpritePath();
}
