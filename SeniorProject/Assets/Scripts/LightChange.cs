using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChange : MonoBehaviour
{
    public List<GameObject> lightObjects;
    public Color a;
    public Color b;
    Color c;
    Color d;
    List<Renderer> objectRenders = new List<Renderer>();
    List<Material> objectMaterials = new List<Material>();
    public bool swap;
    public float rate;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        c = a;
        for(int i = 0; i < lightObjects.Count; i++)
        {
            objectRenders.Add(lightObjects[i].GetComponent<Renderer>());
            objectMaterials.Add(objectRenders[i].material);//objectRenders[0].GetComponent<Material>()
            
        }
        Debug.Log("OBJECTS = " + objectMaterials.Count);
    }

    Color invert(Color x)
    {
        return new Color(1.0f - x.r, 1.0f - x.g, 1.0f - x.b);
    }

    // Update is called once per frame
    void Update()
    {
        time += rate * Time.deltaTime;
        float sine = Mathf.Sin(time);
        if (sine > 0)
        {
            c = new Color(Mathf.Abs((a.r - (b.r * (1.0f - Mathf.Abs(sine))))), Mathf.Abs(a.g - (b.g * (1.0f - Mathf.Abs(sine)))), Mathf.Abs(a.b - (b.b * (1.0f - Mathf.Abs(sine)))));
        }
        else
        {
            c = new Color(Mathf.Abs(b.r - (a.r * (1.0f - Mathf.Abs(sine)))), Mathf.Abs(b.g - (a.g * (1.0f - Mathf.Abs(sine)))), Mathf.Abs(b.b - (a.b * (1.0f - Mathf.Abs(sine)))));
        }
        //d = invert(c);
        for(int i = 0; i < objectMaterials.Count; i++)
        {
            objectMaterials[i].SetColor("_EmissionColor", c);
        }
        //Debug.Log("TIME = " + );//_EmissionColor
    }
}
