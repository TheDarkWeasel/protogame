using UnityEngine;

public class ContextProvider
{

    private MonoBehaviour behaviour;
    private PlayerObjectPool playerObjectPool;
    private BuildChoiceUpdater buildChoiceUpdater;
    private InfantryManager infantryManager;

    public ContextProvider(MonoBehaviour behaviour, PlayerObjectPool playerObjectPool, BuildChoiceUpdater buildChoiceUpdater, InfantryManager infantryManager)
    {
        this.behaviour = behaviour;
        this.playerObjectPool = playerObjectPool;
        this.buildChoiceUpdater = buildChoiceUpdater;
        this.infantryManager = infantryManager;
    }

    public MonoBehaviour GetMonoBehaviour()
    {
        return behaviour;
    }

    public PlayerObjectPool GetPlayerObjectPool()
    {
        return playerObjectPool;
    }

    public BuildChoiceUpdater GetBuildChoiceUpdater()
    {
        return buildChoiceUpdater;
    }

    public InfantryManager GetInfantryManager()
    {
        return infantryManager;
    }
}
