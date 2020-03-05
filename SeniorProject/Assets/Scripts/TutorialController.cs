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
    public TriggerType triggerType;
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
        /*
        switch (triggerType)
        {
            case TriggerType.Controller:
                controllerButtonTrigger();
                break;
            case TriggerType.UIButton:
                uiButtonTrigger();
                break;
        }*/
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
        triggerActivated = true;
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
