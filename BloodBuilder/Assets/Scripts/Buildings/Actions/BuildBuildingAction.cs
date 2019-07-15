public class BuildBuildingAction : IBuildAction
{
    private PlacementController placementController;
    private BuildingManager buildingManager;

    public BuildBuildingAction(PlacementController placementController, BuildingManager buildingManager)
    {
        this.placementController = placementController;
        this.buildingManager = buildingManager;
    }

    public void Execute()
    {
        placementController.BuildBuilding(buildingManager);
    }
}
