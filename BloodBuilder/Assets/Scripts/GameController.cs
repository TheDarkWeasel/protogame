using UnityEngine;

public class GameController : MonoBehaviour
{

    private static ITheme theme = new OpenSourceTheme();
    private static IHotkeys hotkeys = new PCHotkeys();
    private PlacementController placementController;
    private SelectionController selectionController;
    private PlayerObjectPool playerObjectPool;

    void Start()
    {
        ContextProvider context;

        playerObjectPool = new PlayerObjectPool();

        BuildChoiceUpdater buildChoiceUpdater = new BuildChoiceUpdater();

        placementController = new PlacementController(playerObjectPool, buildChoiceUpdater);

        UnitBuildChoiceProvider unitBuildChoiceProvider = new UnitBuildChoiceProvider(placementController);
        InfantryManager infantryManager = new InfantryManager(unitBuildChoiceProvider);

        context = new ContextProvider(this, playerObjectPool, buildChoiceUpdater, infantryManager);

        selectionController = new SelectionController(context);

        IBuildingManager playerBaseManager = new PlayerBaseManager(context);
        placementController.RegisterBuildingManager(playerBaseManager);
        playerObjectPool.RegisterSelectableObjectContainer(playerBaseManager);
        unitBuildChoiceProvider.RegisterBloodBuildable(playerBaseManager);

        IBuildingManager barracksManager = new BarracksManager(context);
        placementController.RegisterBuildingManager(barracksManager);
        playerObjectPool.RegisterSelectableObjectContainer(barracksManager);
        unitBuildChoiceProvider.RegisterBloodBuildable(barracksManager);

        playerObjectPool.RegisterSelectableObjectContainer(infantryManager);

        Camera.main.gameObject.AddComponent<PCCameraController>();

        GameObject hud = SetupHUD();

        PlayerResources.GetInstance().RegisterListener(hud.GetComponent<BloodCounterHUD>());
        PlayerResources.GetInstance().RegisterListener(buildChoiceUpdater);

        buildChoiceUpdater.RegisterBuildChoiceChangeListener(hud.GetComponent<ActionsMenuHUD>());
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
            selectionController.Update();
            selectionController.SetActive(!placementControllerActive);
        }

        if (playerObjectPool != null)
        {
            //TODO I don't know, if this will be the final way of triggering the unit building process.
            //It does not feel right and looks inperfomant. Very likely to be changed. But for now it should work.
            foreach (IPlayerSelectableObject playerSelectableObject in playerObjectPool.GetSelectedObjects())
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

    public static ITheme GetGlobalTheme()
    {
        return theme;
    }

    public static IHotkeys GetHotkeys()
    {
        return hotkeys;
    }
}
