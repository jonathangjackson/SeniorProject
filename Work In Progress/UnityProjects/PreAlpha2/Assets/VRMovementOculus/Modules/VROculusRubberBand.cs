using UnityEngine;
using System.Collections;
[RequireComponent(typeof(VRMovementOculus))]
public class VROculusRubberBand : MonoBehaviour {

    [Header("-Drag Points-")]
    public Transform DragPointL;              // Drag Point for RubberBand Movement Must be Parented to OVRCamerRig
    public Transform DragPointR;              // Drag Point for RubberBand Movement Must be Parented to OVRCamerRig
    [Header("-RubberBand Lines-")]
    public LineArcSystem RubberBandLineL;
    public LineArcSystem RubberBandLineR;
    [Header("-Settings-")]
    public float maxRadius = .2f;
    public bool showLines = true;
    bool rbLeftOn;
    bool rbRightOn;
    public bool invertDirection;
    VRMovementOculus refSystem;

    void Start()
    {
        refSystem = GetComponent<VRMovementOculus>();
        //OverRideSYstem
        refSystem.mainMovementOverRide = true;
        //ParentObjects just in case you forgot
        DragPointL.parent = refSystem.yourRig.transform;
        DragPointR.parent = refSystem.yourRig.transform;
        RubberBandLineL.transform.parent = refSystem.yourRig.transform;
        RubberBandLineR.transform.parent = refSystem.yourRig.transform;
        //Hide Drag Points
        DragPointL.gameObject.SetActive(false);
        DragPointR.gameObject.SetActive(false);
    }

    //RubberBandControlSystems;
    void Update()
    {
        if(refSystem.canMove)
        {
            //Rubberband needs to be seporated into either hand
            switch (refSystem.ControlsOn)
            {
                case VRMovementOculus.eControllerType.Left:
                    RubberBandMoveL();
                    break;
                case VRMovementOculus.eControllerType.Right:
                    RubberBandMoveR();
                    break;
                case VRMovementOculus.eControllerType.Both:
                    RubberBandMoveL();
                    RubberBandMoveR();
                    break;
                default:
                    break;
            }

        }
        refSystem.ApplyGravity();
    }

    //Apply Gravity if Requested
   

    //Late Update is used to show lines and prevent Jittering
    void LateUpdate()
    {
        if(!showLines)
        {
            return;
        }
        if (rbLeftOn && RubberBandLineL)
        {
            RubberBandLineL.CreateLine(DragPointL.position, refSystem.leftController.position, Color.blue);
        }
        if (rbRightOn && RubberBandLineR)
        {
            RubberBandLineR.CreateLine(DragPointR.position, refSystem.rightController.position, Color.blue);
        }
    }

    //Left Rubberband Movement
    public void RubberBandMoveL()
    {
        //Gather Inputs
        VRMovementOculus.InputData InputHolderDown = refSystem.InputReturnDown(refSystem.ForwardButton);
        VRMovementOculus.InputData InputHolderUp = refSystem.InputReturnUp(refSystem.ForwardButton);

        if(InputHolderDown.pressed && InputHolderDown.isLeft)
        {
            DragPointL.gameObject.SetActive(true);
            DragPointL.position = refSystem.leftController.position;
            rbLeftOn = true;
        }

        if(rbLeftOn)
        {
            Vector3 holder = refSystem.leftController.position - DragPointL.position;
            float distance = Vector3.Distance(refSystem.leftController.position, DragPointL.position);            distance = Mathf.Clamp(distance, -maxRadius/2, maxRadius/2);            distance = ConvertRange(0, maxRadius / 2, 0, 1, distance);
            //Multiply By Distance
            holder = holder.normalized * distance;
            if (invertDirection)
            {
                holder = holder * (-refSystem.moveSpeed / 2) * Time.deltaTime;
            }
            else
            {
                holder = holder * (refSystem.moveSpeed / 2) * Time.deltaTime;
            }

            if(refSystem.MovementMode == VRMovementOculus.eMovementMode.Grounded)
            {
                holder.y = 0;
            }
            refSystem.yourRig.Move(holder);

            if (InputHolderUp.pressed && InputHolderUp.isLeft)
            {
                rbLeftOn = false;
                RubberBandLineL.HideLine();
                DragPointL.gameObject.SetActive(false);
            }
        }
    }
    
    //Right Rubberband Movement
    public void RubberBandMoveR()
    {
        //GatherInputs
        VRMovementOculus.InputData InputHolderDown = refSystem.InputReturnDown(refSystem.ForwardButton);
        VRMovementOculus.InputData InputHolderUp = refSystem.InputReturnUp(refSystem.ForwardButton);

        if (InputHolderDown.pressed && InputHolderDown.isRight)
        {
            DragPointR.gameObject.SetActive(true);
            DragPointR.position = refSystem.rightController.position;
            rbRightOn = true;
        }

        if (rbRightOn)
        {
            Vector3 holder = refSystem.rightController.position - DragPointR.position;
            float distance = Vector3.Distance(refSystem.rightController.position, DragPointR.position);            distance = Mathf.Clamp(distance, -maxRadius / 2, maxRadius / 2);            distance = ConvertRange(0, maxRadius / 2, 0, 1, distance);
            //Multiply By Distance
            holder = holder.normalized * distance;
            if (invertDirection)
            {
                holder = holder * (-refSystem.moveSpeed / 2) * Time.deltaTime;
            }
            else
            {
                holder = holder * (refSystem.moveSpeed / 2) * Time.deltaTime;
            }
            if (refSystem.MovementMode == VRMovementOculus.eMovementMode.Grounded)
            {
                holder.y = 0;
            }
            refSystem.yourRig.Move(holder);

            if (InputHolderUp.pressed && InputHolderUp.isRight)
            {
                rbRightOn = false;
                RubberBandLineR.HideLine();
                DragPointR.gameObject.SetActive(false);
            }
        }
    }

    //Convert Range Helper
    public float ConvertRange(           float originalStart, float originalEnd,           float newStart, float newEnd,           float value)    {        float originalDiff = originalEnd - originalStart;        float newDiff = newEnd - newStart;        float ratio = newDiff / originalDiff;        float newProduct = value * ratio;        float finalValue = newProduct + newStart;        return finalValue;    }
}
