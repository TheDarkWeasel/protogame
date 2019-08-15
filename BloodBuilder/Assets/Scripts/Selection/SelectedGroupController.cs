using System.Collections.Generic;
using UnityEngine;

/**
 * Class, which controls the group of currently selected player objects.
 **/
public class SelectedGroupController
{
    private ContextProvider context;
    private bool isActive = false;
    private Camera mainCamera;
    private RaycastHit hitInfo;
    private Vector3 center = new Vector3(0, 0, 0);

    private GameObject selectionCirclePrefab;

    public SelectedGroupController(ContextProvider context)
    {
        this.context = context;
        mainCamera = Camera.main;
        selectionCirclePrefab = Resources.Load<GameObject>(GameController.GetGlobalTheme().GetSelectionCirclePrefabPath());
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
                    //Calculate the center of the currently selected units
                    center.Set(0, 0, 0);

                    foreach (IPlayerSelectableObject playerSelectableObject in selectedObjects)
                    {
                        center += playerSelectableObject.GetGameObject().transform.position;
                    }

                    center /= selectedObjects.Count;

                    float maxDistanceToCenter = 0f;

                    foreach (IPlayerSelectableObject playerSelectableObject in selectedObjects)
                    {
                        maxDistanceToCenter = Mathf.Max(Vector3.Distance(center, playerSelectableObject.GetGameObject().transform.position), maxDistanceToCenter);
                    }

                    if (Vector3.Distance(hitInfo.point, center) > maxDistanceToCenter)
                    {
                        //If the click is outside the current formation,
                        //move the selected units, respecting their current offset from the center of the group (so they stay in the same formation)
                        foreach (IPlayerSelectableObject playerSelectableObject in selectedObjects)
                        {
                            Vector3 offset = playerSelectableObject.GetGameObject().transform.position - center;
                            playerSelectableObject.OnSecondaryAction(hitInfo.point + offset);
                            ShowTargetClue(offset);
                        }
                    }
                    else
                    {
                        //If the click is inside the current formation, just move the units to the point (with a small random offset).
                        foreach (IPlayerSelectableObject playerSelectableObject in selectedObjects)
                        {
                            playerSelectableObject.OnSecondaryAction(hitInfo.point);
                            ShowTargetClue(new Vector3(Random.Range(0, 1.5f), Random.Range(0, 1.5f), Random.Range(0, 1.5f)));
                        }
                    }
                }
            }
        }
    }

    /**
     * Creates a hint to where the units will move to.
     **/
    private void ShowTargetClue(Vector3 offset)
    {
        //TODO Add pooling for this
        GameObject targetClue = Object.Instantiate(selectionCirclePrefab);
        targetClue.transform.position = hitInfo.point + offset;
        targetClue.transform.eulerAngles = new Vector3(90, 0, 0);
        targetClue.GetComponent<Projector>().orthographicSize = 1f;
        targetClue.GetComponent<Animation>().Play("SelectionFadeOut");
        Object.Destroy(targetClue, 1f);
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