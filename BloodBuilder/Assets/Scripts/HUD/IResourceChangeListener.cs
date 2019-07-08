using UnityEngine;
using System.Collections;

public interface IResourceChangeListener
{
    void OnResourceChange(PlayerResources.PlayerResource resource, int amount);
}
