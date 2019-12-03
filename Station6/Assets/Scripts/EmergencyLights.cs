using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmergencyLights : MonoBehaviour
{

    private Light emergencyLight;
    private float waitTime;
    private float currentTime;
    bool toggleLight;
    // Start is called before the first frame update
    void Start()
    {
        emergencyLight = GetComponent<Light>();
        emergencyLight.intensity = 0;

        waitTime = 0.07F;
        //currentTime = Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime > waitTime)
        {
            currentTime = 0;

            emergencyLight.intensity += 0.05F;

            if (emergencyLight.intensity >= 1)
            {
                emergencyLight.intensity = 0;

            }
        }
    }
       
}
