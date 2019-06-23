using UnityEngine;
using System.Collections.Generic;
using System.Linq;

/**
  * Contains parts from: https://hyunkell.com/blog/rts-style-unit-selection-in-unity-5/ written by Jeff Zimmer
  **/
public class SelectionController
{

    private bool isActive = false;
    private bool isSelecting = false;
    private Vector3 mousePosition1;
    private ContextProvider context;
    private Camera mainCamera;

    public GameObject selectionCirclePrefab;

    public SelectionController(ContextProvider context)
    {
        this.context = context;
        selectionCirclePrefab = Resources.Load<GameObject>(GameController.GetGlobalTheme().GetSelectionCirclePrefabPath());
        mainCamera = Camera.main;
    }

    public void Update()
    {
        if (isActive)
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
            }
            // If we let go of the left mouse button, end selection
            if (Input.GetMouseButtonUp(0))
            {
                foreach (var selectableObject in context.GetPlayerObjectPool().GetPlayerSelectableObjects())
                {
                    PlayerSelectableObject current = selectableObject;
                    if (IsWithinSelectionBounds(current.GetGameObject()))
                    {
                        selectableObject.Select(true);
                    } else
                    {
                        selectableObject.Select(false);
                    }
                }
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

    private void Select(PlayerSelectableObject selectableObject)
    {
        if (selectableObject.GetSelectionCircle() == null)
        {
            selectableObject.SetSelectionCircle(Object.Instantiate(selectionCirclePrefab));
            selectableObject.GetSelectionCircle().transform.SetParent(selectableObject.GetGameObject().transform, false);
            selectableObject.GetSelectionCircle().transform.eulerAngles = new Vector3(90, 0, 0);
            selectableObject.GetSelectionCircle().GetComponent<Projector>().orthographicSize = selectableObject.GetOrthographicSizeForSelectionCircle();
        }
    }

    private void Deselect(PlayerSelectableObject selectableObject)
    {
        if (selectableObject.GetSelectionCircle() != null)
        {
            Object.Destroy(selectableObject.GetSelectionCircle().gameObject);
            selectableObject.SetSelectionCircle(null);
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        Ray ray = mainCamera.ScreenPointToRay(mousePosition1);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            if (gameObject.GetComponent<Collider>().bounds.Contains(hitInfo.point))
                return true;
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
