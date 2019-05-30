using UnityEngine;
using System.Collections;

public class PCCameraController : MonoBehaviour
{

    public float scrollZone = 30;
    public float scrollSpeed = 5;

    //TODO values dependent on map
    public float xMax = 20;
    public float xMin = -20;
    public float yMax = 30;
    public float yMin = 10;
    public float zMax = 20;
    public float zMin = -20;

    private Vector3 desiredPosition;

    void Start()
    {
        desiredPosition = transform.position;
    }

    void Update()
    {
        float x = 0;
        float y = 0;
        float z = 0;
        float speed = scrollZone * Time.deltaTime;

        if(Input.mousePosition.x < scrollZone || Input.GetKey(GameController.GetHotkeys().GetCameraMoveLeftHotkey()))
        {
            x -= speed;
        } else if(Input.mousePosition.x > Screen.width - scrollZone || Input.GetKey(GameController.GetHotkeys().GetCameraMoveRightHotkey()))
        {
            x += speed;
        }

        if(Input.mousePosition.y < scrollZone || Input.GetKey(GameController.GetHotkeys().GetCameraMoveDownHotkey()))
        {
            z -= speed;
        } else if(Input.mousePosition.y > Screen.height - scrollZone || Input.GetKey(GameController.GetHotkeys().GetCameraMoveUpHotkey()))
        {
            z += speed;
        }

        y += Input.GetAxis("Mouse ScrollWheel");

        Vector3 move = new Vector3(x, y, z) + desiredPosition;
        move.x = Mathf.Clamp(move.x, xMin, xMax);
        move.y = Mathf.Clamp(move.y, yMin, yMax);
        move.z = Mathf.Clamp(move.z, zMin, zMax);
        desiredPosition = move;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, 0.2f);
    }
}
