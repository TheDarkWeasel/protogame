using UnityEngine;
using System.Collections;

public class ContextProvider
{

    private MonoBehaviour behaviour;
    private PlayerObjectPool playerObjectPool;

    public ContextProvider(MonoBehaviour behaviour, PlayerObjectPool playerObjectPool)
    {
        this.behaviour = behaviour;
        this.playerObjectPool = playerObjectPool;
    }

    public MonoBehaviour GetMonoBehaviour()
    {
        return behaviour;
    }

    public PlayerObjectPool GetPlayerObjectPool()
    {
        return playerObjectPool;
    }
}
