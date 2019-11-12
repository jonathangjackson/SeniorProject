using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.XR;
public class VROculusRotate : MonoBehaviour
{

    public bool canRotate = true;
    [Header("-Handness-")]
    public VRMovementOculus.eControllerType RotateOnHand;
    public bool copyHandFromMain = true;
    public bool invertHand = false;
    public enum eRotationMode { ButtonPointAndShoot, QuickStick, DoubleTapQuickStick, SlowStick, ButtonRotateLR, TouchPadClick, TouchPadSwipe, None };
    [Header("-Rotate Modes-")]
    public eRotationMode RotationMode = eRotationMode.QuickStick;
    [Header("-Rotate Controls-")]
    public OVRInput.Button RotateButton;
    [Header("-Rotate Settings-")]
    public bool fadeRotate = false;
    [Range(0, 180)]
    public float rotateDegrees = 45;
    public float rotateTime = 0;        //ButtonRotation
    public float slowRotateSpeed = .7f;   //Speed for Stick Rotate
    public float doubleTapTime = .3f;
    VRMovementOculus refSystem;
    //Double Tap Vars
    bool doubleTapLeft;
    bool doubleTapRight;
    float dTapTimeLeft;
    float dTapTimeRight;

    void Start()
    {
        refSystem = GetComponent<VRMovementOculus>();
    }

    void Update()
    {
        if(copyHandFromMain)
        {
            if(invertHand)
            {
                if(refSystem.ControlsOn == VRMovementOculus.eControllerType.Left)
                {
                    RotateOnHand = VRMovementOculus.eControllerType.Right;
                }
                else
                {
                    RotateOnHand = VRMovementOculus.eControllerType.Left;
                }
            }
            else
            {
                RotateOnHand = refSystem.ControlsOn;
            }

        }
        if (canRotate)
        {
            if (RotationMode == eRotationMode.ButtonPointAndShoot)
            {
                PointShootPressed();
            }
            if (RotationMode == eRotationMode.QuickStick)
            {
                QuickStickRotate();
            }
            if (RotationMode == eRotationMode.DoubleTapQuickStick)
            {
                DoubleTapQuickStick();
            }
            if (RotationMode == eRotationMode.SlowStick)
            {
                SlowStickRotate();
            }
            if (RotationMode == eRotationMode.ButtonRotateLR)
            {
                ButtonRotateLR();
            }
            if (RotationMode == eRotationMode.TouchPadClick)
            {
                TouchPadMove();
            }
            if (RotationMode == eRotationMode.TouchPadSwipe)
            {
                QuickStickRotate();
            }
        }
    }

