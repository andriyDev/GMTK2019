using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TogglableText : MonoBehaviour, Togglable
{
    public void Toggle()
    {
        bool isActive = transform.parent.gameObject.active;
        transform.parent.gameObject.SetActive(!isActive);
    }
}
