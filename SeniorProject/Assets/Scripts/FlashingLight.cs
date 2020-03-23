using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashingLight : MonoBehaviour
{
    public List<GameObject> lightObjects = new List<GameObject>();
    public Material emissionMat;
    public bool setFlashing = false;
    private float timer = 0.0f;

    private bool flasing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (setFlashing)
        {
            setFlashing = false;
            flasing = true;
            for(int i = 0; i < lightObjects.Count; i++)
            {
                lightObjects[i].GetComponent<Renderer>().material = emissionMat;
            }
        }
        if (flasing)
        {
            if (timer > 0.0f)
            {
                timer -= 0.5f * Time.deltaTime;
            }
            else
            {

            }
        }
    }
}
