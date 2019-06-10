using System.Collections.Generic;

public class PlayerObjectPool
{
    private List<SelectableObjectContainer> selectableObjectcontainers = new List<SelectableObjectContainer>();

    public void RegisterSelectableObjectContainer(SelectableObjectContainer obj)
    {
        selectableObjectcontainers.Add(obj);
    }

    public List<PlayerSelectableObject> GetPlayerSelectableObjects()
    {
        List<PlayerSelectableObject> result = new List<PlayerSelectableObject>();
        foreach (SelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            result.AddRange(selectableObjectContainer.GetPlayerSelectableObjects());
        }
        return result;
    }

    public List<PlayerSelectableObject> GetSelectedObjects()
    {
        List<PlayerSelectableObject> result = new List<PlayerSelectableObject>();
        foreach (SelectableObjectContainer selectableObjectContainer in selectableObjectcontainers)
        {
            foreach(PlayerSelectableObject selectableObject in selectableObjectContainer.GetPlayerSelectableObjects())
            {
                if(selectableObject.IsSelected())
                {
                    result.Add(selectableObject);
                }
            }
        }
        return result;
    }
}
