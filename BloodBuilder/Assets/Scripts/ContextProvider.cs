using UnityEngine;

public class ContextProvider
{

    private MonoBehaviour behaviour;
    private PlayerObjectPool playerObjectPool;
    private BuildChoiceManager buildChoiceManager;

    public ContextProvider(MonoBehaviour behaviour, PlayerObjectPool playerObjectPool, BuildChoiceManager buildChoiceManager)
    {
        this.behaviour = behaviour;
        this.playerObjectPool = playerObjectPool;
        this.buildChoiceManager = buildChoiceManager;
    }

    public MonoBehaviour GetMonoBehaviour()
    {
        return behaviour;
    }

    public PlayerObjectPool GetPlayerObjectPool()
    {
        return playerObjectPool;
    }

    public BuildChoiceManager GetBuildChoiceManager()
    {
        return buildChoiceManager;
    }
}
