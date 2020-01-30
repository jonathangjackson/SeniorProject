using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(VRMovementOculus))]
public class VROculusDragMovement : MonoBehaviour
{

    [Header("-Drag Visual-")]
    public Transform DragVisual; // Used if you wish a visual where you started Dragging from
    [Header("-Settings-")]
    [Range(.1f, 50)]
    public float multipler = 1; //Increases the magnitude of your grab 
    public bool invertControls; //Inverts Direction
    Vector3 lastPos;       //Direction Holder
    bool isOn;              //Toggles Between Hands
    VRMovementOculus refSystem; //REf Holder
    Transform storedTransform; //Stores current Controller;
    void Start()
    {

        //Get Ref
        refSystem = GetComponent<VRMovementOculus>();
        refSystem.mainMovementOverRide = true;

        //Hide Drag Visual
        if (DragVisual)
        {
            DragVisual.gameObject.SetActive(false);
        }
    }

    //// Update is called once per frame
    void Update()
    {
        //Checks to siee if CanMove
        if (refSystem.canMove)
            {
                DraggingMove();
            }
        //Apply Gravity if Grounded;
        refSystem.ApplyGravity();
    }

    //public void ApplyGravity()
    //{
    //    //Check to see if Grounded
    //    if(!refSystem.yourRig.isGrounded)
    //    {
    //        //Apply Gravity in Grounded Move Mode
    //        if (refSystem.MovementMode == VRTouchMove2.eMovementMode.Grounded)
    //        {
    //            Vector3 holder = Vector3.zero;
    //            holder.y -= refSystem.PlayerGravity * Time.deltaTime;
    //            refSystem.yourRig.Move(holder);

    //        }
    //    }
    //}
    //Main Draging Function
    public void DraggingMove()
    {
        //Get Inputs
        VRMovementOculus.InputData InputHolderDown = refSystem.InputReturnDown(refSystem.ForwardButton);
        VRMovementOculus.InputData InputHolderUp = refSystem.InputReturnUp(refSystem.ForwardButton);

        if (InputHolderDown.pressed)
        {
            //Store Pressed
            storedTransform = InputHolderDown.selectedController;
            lastPos = storedTransform.position;
            isOn = true;
            if (DragVisual)
            {
                DragVisual.gameObject.SetActive(true);
                DragVisual.transform.position = storedTransform.position;
            }
        }

        if (isOn)
        {
            Vector3 holder = storedTransform.position - lastPos;
            //Invert Controls and Direction
            if (!invertControls)
            {
                holder = holder * ((-100 * multipler)) * Time.deltaTime;

            }
            else
            {
                holder = holder * ((100 * multipler)) * Time.deltaTime;
            }
            //If Grounded Give zero YAxis
            if(refSystem.MovementMode == VRMovementOculus.eMovementMode.Grounded)
            {
                holder.y = 0;
            }
            refSystem.yourRig.Move(holder);
            lastPos = storedTransform.position;
            if (InputHolderUp.pressed)
            {
                if (InputHolderUp.selectedController == storedTransform)
                {
                    isOn = false;
                    if (DragVisual)
                    {
                        DragVisual.gameObject.SetActive(false);
                    }

                }
            }
        }
    }
}
