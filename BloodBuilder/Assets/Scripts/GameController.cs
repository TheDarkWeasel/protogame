using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static Theme theme = new OpenSourceTheme();
    private PlacementController placementController;
    private SelectionController selectionController;
    private ContextProvider context;

    void Start()
    {
        this.context = new ContextProvider(this);
        placementController = new PlacementController();
        selectionController = new SelectionController();

        BuildingManager playerBaseManager = new PlayerBaseManager(context);
        placementController.RegisterBuildingManager(playerBaseManager);

        Camera.main.gameObject.AddComponent<PCCameraController>();
    }


    void Update()
    {
        bool placementControllerActive = false;

        if (placementController != null)
        {
            placementController.Update();
            placementControllerActive = placementController.IsActive();
        }

        if (selectionController != null)
        {
            //Selection is only allowed, if the player is not building something
            selectionController.IsActive = !placementControllerActive;
            selectionController.Update();
        }

        //Exit
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    void OnGUI()
    {
        if (selectionController != null)
        {
            selectionController.OnGUI();
        }
    }

    public static Theme getGlobalTheme()
    {
        return theme;
    }
}
