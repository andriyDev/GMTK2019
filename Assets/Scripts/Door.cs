using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, Togglable
{
    public SpriteRenderer doorRenderer;
    public Collider2D doorCollider;

    public void Toggle()
    {
        doorRenderer.enabled = !doorRenderer.enabled;
        doorCollider.enabled = !doorCollider.enabled;
    }
}
