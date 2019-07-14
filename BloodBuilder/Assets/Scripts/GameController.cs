using UnityEngine;

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

        BuildChoiceUpdater buildChoiceManager = new BuildChoiceUpdater();
        UnitBuildChoiceProvider unitBuildChoiceProvider = new UnitBuildChoiceProvider();

        InfantryManager infantryManager = new InfantryManager(unitBuildChoiceProvider);

        this.context = new ContextProvider(this, playerObjectPool, buildChoiceManager, infantryManager);
        placementController = new PlacementController(context);
        selectionController = new SelectionController(context);

        BuildingManager playerBaseManager = new PlayerBaseManager(context);
        placementController.RegisterBuildingManager(playerBaseManager);
        playerObjectPool.RegisterSelectableObjectContainer(playerBaseManager);
        unitBuildChoiceProvider.RegisterBloodBuildable(playerBaseManager);

        BuildingManager barracksManager = new BarracksManager(context);
        placementController.RegisterBuildingManager(barracksManager);
        playerObjectPool.RegisterSelectableObjectContainer(barracksManager);
        unitBuildChoiceProvider.RegisterBloodBuildable(barracksManager);

        playerObjectPool.RegisterSelectableObjectContainer(infantryManager);

        Camera.main.gameObject.AddComponent<PCCameraController>();

        GameObject hud = SetupHUD();

        PlayerResources.GetInstance().RegisterListener(hud.GetComponent<BloodCounterHUD>());
        PlayerResources.GetInstance().RegisterListener(buildChoiceManager);

        buildChoiceManager.RegisterBuildChoiceChangeListener(hud.GetComponent<ActionsMenuHUD>());
    }

    private static GameObject SetupHUD()
    {
        GameObject hud = GameObject.Find("HUD");
        hud.AddComponent<BloodCounterHUD>();
        hud.AddComponent<ActionsMenuHUD>();
        return hud;
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
            selectionController.SetActive(!placementControllerActive);
            selectionController.Update();
        }

        if (playerObjectPool != null)
        {
            //TODO I don't know, if this will be the final way of triggering the unit building process.
            //It does not feel right and looks inperfomant. Very likely to be changed. But for now it should work.
            foreach (PlayerSelectableObject playerSelectableObject in playerObjectPool.GetSelectedObjects())
            {
                playerSelectableObject.Update();
            }
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
