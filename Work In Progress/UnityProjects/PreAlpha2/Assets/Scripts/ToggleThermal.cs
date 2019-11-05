using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ToggleThermal : MonoBehaviour
{
    public Shader shade;
    public Material thermMat;
    private bool thermOnOff;
    List<GameObject> thermalShaderObjects = new List<GameObject>();
    void Start()
    {
        thermOnOff = false;
        Debug.Log(thermMat.shader.name.ToString());
        GameObject[] obs = (GameObject[])Object.FindObjectsOfType(typeof(GameObject));

        for (int i = 0; i < obs.Length; i++)
        {

            Debug.Log(obs[i].name);
            if (obs[i].GetComponent<Renderer>() != null)
            {
                Debug.Log("IN");
                if (obs[i].GetComponent<Renderer>().material.shader.name.ToString().CompareTo(shade.name) == 0)//.name.ToString().CompareTo(thermMat.shader.name.ToString())
                {
                    thermalShaderObjects.Add(obs[i]);
                }
            }
        }
    }

    void Update()
    {
        
        if (OVRInput.GetDown(OVRInput.Button.Two)) // Input.GetKeyDown("space")
        {
            for(int i = 0; i < thermalShaderObjects.Count; i++)
            {
                if(!thermOnOff)
                    thermalShaderObjects[i].GetComponent<Renderer>().material.SetFloat("_Thermal", 1.0f);
                else
                    thermalShaderObjects[i].GetComponent<Renderer>().material.SetFloat("_Thermal", 0.0f);
                thermOnOff = !thermOnOff;
            }
        }
    }
}
