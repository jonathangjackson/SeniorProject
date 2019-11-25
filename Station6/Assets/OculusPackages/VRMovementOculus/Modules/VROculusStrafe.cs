using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(VRMovementOculus))]
public class VROculusStrafe : MonoBehaviour
{

    VRMovementOculus refSystem;
    public bool useController = false;
    void Start()
    {
        refSystem = GetComponent<VRMovementOculus>();
        //OverRideSYstem
        refSystem.mainMovementOverRide = true;
        if(useController)
        {
            if (refSystem.ControlsOn == VRMovementOculus.eControllerType.Both)
            {
                Debug.LogError("VROculusStrafe with controller Type Both will not work as expected");
            }
        }
        
    }

    private void Update()
    {
        if (refSystem.canMove)
        {
            switch (refSystem.ControlsOn)
            {
                case VRMovementOculus.eControllerType.Left:
                    FPSMove(refSystem.defaultLeft);
                    break;
                case VRMovementOculus.eControllerType.Right:
                    FPSMove(refSystem.defaultRight);
                    break;
                case VRMovementOculus.eControllerType.Both:
                    FPSMove(refSystem.defaultLeft);
                    FPSMove(refSystem.defaultRight);
                    break;
                default:
                    break;
            }

        }
        
    }

    void FPSMove(OVRInput.Controller pickedController)
    {
        float h = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, pickedController).x;
        float v = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick, pickedController).y;
        Vector3 moveDirection = new Vector3(h,0,v);
        moveDirection *= refSystem.moveSpeed * Time.deltaTime;
        if(useController)
        {
            if(refSystem.ControlsOn == VRMovementOculus.eControllerType.Left)
            {
                moveDirection = refSystem.leftController.transform.TransformDirection(moveDirection);
            }
            else
            {
                moveDirection = refSystem.rightController.transform.TransformDirection(moveDirection);
            }
        }
        else
        {
            moveDirection = refSystem.headRig.transform.TransformDirection(moveDirection);
        }

        // moveDirection = refSystem.headRig.transform.TransformDirection(moveDirection);
        if (refSystem.MovementMode == VRMovementOculus.eMovementMode.Grounded)
        {
            moveDirection.y = 0;
            refSystem.ApplyGravity();
        }
        refSystem.yourRig.Move(moveDirection);
    }
}
