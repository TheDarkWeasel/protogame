using System.Collections.Generic;
using UnityEngine;

public class ActionsMenuHUD : MonoBehaviour, IBuildChoiceChangeListener
{
    List<ActionsMenuItem> menuItems;

    void Start()
    {
        //prepare and get menu items
        menuItems = new List<ActionsMenuItem>();
        GameObject actionsMenu = GameObject.Find("ActionsMenu");
        foreach (Transform child in actionsMenu.transform)
        {
            child.gameObject.AddComponent<ActionsMenuItem>();
            menuItems.Add(child.gameObject.GetComponent<ActionsMenuItem>());
        }

        Debug.Log("Menuitems size: " + menuItems.Count);
    }

    public void OnBuildChoicesChanged(List<BuildChoice> buildChoices)
    {
        for (int i = 0; i < menuItems.Count; i++)
        {
            if (i < buildChoices.Count)
            {
                menuItems[i].AddBuildChoice(buildChoices[i]);
            }
            else
            {
                menuItems[i].ClearItem();
            }
        }
    }
}
