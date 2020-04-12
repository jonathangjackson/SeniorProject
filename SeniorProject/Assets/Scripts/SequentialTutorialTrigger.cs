using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequentialTutorialTrigger : MonoBehaviour
{
    public enum TriggerType
    {
        Collision,
        Controller,
        UIButton,
        Animation
    }
    public enum ButtonType
    {
        x,
        y,
        a,
        b,
        lIndex,
        lMiddle,
        rIndex,
        rMiddle
    }

    public ButtonType buttonType;
    public TriggerType triggerType;
    public string collisionTag;
    public bool triggerActivated = false;
    public Animator animation;

    public void Update()
    {
        switch (triggerType)
        {
            case TriggerType.Controller:
                controllerButtonTrigger();
                break;
        }

        if (Input.GetKeyDown("space"))
        {
            triggerActivated = true;
        }
    }

    public void setActive()
    {
        this.gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ARROW COLLISION?");
        if(triggerType == TriggerType.Collision)
        {
            if (other.tag == collisionTag)
                triggerActivated = true;
        }
    }


    public void animationTrigger()
    {
        animation.SetBool("Play", true);
    }

    public void callAnimTrigger()
    {
        triggerActivated = true;
    }

    private void controllerButtonTrigger()
    {
        switch (buttonType)
        {
            case ButtonType.y:
                if (OVRInput.GetDown(OVRInput.Button.Four))
                    triggerActivated = true;
                break;
            case ButtonType.x:
                if (OVRInput.GetDown(OVRInput.Button.Three))
                    triggerActivated = true;
                break;
            case ButtonType.a:
                if (OVRInput.GetDown(OVRInput.Button.One))
                    triggerActivated = true;
                break;
            case ButtonType.b:
                if (OVRInput.GetDown(OVRInput.Button.Two))
                    triggerActivated = true;
                break;
            case ButtonType.lIndex:
                if (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger) > 0.9f)
                    triggerActivated = true;
                break;
            case ButtonType.rIndex:
                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger) > 0.9f)
                    triggerActivated = true;
                break;
            case ButtonType.lMiddle:
                if (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger) > 0.9f)
                    triggerActivated = true;
                break;
            case ButtonType.rMiddle:
                if (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger) > 0.9f)
                    triggerActivated = true;
                break;
        }
    }

    public void uiButtonTrigger()
    {
        triggerActivated = true;
    }

    public void setObjectActive(bool val)
    {
        //this.gameObject.SetActive(val);
    }

}
