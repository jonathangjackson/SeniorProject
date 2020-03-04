using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchMat : MonoBehaviour
{
    public GameObject sonarOnOff;
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
        if (sonarOnOff.GetComponent<ArmMenu>().SonarWaveOn && !on)
        {
            on = true;
            rend.material = replacementMat;
            return;
        }
        if (!sonarOnOff.GetComponent<ArmMenu>().SonarWaveOn && on)
        {
            on = false;
            rend.material = currentMat;
            return;
        }
    }
}
