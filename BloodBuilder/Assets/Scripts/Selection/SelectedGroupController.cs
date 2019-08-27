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

    private readonly GameObject selectionCirclePrefab;
    private ObjectPool<GameObject> selectionCirclePool;

    public SelectedGroupController(ContextProvider context)
    {
        this.context = context;
        mainCamera = Camera.main;
        selectionCirclePrefab = Resources.Load<GameObject>(GameController.GetGlobalTheme().GetSelectionCirclePrefabPath());
        this.selectionCirclePool = new ObjectPool<GameObject>(() => InternalCreateSelectionCircle(), (i) => InternalActivateSelectionCircle(i), (i) => InternalDeactivateSelectionCircle(i));
    }

    private GameObject InternalCreateSelectionCircle()
    {
        return Object.Instantiate(selectionCirclePrefab);
    }

    private void InternalActivateSelectionCircle(GameObject selectionCircle)
    {
        selectionCircle.SetActive(true);
    }

    private void InternalDeactivateSelectionCircle(GameObject selectionCircle)
    {
        selectionCircle.SetActive(false);
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

            //TODO The code below, could take longer to execute. It should be put into another thread.
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
                            playerSelectableObject.OnSecondaryAction(hitInfo.point + offset, new List<Vector3>());
                            ShowTargetClue(offset);
                        }
                    }
                    else
                    {
                        //If the click is inside the current formation, just move the units to the point (with a small random offset).
                        List<Vector3> offsets = new List<Vector3>();

                        float range = 2.5f;

                        for (int i = 0; i < selectedObjects.Count; i++)
                        {
                            Vector3 vectorToAdd = new Vector3(Random.Range(-range, range), 0, Random.Range(-range, range));

                            while (Utils.CheckIfBlocked(vectorToAdd, 1.0f, offsets))
                            {
                                vectorToAdd.x += Random.Range(-range, range);
                                vectorToAdd.z += Random.Range(-range, range);
                            }
                            offsets.Add(vectorToAdd);
                        }

                        for (int i = 0; i < selectedObjects.Count; i++)
                        {
                            IPlayerSelectableObject playerSelectableObject = selectedObjects[i];
                            Vector3 offset = offsets[i];
                            playerSelectableObject.OnSecondaryAction(hitInfo.point + offset, new List<Vector3>());
                            ShowTargetClue(offset);
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
        GameObject targetClue = selectionCirclePool.GetObject();
        targetClue.transform.position = hitInfo.point + offset;
        targetClue.transform.eulerAngles = new Vector3(90, 0, 0);
        targetClue.GetComponent<Projector>().orthographicSize = 1f;
        targetClue.GetComponent<Animation>().Play("SelectionFadeOut");
        context.GetMonoBehaviour().StartCoroutine(Utils.ExecuteAfterTime(1f, () => PutBackToPool(targetClue)));
    }

    private void PutBackToPool(GameObject selectionCircle)
    {
        selectionCirclePool.PutObject(selectionCircle);
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