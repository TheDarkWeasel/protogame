using UnityEngine;
using System.Collections.Generic;

public class BuildChoiceUpdater : IResourceChangeListener
{
    private List<IBuildChoiceChangeListener> buildChoiceChangeListeners;
    private PlayerSelectableObject mainObjectForHUD = null;

    public BuildChoiceUpdater()
    {
        buildChoiceChangeListeners = new List<IBuildChoiceChangeListener>();
    }

    public void RegisterBuildChoiceChangeListener(IBuildChoiceChangeListener buildChoiceChangeListener)
    {
        buildChoiceChangeListeners.Add(buildChoiceChangeListener);
    }

    public void UnregisterBuildChoiceChangeListener(IBuildChoiceChangeListener buildChoiceChangeListener)
    {
        buildChoiceChangeListeners.Remove(buildChoiceChangeListener);
    }

    public bool IsMainObjectForHud(PlayerSelectableObject playerSelectableObject)
    {
        if (playerSelectableObject == null)
        {
            return mainObjectForHUD == null;
        }
        return playerSelectableObject.Equals(mainObjectForHUD);
    }

    public void SetMainObjectForHud(PlayerSelectableObject playerSelectableObject)
    {
        mainObjectForHUD = playerSelectableObject;
        TriggerBuildChoiceUpdate();
    }

    public void OnResourceChange(PlayerResources.PlayerResource resource, int amount)
    {
        TriggerBuildChoiceUpdate();
    }

    private void TriggerBuildChoiceUpdate()
    {
        foreach (IBuildChoiceChangeListener listener in buildChoiceChangeListeners)
        {
            listener.OnBuildChoicesChanged(mainObjectForHUD == null ? new List<BuildChoice>() : mainObjectForHUD.GetBuildChoices());
        }
    }
}
