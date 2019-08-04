using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : Interactable
{
    public List<Component> targets;

    public override void onInteract() {

        for (int i = 0; i < targets.Count; i++)
        {
            ((Togglable)targets[i]).Toggle();
        }
        Destroy(this.gameObject);
    }
}
