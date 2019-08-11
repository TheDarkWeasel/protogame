public class BuildBuildingAction : IBuildAction
{
    private PlacementController placementController;
    private IBuildingManager buildingManager;

    public BuildBuildingAction(PlacementController placementController, IBuildingManager buildingManager)
    {
        this.placementController = placementController;
        this.buildingManager = buildingManager;
    }

    public void Execute()
    {
        placementController.BuildBuilding(buildingManager);
    }
}
