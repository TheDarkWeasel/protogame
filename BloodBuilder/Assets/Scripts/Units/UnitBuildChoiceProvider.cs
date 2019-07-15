using System.Collections.Generic;

public class UnitBuildChoiceProvider
{
    private List<IBuildableByBlood> bloodBuildables;
    private PlacementController placementController;

    public UnitBuildChoiceProvider(PlacementController placementController)
    {
        bloodBuildables = new List<IBuildableByBlood>();
        this.placementController = placementController;
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
                menuSprite = buildableByBlood.GetUnitProductionSpriteForMenu(),
                buildAction = buildableByBlood.GetBuildAction(placementController),
                canCurrentlyBeBuild = PlayerResources.GetInstance().GetResourceCount(PlayerResources.PlayerResource.SELECTED_BLOOD) >= buildableByBlood.GetBuildCosts()
            };
            result.Add(choice);
        }
        return result;
    }
}
