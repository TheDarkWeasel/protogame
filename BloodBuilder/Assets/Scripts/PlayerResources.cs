using System.Runtime.CompilerServices;
using System.Collections.Generic;

public class PlayerResources
{
    private int overallBlood = 0;
    private int selectedBlood = 0;

    private static PlayerResources instance = null;

    private List<IResourceChangeListener> resourceChangeListeners = new List<IResourceChangeListener>();

    public enum PlayerResource
    {
        OVERALL_BLOOD, SELECTED_BLOOD
    }

    public static PlayerResources GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerResources();
        }

        return instance;
    }

    public int GetResourceCount(PlayerResource resourceType)
    {
        switch (resourceType)
        {
            case PlayerResource.OVERALL_BLOOD:
                return overallBlood;
            case PlayerResource.SELECTED_BLOOD:
                return selectedBlood;
            default:
                throw new NotYetImplementedException("Resource type not implemented");
        }
    }

    public void RegisterListener(IResourceChangeListener resourceChangeListener)
    {
        resourceChangeListeners.Add(resourceChangeListener);
    }


    public void UnregisterListener(IResourceChangeListener resourceChangeListener)
    {
        resourceChangeListeners.Remove(resourceChangeListener);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void IncreaseResource(int amount, PlayerResource type)
    {
        int newAmount = 0;
        switch (type)
        {
            case PlayerResource.OVERALL_BLOOD:
                newAmount = overallBlood += amount;
                break;
            case PlayerResource.SELECTED_BLOOD:
                newAmount = selectedBlood += amount;
                break;
            default:
                throw new NotYetImplementedException("Resource type not implemented");
        }

        foreach (IResourceChangeListener listener in resourceChangeListeners)
        {
            listener.OnResourceChange(type, newAmount);
        }
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void DecreaseResource(int amount, PlayerResource type)
    {
        int newAmount = 0;
        switch (type)
        {
            case PlayerResource.OVERALL_BLOOD:
                newAmount = overallBlood -= amount;
                break;
            case PlayerResource.SELECTED_BLOOD:
                newAmount = selectedBlood -= amount;
                break;
            default:
                throw new NotYetImplementedException("Resource type not implemented");
        }

        foreach (IResourceChangeListener listener in resourceChangeListeners)
        {
            listener.OnResourceChange(type, newAmount);
        }
    }
}
