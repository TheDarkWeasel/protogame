using UnityEngine;
using System.Collections;

public interface SacrificableSelectableObject : PlayerSelectableObject
{
    int GetBloodAmount();
    void Sacrifice();
}
