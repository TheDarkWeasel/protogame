using UnityEngine;
using System.Collections;

public interface PlayerSelectableObject
{
    /**
     * If selected is true, object will be selected. Otherwise it will be deselected.
     * **/
    void Select(bool selected);
    bool IsSelected();

    void Update();

    GameObject GetGameObject();
    GameObject GetSelectionCircle();
    void SetSelectionCircle(GameObject selectionCircle);
}
