using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private static Theme theme = new OpenSourceTheme();
    private PlacementController placementController;

    void Start()
    {
        placementController = new PlacementController();

        BuildingManager playerBaseManager = new PlayerBaseManager();
        placementController.RegisterBuildingManager(playerBaseManager);
    }


    void Update()
    {
        if(placementController != null)
        {
            placementController.Update();
        }
    }

    public static Theme getGlobalTheme()
    {
        return theme;
    }
}
