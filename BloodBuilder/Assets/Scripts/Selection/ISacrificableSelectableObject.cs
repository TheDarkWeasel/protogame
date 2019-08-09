using UnityEngine;
using System.Collections;

public interface ISacrificableSelectableObject : IPlayerSelectableObject
{
    int GetBloodAmount();
    void Sacrifice();
}
