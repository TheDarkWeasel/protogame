using System.Collections.Generic;

public interface SelectableObjectContainer
{
    /**
     * Returns true, if objects have been added to outParam
     **/
    bool GetPlayerSelectableObjects(List<PlayerSelectableObject> outParam, SelectionState selectionState);
}
