using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerViewSwitch : MonoBehaviour
{

    public Camera Minerva_Camera;
    public Camera S5ANT_Camera;

    int cameraState;
    float waitTime;
    float currentTime;

    // Start is called before the first frame update
    void Start()
    {
        cameraState = 1;
        waitTime = 2.0f;
        currentTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        currentTime += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            cameraState *= -1;
            currentTime -= waitTime;
        }

        if(cameraState == 1)
        {
            minervaCameraView();
        }

        if(cameraState == -1)
        {
            botView();
        }

    }

    private void botView()
    {
        Minerva_Camera.enabled = false;
        S5ANT_Camera.enabled = true;
    }

    private void minervaCameraView()
    {
        Minerva_Camera.enabled = true;
        S5ANT_Camera.enabled = false;
    }
}
