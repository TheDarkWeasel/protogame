﻿using UnityEngine;
using UnityEngine.UI;

public class ActionsMenuItem : MonoBehaviour
{
    Button button;
    Image iconImage;

    void Start()
    {
        button = GetComponent<Button>();
        iconImage = transform.Find("Icon").GetComponent<Image>();
    }

    public void AddBuildChoice(BuildChoice buildChoice)
    {
        button.interactable = true;
        iconImage.sprite = buildChoice.menuSprite;
        //TODO onClickAction
    }

    public void ClearItem()
    {
        button.interactable = false;
        iconImage.sprite = null;
    }
}
