using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SwitchMat : MonoBehaviour
{
    public GameObject sonarOnOff;
    public Material replacementMat;
    private List<Material> currentMat = new List<Material>();
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
            for(int i = 0; i < rend.materials.Length; i++)
            {
                currentMat.Add(rend.materials[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (sonarOnOff.GetComponent<ArmMenu>().SonarWaveOn && !on)
        {
            on = true;
            Material[] tempMats = new Material[rend.materials.Length];

            for (int i = 0; i < currentMat.Count; i++)
            {
                tempMats[i] = replacementMat;
            }
            rend.materials = tempMats;
            return;
        }
        if (!sonarOnOff.GetComponent<ArmMenu>().SonarWaveOn && on)
        {
            on = false;

            Material[] tempMats = new Material[currentMat.Count];
            for (int i = 0; i < currentMat.Count; i++)
            {
                tempMats[i] = currentMat[i];
            }
            rend.materials = tempMats;

            return;
        }
    }
}
