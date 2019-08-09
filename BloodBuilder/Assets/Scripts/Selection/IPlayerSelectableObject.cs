using UnityEngine;
using System.Collections.Generic;

public interface IPlayerSelectableObject
{
    /**
     * If selected is true, object will be selected. Otherwise it will be deselected.
     * **/
    void Select(bool selected);
    bool IsSelected();

    /**
     * Returns the priority of the selected unit for being displayed in the HUD as "main"-selection.
     * The lower the number, the higher the priority.
     */
    int GetSelectionPriority();

    /**
     * Returns the build-actions to be displayed in the action-ui
     */
    List<BuildChoice> GetBuildChoices();

    void Update();

    GameObject GetGameObject();
    void CreateSelectionCircle(GameObject selectionCirclePrefab);
    void DestroySelectionCircle();
}
