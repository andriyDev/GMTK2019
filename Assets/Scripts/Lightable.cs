using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Lightable : MonoBehaviour
{
    public Sprite shadowSprite
    {
        get
        {
            return _shadowSprite;
        }
        set
        {
            _shadowSprite = value;
            foreach(GameObject obj in shadows.Values)
            {
                obj.GetComponent<SpriteRenderer>().sprite = value;
            }
        }
    }

    [SerializeField]
    private Sprite _shadowSprite;
    public float shadowSize = 1;
    public float shadowBias = 0;
    public float shadowLightBias = 0.5f;

    private Dictionary<LightSource, GameObject> shadows;

    // Start is called before the first frame update
    void Start()
    {
        shadows = new Dictionary<LightSource, GameObject>();
    }

    private void Update()
    {
        foreach(KeyValuePair<LightSource, GameObject> pair in shadows)
        {
            Vector3 delta = (transform.position - pair.Key.sourcePoint.transform.position);
            pair.Value.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg + 270);
            pair.Value.transform.position = delta.normalized * shadowBias + transform.position;
            SpriteRenderer s = pair.Value.GetComponent<SpriteRenderer>();
            float brightness = 1.0f / (-(shadows.Count - 1) - 1) + 1;
            s.color = new Color(0, 0, 0, (1 - brightness) * (1 - Mathf.Min((delta.magnitude + shadowLightBias) / pair.Key.range, 1.0f)));
        }
    }

    public void Light(LightSource source)
    {
        if (shadows.ContainsKey(source))
        {
            return;
        }
        GameObject newShadow = new GameObject();
        shadows.Add(source, newShadow);
        newShadow.transform.parent = transform;
        newShadow.transform.localPosition = new Vector3(0, 0, 0);
        newShadow.transform.localScale = Vector3.one * shadowSize;
        SpriteRenderer s = newShadow.AddComponent<SpriteRenderer>();
        s.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        s.sprite = shadowSprite;
        s.sortingOrder = -1;

        Update();
    }

    public void Unlight(LightSource source)
    {
        if(shadows.ContainsKey(source))
        {
            GameObject shadow = shadows[source];
            shadows.Remove(source);
            Destroy(shadow);

            Update();
        }
    }
}
