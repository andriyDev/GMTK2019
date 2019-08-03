using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera managedCamera;
    public CameraTarget target;
    public float positionMoveSpeed = 10;
    public float zoomSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        float distanceTime = (target.transform.position - managedCamera.transform.position).magnitude / positionMoveSpeed;
        float zoomTime = Mathf.Abs(target.viewSize - managedCamera.orthographicSize) / zoomSpeed;
        float maxTime = Mathf.Max(distanceTime, zoomTime);
        if(maxTime == 0)
        {
            maxTime = 1;
        }
        managedCamera.transform.position = Vector3.MoveTowards(managedCamera.transform.position, target.transform.position, positionMoveSpeed * (distanceTime / maxTime) * Time.deltaTime);
        managedCamera.orthographicSize = Mathf.MoveTowards(managedCamera.orthographicSize, target.viewSize, zoomSpeed * Time.deltaTime * (zoomTime / maxTime));
    }
}
