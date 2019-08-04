using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour, Togglable
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
                    lightable.Light(this);
                }
                else
                {
                    lightable.Unlight(this);
                }
            }
            mask.enabled = _lightOn;
            sprite.enabled = _lightOn;
        }
    }

    public SpriteRenderer sprite;
    public SpriteMask mask;
    public GameObject sourcePoint;

    public float range;
    public float flickerRate = -1;

    [SerializeField]
    private bool _lightOn;

    private List<Lightable> lightables = new List<Lightable>();

    void OnTriggerEnter2D(Collider2D c)
    {
        Lightable l = c.gameObject.GetComponent<Lightable>();
        if(l)
        {
            lightables.Add(l);
            if(lightOn)
            {
                l.Light(this);
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
                l.Unlight(this);
            }
        }
    }

    float time = 0;
    
    void Start()
    {
        lightOn = lightOn;
        sprite.material.SetVector("_LightPosition", new Vector4(sourcePoint.transform.position.x, sourcePoint.transform.position.y));
        sprite.material.SetFloat("_LightRange", range);
    }

    // Update is called once per frame
    void Update()
    {
        if (flickerRate > 0)
        {
            time += Time.deltaTime;
            if (time > flickerRate)
            {
                time -= flickerRate;
                lightOn = !lightOn;
            }
        }
    }

    public void Toggle()
    {
        lightOn = !lightOn;
    }
}
