using System.Collections.Generic;

public class PlayerObjectPool
{
    private List<PlayerSelectableObject> selectableObjects = new List<PlayerSelectableObject>();

    public void AddSelectableObject(PlayerSelectableObject obj)
    {
        selectableObjects.Add(obj);
    }

    public List<PlayerSelectableObject> GetPlayerSelectableObjects()
    {
        return new List<PlayerSelectableObject>(selectableObjects);
    }
}
