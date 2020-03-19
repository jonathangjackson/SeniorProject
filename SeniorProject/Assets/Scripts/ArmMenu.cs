using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using OVR;
using UnityEngine.EventSystems;

public class ArmMenu : MonoBehaviour
{
    public bool SonarWaveOn = false;
    private IEnumerator coroutine;
    //Positions
    public GameObject VRMovement;
    public GameObject minerva;
    public GameObject rig;
    public GameObject leftIK;
    public GameObject rightIK;
    public GameObject centerEyeCamera;
    public GameObject rightElbowObj;
    public GameObject trackingSpace;
    public GameObject[] gun = new GameObject[4];//0 = trigger, 1 = barrel, 2 = pulsescript, 3 = hologram
    public Material hologramMat;
    private Vector3 originalPos;
    private Vector3 originalRot;
    private bool grabGun = false;
    private bool grabbedGun = false;
    Renderer[] rend = new Renderer[2];
    Material[] currentMat = new Material[4];


    //NEW INPUT FOR ANT PLACEMENT
    public GameObject antHologramGrp;
    public GameObject antPlacementPosition;
    public GameObject S5Ant;
    public GameObject eventSystem;
    public GameObject leftBotHand;
    public GameObject pointerIndex;
    public GameObject laserPointer;

    //Input for Ant Placement
    public GameObject antHologram;
    public LineRenderer antLineRender;
    public GameObject indexPos;
    public GameObject rightAnchor;

    public GameObject leftBotIK, rightBotIK;

    private bool menuOn = false;
    private bool placeAnt = false;
    private float dissolveState = 1.0f;

    private float rayLength = 2.0f;

