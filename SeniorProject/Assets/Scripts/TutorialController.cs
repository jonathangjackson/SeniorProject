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

    public bool triggerActivated = false;
    public int hologramAnimPosition;
    public GameObject hologram;
    public AudioClip audioClip;
    public List<SequentialTutorialTrigger> sequentialTriggers;
    public bool hasHighlights = false;
    public List<GameObject> highlightObjects;
    public Material highlightMaterial;
    public Animator animationController;

    private List<Material> originalMaterial = new List<Material>();

    void Start()
    {
        hologram.GetComponent<Animator>().SetInteger("Tutorial", hologramAnimPosition);
        if (hasHighlights)
        {
            storeMaterials();
        }
    }

    void Update()
    {
        if (sequentialTriggers[0].triggerActivated)
        {
            if (sequentialTriggers.Count == 1)
            {
                triggerActivated = true;

            }
            else
            {
                sequentialTriggers.RemoveAt(0);
                Debug.Log("Sequential Trigger Type = " + sequentialTriggers[0].triggerType.ToString()); 
                if (sequentialTriggers[0].triggerType.ToString() == "Animation")
                {
                    sequentialTriggers[0].animationTrigger();
                }
                //sequentialTriggers[0].gameObject.SetActive(true);
            }

        }
    }
    
    public void setChildActive(int pos)
    {
        if(this.transform.childCount > 0)
            this.transform.GetChild(pos).gameObject.SetActive(true);
    }

    public void startAnimation()
    {
        animationController.SetBool("Locked", false);
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
