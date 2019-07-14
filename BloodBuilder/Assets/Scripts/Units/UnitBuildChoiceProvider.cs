using System.Collections.Generic;

public class UnitBuildChoiceProvider
{
    private List<IBuildableByBlood> bloodBuildables;

    public UnitBuildChoiceProvider()
    {
        bloodBuildables = new List<IBuildableByBlood>();
    }

    public void RegisterBloodBuildable(IBuildableByBlood buildableByBlood)
    {
        bloodBuildables.Add(buildableByBlood);
    }

    public void UnregisterBloodBuildable(IBuildableByBlood buildableByBlood)
    {
        bloodBuildables.Remove(buildableByBlood);
    }

    public List<BuildChoice> GetBuildChoicesForSelectedBlood()
    {
        List<BuildChoice> result = new List<BuildChoice>();
        foreach (IBuildableByBlood buildableByBlood in bloodBuildables)
        {
            BuildChoice choice = new BuildChoice
            {
                menuSprite = buildableByBlood.getUnitProductionSpriteForMenu(),
                //TODO buildAction
                canCurrentlyBeBuild = PlayerResources.GetInstance().GetResourceCount(PlayerResources.PlayerResource.SELECTED_BLOOD) >= buildableByBlood.GetBuildCosts()
            };
            result.Add(choice);
        }
        return result;
    }
}
