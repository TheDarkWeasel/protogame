using UnityEngine;
using System.Collections;

public class SelectionController
{

    private bool isActive = false;

    public void Update()
    {
        if(isActive)
        {
            //TODO selection
        }
    }


    public bool IsActive { get => isActive; set => isActive = value; }
}
