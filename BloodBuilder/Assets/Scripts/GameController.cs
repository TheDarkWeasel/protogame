using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    private PlacementController mPlacementController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject playerBase = Resources.Load<GameObject>("Models/PlayerBase");
        mPlacementController = new PlacementController(playerBase, KeyCode.A);
    }

    // Update is called once per frame
    void Update()
    {
        if(mPlacementController != null)
        {
            mPlacementController.Update();
        }
    }
}
