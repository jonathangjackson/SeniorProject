using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using OVR;

public class ArmMenu : MonoBehaviour
{
    //Positions
    public GameObject minerva;
    public GameObject ant;
    public GameObject rig;
    public GameObject leftIK;
    public GameObject rightIK;
    public GameObject parentObj;
    public GameObject rightElbowObj;
    private Vector3 originalPos;
    private Vector3 originalRot;


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
        originalPos = this.GetComponent<RectTransform>().localPosition;
        originalRot = this.GetComponent<RectTransform>().localEulerAngles;
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
                //antHologram.SetActive(false);
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

        leftIK.GetComponent<InverseKinematics>().enabled = false;
        rightIK.GetComponent<InverseKinematics>().enabled = false;
        minerva.GetComponent<PositionConstraint>().enabled = false;
        minerva.transform.parent = null;

        rig.GetComponent<CharacterController>().height = 0.1f;
        Debug.Log(rig.transform.position);
        //rig.transform.localPosition = (new Vector3(0, 0, 0));//
        transform.Translate(Time.deltaTime, 0, 0, Camera.main.transform);
        // rig.transform.position = (new Vector3(0, 0, 0));//
        Debug.Log(rig.transform.position);

        ant.GetComponent<LookAtConstraint>().enabled = false;
        ant.transform.parent = parentObj.transform;
        ant.transform.localPosition = new Vector3(0, 0, 0);
        this.transform.parent = ant.transform;
        //this.transform.position = new Vector3(17, 12.3f, -80.9f);
        this.GetComponent<RectTransform>().localPosition = new Vector3(17, 12.3f, -80.9f);

        this.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, -180, 0);
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
            //antHologram.SetActive(false);
        }
    }


    public void swapToMinerva()
    {
        Vector3 MinervaPos = minerva.transform.position;

        ant.transform.parent = null;

        rig.GetComponent<CharacterController>().height = 0.9f;
        rig.transform.position = MinervaPos;

        ant.GetComponent<LookAtConstraint>().enabled = true;
        minerva.transform.parent = parentObj.transform;
        minerva.transform.localEulerAngles = new Vector3(0, 0, 0);
        //ant.transform.position = new Vector3(0, 0, 0);


        leftIK.GetComponent<InverseKinematics>().enabled = true;
        rightIK.GetComponent<InverseKinematics>().enabled = true;
        minerva.GetComponent<PositionConstraint>().enabled = true;

        this.transform.parent = rightElbowObj.transform;
        this.GetComponent<RectTransform>().localPosition = originalPos;
        this.GetComponent<RectTransform>().localEulerAngles = originalRot;
    }

    public void openTools()
    {
        Debug.Log("Open Tools");
    }

    public void closeTools()
    {

    }
}
