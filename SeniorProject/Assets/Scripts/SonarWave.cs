using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SonarWave : MonoBehaviour
{
    public Material sonarMat;
    //public PostProcessVolume volume;
    private bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_WaveActive", 1.0f);
    }

    // Update is called once per fra.me
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four)) 
        {
            if (!on)
            {
                on = true;
                sonarMat.SetFloat("_WaveActive", 1.0f);
                //volume.weight = 1.0f;
            }
            else
            {
                on = false;
                sonarMat.SetFloat("_WaveActive", 0.0f);
                //volume.weight = 0.0f;
                sonarMat.SetFloat("_WaveDistance", 0.3f);
            }
        }
        if (on)
        {
            drawSonar();
        }
    }

    void drawSonar()
    {
        if (sonarMat.GetFloat("_WaveDistance") > 0)
        {
            sonarMat.SetFloat("_WaveDistance", sonarMat.GetFloat("_WaveDistance") - (0.05f * Time.deltaTime));
        }
        else
        {
            sonarMat.SetFloat("_WaveDistance", 0.3f);
        }
    }
}
