using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : Interactable
{
    public Lightable lightable;
    public List<Togglable> targets;
    public Sprite[] onSprites;
    public Sprite[] offSprites;

    private bool triggered;

    public override void onInteract()
    {
        for(int i = 0; i < targets.Count; i++)
        {
            targets[i].Toggle();
        }
        triggered = !triggered;
        lightable.shadowSprites = triggered ? onSprites : offSprites;
        bool[] directions = lightable.litDirections;
        for(int i = 0; i < 4; i++)
        {
            if(directions[i])
            {
                lightable.Unlight(i);
                lightable.Light(i);
            }
        }
    }
}
