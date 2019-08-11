using UnityEngine;
using System.Collections;

public interface IHotkeys
{
    KeyCode GetQuitHotkey();

    KeyCode GetCameraMoveUpHotkey();
    KeyCode GetCameraMoveDownHotkey();
    KeyCode GetCameraMoveLeftHotkey();
    KeyCode GetCameraMoveRightHotkey();

    KeyCode GetBuildPlayerBaseHotkey();
    KeyCode GetBuildBarracksHotkey();

    KeyCode GetInfantryBuildHotkey();
}
