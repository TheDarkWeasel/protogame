﻿using UnityEngine;
using UnityEngine.UI;

public class BloodCounterHUD : MonoBehaviour, IResourceChangeListener
{

    Text overallBloodCounter;
    Text selectedBloodCounter;

    Animation overallBloodBounce;

    void Start()
    {
        overallBloodCounter = GameObject.Find("OverallBloodCounter").GetComponent<Text>();
        selectedBloodCounter = GameObject.Find("SelectedBloodCounter").GetComponent<Text>();
        overallBloodBounce = overallBloodCounter.GetComponent<Animation>();
    }

    void IResourceChangeListener.OnResourceChange(PlayerResources.PlayerResource resource, int amount)
    {
        switch(resource)
        {
            case PlayerResources.PlayerResource.OVERALL_BLOOD:
                overallBloodCounter.text = "Overall Blood: " + amount;
                overallBloodBounce.Play("UIBounce");
                break;
            case PlayerResources.PlayerResource.SELECTED_BLOOD:
                selectedBloodCounter.text = "Selected Blood: " + amount;
                break;
            default:
                throw new NotYetImplementedException("Resource type not implemented");
        }
    }
}
