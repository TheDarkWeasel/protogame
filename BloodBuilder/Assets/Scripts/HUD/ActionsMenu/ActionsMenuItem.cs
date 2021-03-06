﻿using UnityEngine;
using UnityEngine.UI;

public class ActionsMenuItem : MonoBehaviour
{
    Button button;
    Image iconImage;

    Sprite clearSprite;

    void Start()
    {
        button = GetComponent<Button>();
        iconImage = transform.Find("Icon").GetComponent<Image>();
        clearSprite = Resources.Load<Sprite>("Sprites/ActionsMenu/transparent");
        button.interactable = false;
    }

    public void AddBuildChoice(BuildChoice buildChoice)
    {
        button.interactable = buildChoice.canCurrentlyBeBuild;
        iconImage.sprite = buildChoice.menuSprite;
        if (buildChoice.buildAction != null)
        {
            button.onClick.AddListener(buildChoice.buildAction.Execute);
        }
        else
        {
            button.onClick.RemoveAllListeners();
        }
    }

    public void ClearItem()
    {
        button.interactable = false;
        iconImage.sprite = clearSprite;
        button.onClick.RemoveAllListeners();
    }
}
