using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ParticleOnOff : MonoBehaviour
{
    bool onOff;
    // Start is called before the first frame update
    void Start()
    {
        onOff = false;
        this.GetComponent<ParticleSystem>().Stop();
        this.GetComponent<ParticleSystem>().Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            if (onOff)
            {
                onOff = false;
                this.GetComponent<ParticleSystem>().Stop();
                this.GetComponent<ParticleSystem>().Clear();
            }
            else
            {
                onOff = true;
                this.GetComponent<ParticleSystem>().Play();
            }
        }
    }
}
