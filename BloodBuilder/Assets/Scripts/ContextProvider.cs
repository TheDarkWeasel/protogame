using UnityEngine;
using System.Collections;

public class ContextProvider
{

    private MonoBehaviour behaviour;

    public ContextProvider(MonoBehaviour behaviour)
    {
        this.behaviour = behaviour;
    }

    public MonoBehaviour GetMonoBehaviour()
    {
        return behaviour;
    }
}