    void TouchPadMove()
    {
        Vector2 curTouchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, refSystem.defaultLeft);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad, refSystem.defaultLeft))
        {
            if (curTouchPad.x >= .2f)
            {
                RotateByDegrees(-rotateDegrees);
            }
            if (curTouchPad.x <= -.2f)
            {
                RotateByDegrees(rotateDegrees);
            }
        }
        curTouchPad = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad, refSystem.defaultRight);
        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad, refSystem.defaultRight))
        {
            //Left!
            if (curTouchPad.x <= -.2f)
            {
                RotateByDegrees(rotateDegrees);
            }
            //Right!
            if (curTouchPad.x >= .2f)
            {
                RotateByDegrees(rotateDegrees);
            }

        }
    }


    void ButtonRotateLR()
    {
        if (OVRInput.GetDown(RotateButton, refSystem.defaultLeft))
        {
            RotateByDegrees(-rotateDegrees);
        }
        if (OVRInput.GetDown(RotateButton, refSystem.defaultRight))
        {
            RotateByDegrees(rotateDegrees);
        }
    }

    //Slowly Rotates the Charactor Controller
    void SlowStickRotate()
    {
        float h = 0;
        switch (RotateOnHand)
        {
            case VRMovementOculus.eControllerType.Left:
                h = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, refSystem.defaultLeft).x;
                break;
            case VRMovementOculus.eControllerType.Right:
                h = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, refSystem.defaultRight).x;
                break;
            case VRMovementOculus.eControllerType.Both:
                h = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, refSystem.defaultLeft).x;
                h += OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, refSystem.defaultRight).x;
                h = Mathf.Clamp(h, -1, 1);
                break;

        }
        if (Mathf.Abs(h) > .1f)
        {
            RotateByDegrees(h * slowRotateSpeed);
        }
    }
    //Quick 45 Degree Rotations for the Charactor Controller
    void QuickStickRotate()
    {
        switch (RotateOnHand)
        {
            case VRMovementOculus.eControllerType.Left:
                QuickStickResolve(refSystem.defaultLeft);
                break;
            case VRMovementOculus.eControllerType.Right:
                QuickStickResolve(refSystem.defaultRight);
                break;
            case VRMovementOculus.eControllerType.Both:
                QuickStickResolve(refSystem.defaultLeft);
                QuickStickResolve(refSystem.defaultRight);
                break;

        }
    }

    void QuickStickResolve(OVRInput.Controller selectedController)
    {
        if (OVRInput.GetDown(OVRInput.Button.Left, selectedController))
        {
            RotateByDegrees(-rotateDegrees);
        }
        if (OVRInput.GetDown(OVRInput.Button.Right, selectedController))
        {
            RotateByDegrees(rotateDegrees);
        }
    }

    void DoubleTapQuickStick()
    {
        switch (refSystem.ControlsOn)
        {
            case VRMovementOculus.eControllerType.Left:
                DoubleTapResolve(refSystem.defaultLeft);
                break;
            case VRMovementOculus.eControllerType.Right:
                DoubleTapResolve(refSystem.defaultRight);
                break;
            case VRMovementOculus.eControllerType.Both:
                DoubleTapResolve(refSystem.defaultLeft);
                DoubleTapResolve(refSystem.defaultRight);
                break;

        }
    }

    void DoubleTapResolve(OVRInput.Controller selectedController)
    {
        doubleTapLeft = false;
        doubleTapRight = false;
        if (OVRInput.GetDown(OVRInput.Button.Left, selectedController))
        {
            if (Time.time < dTapTimeLeft + doubleTapTime)
            {
                doubleTapLeft = true;
            }
            dTapTimeLeft = Time.time;
        }
        if (doubleTapLeft)
        {
            RotateByDegrees(-rotateDegrees);
        }
        if (OVRInput.GetDown(OVRInput.Button.Right, selectedController))
        {
            if (Time.time < dTapTimeRight + doubleTapTime)
            {
                doubleTapRight = true;
            }
            dTapTimeRight = Time.time;
        }
        if (doubleTapRight)
        {
            RotateByDegrees(rotateDegrees);
        }
    }
    //Point Shoot Pressed Event
    void PointShootPressed()
    {
        VRMovementOculus.InputData InputHolder = refSystem.InputReturnDown(RotateButton);
        if (InputHolder.isRight == true)
        {
            PointAndShootRotation(refSystem.rightController);
        }
        if (InputHolder.isLeft == true)
        {
            PointAndShootRotation(refSystem.leftController);
        }
    }

    /// <summary>
    /// Point and Shoot Rotation ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    ///  Changes the Rotation based on where the Controller is pointing and where you are looking only only in one direction. This allows someone to quickly look behind them.
    /// </summary>

    void PointAndShootRotation(Transform selectedController)
    {
        if (fadeRotate)
        {
            refSystem.myFade.StartFadeIn(refSystem.fadeTime);
        }
        //Get Position in front of Object
        Vector3 holder = new Ray(selectedController.position, selectedController.forward).GetPoint(1);
        //Get Rotational Direction
        Vector3 lookPos = holder - refSystem.headRig.transform.position;
        //Remove Y Component
        lookPos.y = 0;
        //Transform rotation
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        //Get Rotational Direction
        Vector3 rotPosition = rotation.eulerAngles - refSystem.transform.localRotation.eulerAngles;
        //Flatten Rotational Return
        rotPosition.x = 0;
        rotPosition.z = 0;
        //Apply Rotation
        refSystem.yourRig.transform.DORotate(rotPosition, rotateTime);
    }

    //Helper Function To rotate by set Degrees
    public void RotateByDegrees(float degrees)
    {
        RotateByDegrees2(degrees);
        //if (refSystem.myFade && fadeRotate)
        //{
        //    refSystem.myFade.StartFadeIn(refSystem.fadeTime);
        //}
        //Vector3 holder1;
        //holder1 = refSystem.yourRig.transform.rotation.eulerAngles;
        //holder1.y += degrees;
        //Vector3 rotPosition = holder1;
        //refSystem.yourRig.transform.DORotate(rotPosition, rotateTime);
    }

    void RotateByDegrees2(float degrees)
    {
        InputTracking.Recenter();
        if (refSystem.myFade && fadeRotate)
        {
            refSystem.myFade.StartFadeIn(refSystem.fadeTime);
        }
        //Transform mc =//LogsUtil.getMainCamera();
        Vector3 cameraPos = transform.TransformPoint(refSystem.headRig.position);
        Vector3 holder1;
        holder1 = refSystem.yourRig.transform.rotation.eulerAngles;
        holder1.y += degrees;
        refSystem.yourRig.transform.DORotate(holder1, rotateTime);
        //refSystem.yourRig.transform.eulerAngles = holder1;
        Vector3 newCameraPos = transform.TransformPoint((refSystem.headRig.position));

        if (refSystem.oculusType != VRMovementOculus.eHMD.Go)
        {
            Vector3 cameraDifference = newCameraPos - cameraPos;
            refSystem.yourRig.transform.position = refSystem.yourRig.transform.position - cameraDifference;
        }
        //Debug.Log("now cameraPos=" + cameraPos);
    }
}

