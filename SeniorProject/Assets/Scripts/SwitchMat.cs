using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchMat : MonoBehaviour
{
    public Material replacementMat;
    private Material currentMat;
    Renderer rend;
    private bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        if(this.GetComponent<MeshRenderer>() == null)
        {
            this.gameObject.GetComponent<SwitchMat>().enabled = false;
        }
        else
        {
            rend = GetComponent<Renderer>();
            currentMat = rend.material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four))
        {
            if(!on)
                rend.material = replacementMat;
            else
                rend.material = currentMat;
            on = !on;
        }
    }
}
