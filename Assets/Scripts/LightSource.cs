using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    [SerializeField]
    public bool lightOn
    {
        get
        {
            return _lightOn;
        }
        set
        {
            _lightOn = value;
            foreach (Lightable lightable in lightables)
            {
                if (_lightOn)
                {
                    lightable.Light(direction);
                }
                else
                {
                    lightable.Unlight(direction);
                }
            }
            mask.enabled = _lightOn;
            sprite.enabled = _lightOn;
        }
    }

    public int direction
    {
        get
        {
            return _direction;
        }
        set
        {
            foreach (Lightable lightable in lightables)
            {
                lightable.Unlight(direction);
            }
            _direction = value;
            foreach(Lightable lightable in lightables)
            {
                lightable.Light(direction);
            }
        }
    }

    public SpriteRenderer sprite;
    public SpriteMask mask;

    [SerializeField]
    private bool _lightOn;
    [SerializeField]
    private int _direction;

    private List<Lightable> lightables = new List<Lightable>();

    void OnTriggerEnter2D(Collider2D c)
    {
        Lightable l = c.gameObject.GetComponent<Lightable>();
        if(l)
        {
            lightables.Add(l);
            if(lightOn)
            {
                l.Light(direction);
            }
        }
    }

    void OnTriggerExit2D(Collider2D c)
    {
        Lightable l = c.gameObject.GetComponent<Lightable>();
        if(l)
        {
            lightables.Remove(l);
            if(!lightOn)
            {
                l.Unlight(direction);
            }
        }
    }
    float time = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 2)
        {
            time -= 2;
            lightOn = !lightOn;
        }
    }
}
