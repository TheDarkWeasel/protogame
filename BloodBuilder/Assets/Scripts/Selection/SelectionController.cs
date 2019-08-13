using UnityEngine;
using UnityEngine.EventSystems;

/**
  * Controller, which is responsible for selecting units and buildings.
  * 
  * Contains parts from: https://hyunkell.com/blog/rts-style-unit-selection-in-unity-5/ written by Jeff Zimmer
  **/
public class SelectionController
{

    private bool isActive = false;
    private bool isSelecting = false;
    private Vector3 mousePosition1;
    private ContextProvider context;
    private Camera mainCamera;

    private RaycastHit hitInfo;

    private GameObject selectionCirclePrefab;

    public SelectionController(ContextProvider context)
    {
        this.context = context;
        selectionCirclePrefab = Resources.Load<GameObject>(GameController.GetGlobalTheme().GetSelectionCirclePrefabPath());
        mainCamera = Camera.main;
    }

    public void Update()
    {
        if (isActive && !EventSystem.current.IsPointerOverGameObject())
        {
            // If we press the left mouse button, begin selection and remember the location of the mouse
            if (Input.GetMouseButtonDown(0))
            {
                isSelecting = true;
                mousePosition1 = Input.mousePosition;

                foreach (var selectableObject in context.GetPlayerObjectPool().GetPlayerSelectableObjects())
                {
                    Deselect(selectableObject);
                }

                context.GetBuildChoiceUpdater().SetMainObjectForHud(null);
            }
            // If we let go of the left mouse button, end selection
            if (Input.GetMouseButtonUp(0))
            {
                IPlayerSelectableObject mainObjectForHUD = null;

                foreach (var selectableObject in context.GetPlayerObjectPool().GetPlayerSelectableObjects())
                {
                    if (IsWithinSelectionBounds(selectableObject.GetGameObject()))
                    {
                        selectableObject.Select(true);
                        if (mainObjectForHUD == null || selectableObject.GetSelectionPriority() < mainObjectForHUD.GetSelectionPriority())
                        {
                            mainObjectForHUD = selectableObject;
                        }
                    }
                    else
                    {
                        selectableObject.Select(false);
                    }
                }

                context.GetBuildChoiceUpdater().SetMainObjectForHud(mainObjectForHUD);

                isSelecting = false;
            }

            // Highlight all objects within the selection box
            if (isSelecting)
            {
                foreach (var selectableObject in context.GetPlayerObjectPool().GetPlayerSelectableObjects())
                {
                    if (IsWithinSelectionBounds(selectableObject.GetGameObject()))
                    {
                        Select(selectableObject);
                    }
                    else
                    {
                        Deselect(selectableObject);
                    }
                }
            }
        }
    }

    private void Select(IPlayerSelectableObject selectableObject)
    {
        selectableObject.CreateSelectionCircle(selectionCirclePrefab);
    }

    private void Deselect(IPlayerSelectableObject selectableObject)
    {
        selectableObject.DestroySelectionCircle();
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition1);

        if (Physics.Raycast(ray, out hitInfo))
        {
            if (gameObject.GetComponent<Collider>().bounds.Contains(hitInfo.point))
            {
                return true;
            }
        }

        var camera = mainCamera;
        var viewportBounds = Utils.GetViewportBounds(camera, mousePosition1, Input.mousePosition);
        return viewportBounds.Contains(camera.WorldToViewportPoint(gameObject.transform.position));
    }

    public void OnGUI()
    {
        if (isActive && isSelecting)
        {
            // Create a rect from both mouse positions
            var rect = Utils.GetScreenRect(mousePosition1, Input.mousePosition);
            Utils.DrawScreenRect(rect, new Color(0.8f, 0.8f, 0.95f, 0.25f));
            Utils.DrawScreenRectBorder(rect, 2, new Color(0.8f, 0.8f, 0.95f));
        }
    }

    public bool IsActive()
    {
        return isActive;
    }

    public void SetActive(bool active)
    {
        isActive = active;
    }
}
