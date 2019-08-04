using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public Lightable lightable;
    public List<Component> targets;
    public Sprite onSprite;
    public Sprite offSprite;

    private bool triggered;

    public override void onInteract()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            ((Togglable)targets[i]).Toggle();
        }
        triggered = !triggered;
        lightable.shadowSprite = triggered ? onSprite : offSprite;
    }
}
