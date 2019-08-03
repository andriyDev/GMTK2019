using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : Interactable
{
    public override void onInteract() {
        Destroy(this.gameObject);
    }
}
