using UnityEngine;
using System.Collections.Generic;

public class BuildChoiceManager : IResourceChangeListener
{
    private List<IBuildChoiceChangeListener> buildChoiceChangeListeners;
    private PlayerSelectableObject mainObjectForHUD = null;

    public BuildChoiceManager()
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
