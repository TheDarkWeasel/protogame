using System.Collections.Generic;
using UnityEngine;

public class SelectedGroupController
{
    private ContextProvider context;
    private bool isActive = false;
    private Camera mainCamera;
    private RaycastHit hitInfo;

    public SelectedGroupController(ContextProvider context)
    {
        this.context = context;
        mainCamera = Camera.main;
    }

    public void Update()
    {
        if (context.GetPlayerObjectPool() != null)
        {
            List<IPlayerSelectableObject> selectedObjects = context.GetPlayerObjectPool().GetSelectedObjects();

            //TODO I don't know, if this will be the final way of triggering the unit building process.
            //It does not feel right and looks inperfomant. Very likely to be changed. But for now it should work.
            foreach (IPlayerSelectableObject playerSelectableObject in selectedObjects)
            {
                playerSelectableObject.Update();
            }

            if (isActive && Input.GetMouseButtonUp(1))
            {
                Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hitInfo))
                {
                    foreach (IPlayerSelectableObject playerSelectableObject in selectedObjects)
                    {
                        playerSelectableObject.OnSecondaryAction(hitInfo.point);
                    }
                }
            }
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