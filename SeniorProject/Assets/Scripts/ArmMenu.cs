using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using OVR;

public class ArmMenu : MonoBehaviour
{
    private IEnumerator coroutine;
    //Positions
    public GameObject VRMovement;
    public GameObject minerva;
    public GameObject ant;
    public GameObject rig;
    public GameObject leftIK;
    public GameObject rightIK;
    public GameObject centerEyeCamera;
    public GameObject rightElbowObj;
    public GameObject trackingSpace;
    public GameObject gun;
    public Material hologramMat;
    private Vector3 originalPos;
    private Vector3 originalRot;
    private bool grabGun = false;
    private bool grabbedGun = false;
    Renderer rend;
    Material currentMat;


    //Input for Ant Placement
    public GameObject antHologram;
    public LineRenderer antLineRender;
    public GameObject indexPos;
    public GameObject rightAnchor;

    public GameObject leftController, rightController;

    private bool menuOn = false;
    private bool placeAnt = false;

    private float rayLength = 2.0f;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

        rend = gun.GetComponent<Renderer>();
        currentMat = rend.material;
        originalPos = this.GetComponent<RectTransform>().localPosition;
        originalRot = this.GetComponent<RectTransform>().localEulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        //if (OVRInput.GetDown(OVRInput.Button.Three))
        //{

          //  menuOn = !menuOn;
         //   transform.GetChild(0).gameObject.SetActive(menuOn);
        //}

        if (slider.value == 1)
        {
            menuOn = true;
            transform.GetChild(0).gameObject.SetActive(menuOn);
        }
        if(slider.value == 0)
        {
            menuOn = false;
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

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.9f && grabGun)
        {
            grabbedGun = true;
            grabGun = false;
            rend.material = currentMat;
            gun.transform.GetChild(0).GetComponent<Pulse>().isActive = true;
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) < 0.9f && grabbedGun)
        {
            grabbedGun = false;
            gun.transform.GetChild(0).GetComponent<Pulse>().isActive = false;
            gun.gameObject.SetActive(false);
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
        //centerEyeCamera.GetComponent<Camera>().nearClipPlane = 1.0f;
        placeAnt = true;
        //Turn on touch controllers
        leftController.SetActive(true);
        rightController.SetActive(true);

        Vector3 AntPos = antHologram.transform.position;
        AntPos = new Vector3(AntPos.x, AntPos.y + 1.0f, AntPos.z);

        leftIK.GetComponent<InverseKinematics>().enabled = false;
        rightIK.GetComponent<InverseKinematics>().enabled = false;
        minerva.GetComponent<PositionConstraint>().enabled = false;
        minerva.transform.parent = null;

        rig.GetComponent<CharacterController>().height = 0.1f;
        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
        rig.transform.position = AntPos;//


        ant.GetComponent<LookAtConstraint>().enabled = false;
        //ant.transform.parent = centerEyeCamera.transform;
        ant.GetComponent<PositionConstraint>().constraintActive = true;
        ant.transform.position = new Vector3(ant.transform.position.x, ant.transform.position.y, ant.transform.position.z);
        ant.transform.localEulerAngles = new Vector3(0, 90, 0);
        this.transform.parent = ant.transform;
        //this.transform.position = new Vector3(17, 12.3f, -80.9f);
        this.GetComponent<RectTransform>().localPosition = new Vector3(17, 12.3f, -100.9f);

        this.GetComponent<RectTransform>().localEulerAngles = new Vector3(0, -180, 0);

        trackingSpace.transform.localPosition = new Vector3(0, -0.719f, 0);
        coroutine = WaitAndPrint(0.1f);
        StartCoroutine(coroutine);
    }
    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = false;

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
        centerEyeCamera.GetComponent<Camera>().nearClipPlane = 0.01f;

        leftController.SetActive(false);
        rightController.SetActive(false);

        Vector3 MinervaPos = minerva.transform.position;

        ant.transform.parent = null;

        rig.GetComponent<CharacterController>().height = 1.9f;

        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
        rig.transform.position = MinervaPos;
        //VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = false;

        ant.GetComponent<LookAtConstraint>().enabled = true;
        minerva.transform.parent = rig.transform;
        minerva.transform.localEulerAngles = new Vector3(0, 0, 0);
        //ant.transform.position = new Vector3(0, 0, 0);


        leftIK.GetComponent<InverseKinematics>().enabled = true;
        rightIK.GetComponent<InverseKinematics>().enabled = true;
        minerva.GetComponent<PositionConstraint>().enabled = true;

        this.transform.parent = rightElbowObj.transform;
        this.GetComponent<RectTransform>().localPosition = originalPos;
        this.GetComponent<RectTransform>().localEulerAngles = originalRot;
        trackingSpace.transform.localPosition = new Vector3(0, 0, 0);

        coroutine = WaitAndPrint(0.1f);
        StartCoroutine(coroutine);
    }

    public void openTools()
    {
        gun.gameObject.SetActive(true);
        rend.material = hologramMat;
        grabGun = true;
    }

    public void closeTools()
    {

    }
}
