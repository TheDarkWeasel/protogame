using System.Collections.Generic;

public interface ISelectableObjectContainer
{
    /**
     * Returns true, if objects have been added to outParam
     **/
    bool GetPlayerSelectableObjects(List<IPlayerSelectableObject> outParam, SelectionState selectionState);

    /**
     * Returns true, if objects have been added to outParam
     **/
    bool GetSacrificableSelectableObjects(List<ISacrificableSelectableObject> outParam, SelectionState selectionState);
}
