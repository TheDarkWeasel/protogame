using UnityEngine;
using System.Collections;

public class PCHotkeys : Hotkeys
{
    public KeyCode GetBuildPlayerBaseHotkey()
    {
        return KeyCode.B;
    }

    public KeyCode GetCameraMoveDownHotkey()
    {
        return KeyCode.S;
    }

    public KeyCode GetCameraMoveLeftHotkey()
    {
        return KeyCode.A;
    }

    public KeyCode GetCameraMoveRightHotkey()
    {
        return KeyCode.D;
    }

    public KeyCode GetCameraMoveUpHotkey()
    {
        return KeyCode.W;
    }

    public KeyCode GetQuitHotkey()
    {
        return KeyCode.Escape;
    }
}
