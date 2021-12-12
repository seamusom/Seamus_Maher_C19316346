using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Camera _cam;
    public Transform _target;
    public float _distanceToTarget = 10;

    private Vector3 previousPosition;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousPosition = _cam.ScreenToViewportPoint(Input.mousePosition); // this sets up the camera 
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 newPosition = _cam.ScreenToViewportPoint(Input.mousePosition);
            Vector3 direction = previousPosition - newPosition;

            float rotationAroundYAxis = -direction.x * 180; // camera moves horizontally
            float rotationAroundXAxis = direction.y * 180; // camera moves vertically

            _cam.transform.position = _target.position;

            _cam.transform.Rotate(new Vector3(1, 0, 0), rotationAroundXAxis);
            _cam.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis, Space.World); 

            _cam.transform.Translate(new Vector3(0, 0, -_distanceToTarget));

            previousPosition = newPosition;
        }
    }
}