    public Slider slider;
    // Start is called before the first frame update
    void Start()
    {

        rend[0] = gun[0].GetComponent<Renderer>();
        rend[1] = gun[1].GetComponent<Renderer>();
        currentMat[0] = rend[0].materials[0];
        currentMat[1] = rend[0].materials[1];
        currentMat[2] = rend[1].materials[0];
        currentMat[3] = rend[1].materials[1];
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

        if (slider.value == 1.0f)
        {
            menuOn = true;
            transform.GetChild(0).gameObject.SetActive(menuOn);
        }
        if(slider.value == 0.0f)
        {
            menuOn = false;
            transform.GetChild(0).gameObject.SetActive(menuOn);
        }


        //Check if ant placement is active
        if (placeAnt)
        {
            activateBotPlacement();
            if (OVRInput.GetDown(OVRInput.Button.Two) && antPlacementPosition.active)
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

            gun[2].GetComponent<Pulse>().isActive = true;
        }
        if(grabbedGun && !grabGun && dissolveState > 0.0f)
        {
            dissolveState -= (0.5f) * Time.deltaTime;
            for (int i = 0; i < 4; i++)
            {
                currentMat[i].SetFloat("Vector1_51085A6D", dissolveState);
            }
            if(dissolveState <= 0.0f)
            {
                gun[3].SetActive(false);
                dissolveState = 0.0f;
            }
        }
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) < 0.9f && grabbedGun)
        {
            grabbedGun = false;
            gun[2].GetComponent<Pulse>().isActive = false;
        }
        if (!grabbedGun && dissolveState < 1.0f)
        {
            dissolveState += (0.5f) * Time.deltaTime;
            for (int i = 0; i < 4; i++)
            {
                currentMat[i].SetFloat("Vector1_51085A6D", dissolveState);
            }
            if (dissolveState >= 1.0f)
            {
                dissolveState = 1.0f;
            }
        }

    }

    public void swapToBot()
    {

        closeMenu();

        //set ant placement to active 
        placeAnt = true;
        antHologramGrp.SetActive(true);
        antPlacementPosition.SetActive(true);

    }


    private void placeBot()
    {
        eventSystem.GetComponent<OVRInputModule>().rayTransform = leftBotHand.transform;
        laserPointer.GetComponent<LaserPointer>().maxLength = 10.0f;
        S5Ant.transform.GetChild(1).gameObject.SetActive(false);
        //centerEyeCamera.GetComponent<Camera>().nearClipPlane = 1.0f;
        placeAnt = true;
        antHologramGrp.SetActive(false);
        antPlacementPosition.SetActive(false);

        //OLD DELETE THESE LINES AND OBJECTS Turn on touch controllers

        Vector3 AntPos = antPlacementPosition.transform.position;
        AntPos = new Vector3(AntPos.x, AntPos.y + 1f, AntPos.z);

        //Disable Tracking on Minerva IK
        leftIK.GetComponent<InverseKinematics>().enabled = false;
        rightIK.GetComponent<InverseKinematics>().enabled = false;
        minerva.GetComponent<PositionConstraint>().enabled = false;
        minerva.transform.parent = null;

        rig.GetComponent<CharacterController>().height = 0.1f;
        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
        rig.transform.position = AntPos;//

        S5Ant.transform.localEulerAngles = new Vector3(0, 0, 0);
        leftBotIK.GetComponent<InverseKinematics>().enabled = true;
        rightBotIK.GetComponent<InverseKinematics>().enabled = true;

        S5Ant.gameObject.SetActive(true);
        S5Ant.transform.position = AntPos;
        S5Ant.transform.parent = rig.transform;
        S5Ant.GetComponent<PositionConstraint>().enabled = true;
        S5Ant.GetComponent<RotationConstraint>().enabled = true;
        //S5Ant.transform.localPosition = new Vector3(0, -0.156f, -0.163f);
        //this.transform.parent = S5Ant.transform; OLD

        //sets new height for S5Ant
        trackingSpace.transform.localPosition = new Vector3(0, -0.63f, 0);
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

            antPlacementPosition.SetActive(true);
            antLineRender.enabled = true;
            GameObject hitObject = raycastHit.transform.gameObject;
            Vector3 pos = raycastHit.point;
            pos = new Vector3(pos.x, pos.y + 0.01f, pos.z);
            //antHologram.transform.localPosition = pos;
            antPlacementPosition.transform.localPosition = pos;
            antLineRender.SetPosition(0, rightHandPos);
            antLineRender.SetPosition(1, pos);
        }
        else
        {
            antLineRender.enabled = false;
            antPlacementPosition.SetActive(false);
            //antHologram.SetActive(false);
        }
    }

    public void swapToMinerva()
    {
        eventSystem.GetComponent<OVRInputModule>().rayTransform = pointerIndex.transform;
        laserPointer.GetComponent<LaserPointer>().maxLength = 0.03f;
        S5Ant.transform.GetChild(1).gameObject.SetActive(true);
        centerEyeCamera.GetComponent<Camera>().nearClipPlane = 0.01f;

        leftBotIK.GetComponent<InverseKinematics>().enabled = false;
        rightBotIK.GetComponent<InverseKinematics>().enabled = false;

        Vector3 MinervaPos = minerva.transform.position;
        S5Ant.transform.parent = null;
        //ant.transform.parent = null;

        rig.GetComponent<CharacterController>().height = 1.1f;

        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
        rig.transform.position = MinervaPos;
        //VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = false;

        S5Ant.GetComponent<PositionConstraint>().enabled = false;
        S5Ant.GetComponent<RotationConstraint>().enabled = false;
        //ant.GetComponent<LookAtConstraint>().enabled = true;
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

        closeMenu();
        gun[3].SetActive(true);
        grabGun = true;
    }

    public void activateSonar()
    {
        closeMenu();

        if (!SonarWaveOn)
            SonarWaveOn = true;
        else
            SonarWaveOn = false;
    }

    public void closeTools()
    {

    }

    private void closeMenu()
    {
        slider.value = 0.0f;
        //Close Menu
        menuOn = !menuOn;
        transform.GetChild(0).gameObject.SetActive(menuOn);
        //Set Slider to 0
    }
}
