using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grabTurnOff : MonoBehaviour
{

    public GameObject tooltip;
    public OVRGrabbable grabbedObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(grabbedObject.isGrabbed == true)
        {
            tooltip.SetActive(false);
        }
    }
}
