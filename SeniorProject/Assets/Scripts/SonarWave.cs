using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class SonarWave : MonoBehaviour
{
    public Material sonarMat;
    public Shader xRayShader;
    public GameObject arPlaneTrigger;

    //public PostProcessVolume volume;
    private bool on = false;
    // Start is called before the first frame update
    void Start()
    {
        arPlaneTrigger.GetComponent<BoxCollider>().enabled = false;
        sonarMat.SetFloat("_WaveActive", 0.0f);
        sonarMat.SetFloat("_WaveDistance", 0.0f);
        sonarMat.SetFloat("_WaveAlpha", 1.0f);
        //gameObject.GetComponent<Renderer>().sharedMaterial.SetFloat("_WaveActive", 1.0f);
    }
    private void OnEnable()
    {

    }
    // Update is called once per fra.me
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Four)) 
        {
            if (!on)
            {
                arPlaneTrigger.GetComponent<BoxCollider>().enabled = true;
                on = true;
                sonarMat.SetFloat("_WaveActive", 1.0f);
                arPlaneTrigger.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y - 90.0f, 90);
                arPlaneTrigger.transform.position = this.transform.position;
                //volume.weight = 1.0f;
            }
            else
            {
                arPlaneTrigger.GetComponent<BoxCollider>().enabled = false;
                on = false;
                sonarMat.SetFloat("_WaveActive", 0.0f);
                //volume.weight = 0.0f;
                sonarMat.SetFloat("_WaveDistance", 0.0f);
                sonarMat.SetFloat("_WaveAlpha", 1.0f);
            }
        }
        if (on)
        {
            arPlaneTrigger.transform.parent.gameObject.transform.eulerAngles = new Vector3(-90, this.transform.eulerAngles.y - 90.0f, 90);
            arPlaneTrigger.transform.localPosition -= new Vector3(0, 4.0f * Time.deltaTime, 0);
            //arPlaneTrigger.transform.localEulerAngles = new Vector3(-90, this.transform.eulerAngles.y - 90.0f, 90);
            //arPlaneTrigger.transform.forward = new Vector3(0, 4.0f * Time.deltaTime, 0);
            drawSonar();
        }
    }

    void drawSonar()
    {
        if (sonarMat.GetFloat("_WaveDistance") < 20)
        {

            sonarMat.SetFloat("_WaveDistance", sonarMat.GetFloat("_WaveDistance") + (4 * Time.deltaTime));
            //sonarMat.SetFloat("_WaveAlpha", sonarMat.GetFloat("_WaveAlpha") - (0.1f * Time.deltaTime));
        }
        else
        {
            arPlaneTrigger.transform.position = this.transform.position;
            sonarMat.SetFloat("_WaveDistance", 0.0f);
            //sonarMat.SetFloat("_WaveAlpha", 1.0f);
        }
    }
}
