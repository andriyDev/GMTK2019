using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRegion : MonoBehaviour
{
    public CameraTarget target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!collision.CompareTag("Player"))
        {
            return;
        }
        CameraManager manager = FindObjectOfType<CameraManager>();
        if(!manager)
        {
            return;
        }

        manager.target = target;
    }
}
