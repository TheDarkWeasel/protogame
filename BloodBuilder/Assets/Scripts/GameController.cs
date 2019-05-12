using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static Theme theme = new OpenSourceTheme();
    private PlacementController placementController;
    private ContextProvider context;

    void Start()
    {
        this.context = new ContextProvider(this);
        placementController = new PlacementController();

        BuildingManager playerBaseManager = new PlayerBaseManager(context);
        placementController.RegisterBuildingManager(playerBaseManager);

        Camera.main.gameObject.AddComponent<PCCameraController>();
    }


    void Update()
    {
        if(placementController != null)
        {
            placementController.Update();
        }

        //Exit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    public static Theme getGlobalTheme()
    {
        return theme;
    }
}
