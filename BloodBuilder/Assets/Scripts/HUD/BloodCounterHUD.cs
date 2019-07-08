using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BloodCounterHUD : MonoBehaviour, IResourceChangeListener
{

    Text overallBloodCounter;
    Text selectedBloodCounter;

    // Use this for initialization
    void Start()
    {
        overallBloodCounter = GameObject.Find("OverallBloodCounter").GetComponent<Text>();
        selectedBloodCounter = GameObject.Find("SelectedBloodCounter").GetComponent<Text>();
    }

    void IResourceChangeListener.OnResourceChange(PlayerResources.PlayerResource resource, int amount)
    {
        switch(resource)
        {
            case PlayerResources.PlayerResource.OVERALL_BLOOD:
                overallBloodCounter.text = "Overall Blood: " + amount;
                break;
            case PlayerResources.PlayerResource.SELECTED_BLOOD:
                selectedBloodCounter.text = "Selected Blood: " + amount;
                break;
            default:
                throw new NotYetImplementedException("Resource type not implemented");
        }
    }
}
