using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ArmMenu : MonoBehaviour
{
    //Positions
    public GameObject minerva;
    public GameObject ant;
    public GameObject rig;

    //Input for Ant Placement
    public GameObject antHologram;
    public LineRenderer antLineRender;
    public GameObject indexPos;
    public GameObject rightAnchor;

    private bool menuOn = false;
    private bool placeAnt = false;

    private float rayLength = 2.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            menuOn = !menuOn;
            transform.GetChild(0).gameObject.SetActive(menuOn);
        }


        //Check if ant placement is active
        if (placeAnt)
        {
            activateBotPlacement();
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                placeBot();
                antLineRender.enabled = false;
                antHologram.SetActive(false);
                placeAnt = false;
            }
            //and On Button Press  place Ant
        }

    }

    public void swapToBot()
    {
        //Close Menu
        menuOn = !menuOn;
        transform.GetChild(0).gameObject.SetActive(menuOn);

        //set ant placement to active 
        placeAnt = true;

    }

    private void placeBot()
    {
        Vector3 AntPos = antHologram.transform.position;

        rig.GetComponent<CharacterController>().height = 0.4f;
        /*
        minervaActive = false;
        antActive = true;
        minerva.transform.parent = null;
        rig.transform.position = AntPos;
        ant.transform.parent = rig.transform;*/
    }

    private void activateBotPlacement()
    {

        Vector3 rightHandPos = indexPos.transform.position;
        Vector3 rayDirection = rightAnchor.transform.forward;
        //Debug.Log(transform.forward);
        Ray rayscastRay = new Ray(rightHandPos, rayDirection);
        RaycastHit raycastHit;

        Vector3 rayEndPos = rightHandPos + rayDirection * rayLength;

        bool objectHit = Physics.Raycast(rayscastRay, out raycastHit, rayLength);

        if (objectHit)
        {
            antLineRender.enabled = true;
            antHologram.SetActive(true);
            GameObject hitObject = raycastHit.transform.gameObject;
            Vector3 pos = raycastHit.point;
            antHologram.transform.localPosition = pos;

            antLineRender.SetPosition(0, rightHandPos);
            antLineRender.SetPosition(1, pos);
        }
        else
        {
            antLineRender.enabled = false;
            antHologram.SetActive(false);
        }
    }


    public void swapToMinerva()
    {
        Debug.Log("Swap Minerva");
    }

    public void openTools()
    {
        Debug.Log("Open Tools");
    }

    public void closeTools()
    {

    }
}
