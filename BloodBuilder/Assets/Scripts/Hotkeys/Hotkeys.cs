using UnityEngine;
using System.Collections;

public interface Hotkeys
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
