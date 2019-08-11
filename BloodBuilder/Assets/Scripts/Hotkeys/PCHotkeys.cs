using UnityEngine;
using System.Collections;

public class PCHotkeys : IHotkeys
{
    public KeyCode GetBuildBarracksHotkey()
    {
        return KeyCode.N;
    }

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

    public KeyCode GetInfantryBuildHotkey()
    {
        return KeyCode.I;
    }

    public KeyCode GetQuitHotkey()
    {
        return KeyCode.Escape;
    }
}
