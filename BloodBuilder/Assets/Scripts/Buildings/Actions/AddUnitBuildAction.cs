/**
 * Action for adding a UnitCommand to the build queue of the parent building.
 **/ 
public class AddUnitBuildAction : IBuildAction
{
    private UnitCommand command;
    private Building parent;

    public AddUnitBuildAction(UnitCommand command, Building parent)
    {
        this.command = command;
        this.parent = parent;
    }

    public void Execute()
    {
        parent.AddUnitCommand(command);
    }
}
