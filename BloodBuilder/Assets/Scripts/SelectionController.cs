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

    public GameObject selectionCirclePrefab;

    public SelectionController(ContextProvider context)
    {
        this.context = context;
        selectionCirclePrefab = Resources.Load<GameObject>(GameController.getGlobalTheme().getSelectionCirclePrefabPath());
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
                    if (selectableObject.GetSelectionCircle() != null)
                    {
                        Object.Destroy(selectableObject.GetSelectionCircle().gameObject);
                        selectableObject.SetSelectionCircle(null);
                    }
                }
            }
            // If we let go of the left mouse button, end selection
            if (Input.GetMouseButtonUp(0))
            {
                var selectedObjects = new List<PlayerSelectableObject>();
                foreach (var selectableObject in context.GetPlayerObjectPool().GetPlayerSelectableObjects())
                {
                    PlayerSelectableObject current = selectableObject;
                    if (IsWithinSelectionBounds(current.GetGameObject()))
                    {
                        selectableObject.Select(true);
                        selectedObjects.Add(current);
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
                        if (selectableObject.GetSelectionCircle() == null)
                        {
                            selectableObject.SetSelectionCircle(Object.Instantiate(selectionCirclePrefab));
                            selectableObject.GetSelectionCircle().transform.SetParent(selectableObject.GetGameObject().transform, false);
                            selectableObject.GetSelectionCircle().transform.eulerAngles = new Vector3(90, 0, 0);
                        }
                    }
                    else
                    {
                        if (selectableObject.GetSelectionCircle() != null)
                        {
                            Object.Destroy(selectableObject.GetSelectionCircle().gameObject);
                            selectableObject.SetSelectionCircle(null);                        }
                    }
                }
            }
        }
    }

    public bool IsWithinSelectionBounds(GameObject gameObject)
    {
        if (!isSelecting)
            return false;

        var camera = Camera.main;
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


    public bool IsActive { get => isActive; set => isActive = value; }
}
