using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera managedCamera;
    public CameraTarget target;
    public float positionMoveSpeed = 10;
    public float zoomSpeed = 1;

    private float zoomVelocity = 0;
    private Vector3 positionVelocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(!target)
        {
            return;
        }
        float distanceTime = (target.transform.position - managedCamera.transform.position).magnitude / positionMoveSpeed;
        float zoomTime = Mathf.Abs(target.viewSize - managedCamera.orthographicSize) / zoomSpeed;
        float maxTime = Mathf.Max(distanceTime, zoomTime);
        if(maxTime == 0)
        {
            maxTime = 1;
        }
        if(zoomTime == 0) { zoomTime = 1; }
        if(distanceTime == 0) { distanceTime = 1; }
        managedCamera.transform.position = Vector3.SmoothDamp(managedCamera.transform.position, target.transform.position, ref positionVelocity, positionMoveSpeed * (maxTime / distanceTime));
        managedCamera.orthographicSize = Mathf.SmoothDamp(managedCamera.orthographicSize, target.viewSize, ref zoomVelocity, zoomSpeed * (maxTime / zoomTime    ));
    }
}
