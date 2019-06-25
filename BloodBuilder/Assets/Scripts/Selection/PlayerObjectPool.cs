using System.Collections.Generic;

public class PlayerObjectPool
{
    private List<SelectableObjectContainer> selectableObjectcontainers = new List<SelectableObjectContainer>();

    private List<PlayerSelectableObject> playerSelectableObjects = new List<PlayerSelectableObject>();
    private List<PlayerSelectableObject> selectedObjects = new List<PlayerSelectableObject>();

    public void RegisterSelectableObjectContainer(SelectableObjectContainer obj)
    {
        selectableObjectcontainers.Add(obj);
    }

    public List<PlayerSelectableObject> GetPlayerSelectableObjects()
    {
        playerSelectableObjects.Clear();
        foreach (SelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            selectableObjectContainer.GetPlayerSelectableObjects(playerSelectableObjects, SelectionState.ALL);
        }
        return playerSelectableObjects;
    }

    public List<PlayerSelectableObject> GetSelectedObjects()
    {
        selectedObjects.Clear();
        foreach (SelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            selectableObjectContainer.GetPlayerSelectableObjects(selectedObjects, SelectionState.SELECTED);
        }
        return selectedObjects;
    }
}
