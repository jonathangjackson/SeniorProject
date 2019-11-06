using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraViewSwitch : MonoBehaviour
{

    public Camera playerCamera;

    GameObject MinervaUI;
    GameObject AntUI;

    GameObject tempCamPlaceMinerva;
    GameObject tempCamPlaceAnt;

    Vector3 Minerva_Camera_Pos;
    Vector3 S5ANT_Camera_Pos;

    bool BotActive = false;
    bool Minerva_Active = true;


    // Start is called before the first frame update
    void Start()
    {

        MinervaUI = GameObject.Find("FPSController/Minerva/Camera/MinervaCanvas");
        AntUI = GameObject.Find("FPSController/Minerva/Camera/AntCanvas");

        tempCamPlaceMinerva = GameObject.Find("FPSController/Minerva");
        if(tempCamPlaceMinerva != null)
        {
            Minerva_Camera_Pos = new Vector3(tempCamPlaceMinerva.transform.position.x, tempCamPlaceMinerva.transform.position.y, tempCamPlaceMinerva.transform.position.z);
        }

        tempCamPlaceAnt = GameObject.Find("FPSController2/Ant");
        if (tempCamPlaceAnt != null)
        {
            S5ANT_Camera_Pos = new Vector3(tempCamPlaceAnt.transform.position.x, tempCamPlaceAnt.transform.position.y, tempCamPlaceAnt.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        S5ANT_Camera_Pos = new Vector3(tempCamPlaceAnt.transform.position.x, tempCamPlaceAnt.transform.position.y, tempCamPlaceAnt.transform.position.z);
        Minerva_Camera_Pos = new Vector3(tempCamPlaceMinerva.transform.position.x, tempCamPlaceMinerva.transform.position.y, tempCamPlaceMinerva.transform.position.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Minerva_Active == true)
            {
                botView();
            }

            else if (BotActive == true)
            {
                minervaCameraView();
            }
        }
    }

    private void botView()
    {
        MinervaUI.SetActive(false);
        AntUI.SetActive(true);
        Minerva_Active = false;
        BotActive = true;
        playerCamera.transform.parent = tempCamPlaceAnt.transform;
        playerCamera.transform.position = S5ANT_Camera_Pos;
    }

    private void minervaCameraView()
    {
        MinervaUI.SetActive(true);
        AntUI.SetActive(false);
        Minerva_Active = true;
        BotActive = false;
        playerCamera.transform.parent = tempCamPlaceMinerva.transform;
        playerCamera.transform.position = Minerva_Camera_Pos;

    }
}
