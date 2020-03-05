using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
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

    public TriggerType triggerType;
    public ButtonType buttonType;
    public AudioClip audioClip;
    public bool hasHighlights = false;
    public List<GameObject> highlightObjects;
    public bool triggerActivated = false;
    public Material highlightMaterial;

    private List<Material> originalMaterial = new List<Material>();

    void Start()
    {
        if (hasHighlights)
        {
            storeMaterials();
        }
    }

    void Update()
    {
        
        switch (triggerType)
        {
            case TriggerType.Controller:
                controllerButtonTrigger();
                break;
        }
    }

    public void setChildActive(int pos)
    {
        if(this.transform.childCount > 0)
            this.transform.GetChild(pos).gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "tutorial" && triggerType == TriggerType.Collision)
        {
            triggerActivated = true;
        }
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
        }
    }

    public void uiButtonTrigger()
    {
        Debug.Log("TRIGGER UI ACTIVATED");
        triggerActivated = true;
    }

    public AudioClip getAudioClip()
    {
        return audioClip;
    }


    private void storeMaterials()
    {
        for(int i = 0; i < highlightObjects.Count; i++)
        {
            Material mat = highlightObjects[i].GetComponent<Renderer>().material;
            originalMaterial.Add(mat);
        }
    }

    public void activateHighlights()
    {
        if (highlightObjects == null || !hasHighlights)
            return;
        //Set Material to Highlight Material
        for(int i = 0; i < highlightObjects.Count; i++)
        {
            highlightObjects[i].GetComponent<Renderer>().material = highlightMaterial;
        }
    }

    public void deactivateHighlight()
    {
        if (highlightObjects == null || !hasHighlights)
            return;
        //Set Material to Old Material
        for (int i = 0; i < highlightObjects.Count; i++)
        {
            highlightObjects[i].GetComponent<Renderer>().material = originalMaterial[i];
        }
    }

    public void destroyTutorial()
    {
        if (hasHighlights)
        {
            deactivateHighlight();
        }
        this.gameObject.SetActive(false);
    }
}
