using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lightable : MonoBehaviour
{
    // 4 images for each light direction
    public Sprite[] shadowSprites;

    private GameObject[] shadows;

    public bool[] litDirections
    {
        get
        {
            bool[] directions = new bool[4];
            for(int i = 0; i < 4; i++)
            {
                directions[i] = shadows[i] != null;
            }
            return directions;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        shadows = new GameObject[4];
    }
    
    public void Light(int direction)
    {
        shadows[direction] = new GameObject();
        shadows[direction].transform.parent = transform;
        shadows[direction].transform.localPosition = new Vector3(0, 0, 0);
        shadows[direction].transform.localEulerAngles = new Vector3(0, 0, direction * 90);
        SpriteRenderer s = shadows[direction].AddComponent<SpriteRenderer>();
        s.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        s.sprite = shadowSprites[direction];

        int lights = 0;
        foreach(GameObject obj in shadows)
        {
            if(obj)
            {
                lights++;
            }
        }
        foreach(GameObject obj in shadows)
        {
            if(!obj) { continue; }
            s = obj.GetComponent<SpriteRenderer>();
            float brightness = ((lights - 1) / 4.0f);
            s.color = new Color(brightness, brightness, brightness, 1);
        }
    }

    public void Unlight(int direction)
    {
        if(shadows[direction])
        {
            Destroy(shadows[direction]);
            shadows[direction] = null;
        }
    }
}
