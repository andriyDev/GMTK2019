using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public int direction;

    void OnTriggerEnter2D(Collider2D c)
    {
        Lightable l = c.gameObject.GetComponent<Lightable>();
        if(l)
        {
            l.Light(direction);
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Lightable l = c.gameObject.GetComponent<Lightable>();
        if(l)
        {
            l.Unlight(direction);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
