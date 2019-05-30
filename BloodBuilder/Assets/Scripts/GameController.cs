using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{

    private static Theme theme = new OpenSourceTheme();
    private static Hotkeys hotkeys = new PCHotkeys();
    private PlacementController placementController;
    private SelectionController selectionController;
    private PlayerObjectPool playerObjectPool;

    private ContextProvider context;

    void Start()
    {
        playerObjectPool = new PlayerObjectPool();
        this.context = new ContextProvider(this, playerObjectPool);
        placementController = new PlacementController(context);
        selectionController = new SelectionController(context);

        BuildingManager playerBaseManager = new PlayerBaseManager(context);
        placementController.RegisterBuildingManager(playerBaseManager);
        playerObjectPool.RegisterSelectableObjectContainer(playerBaseManager);

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
        if (Input.GetKey(GetHotkeys().GetQuitHotkey()))
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

    public static Theme GetGlobalTheme()
    {
        return theme;
    }

    public static Hotkeys GetHotkeys()
    {
        return hotkeys;
    }
}
