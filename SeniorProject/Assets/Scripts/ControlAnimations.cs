using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimations : MonoBehaviour
{
    public Animator animatorController;
    public bool triggerCollision = false;
    public bool externalCollision = false;
    public bool lockOff = false;
    public AudioSource doorSound;
    public AudioClip finalLevel;
    public string boolName;

    // Start is called before the first frame update
    void Start()
    {
        if (animatorController == null && triggerCollision)
            animatorController = this.GetComponent<Animator>();
        if (lockOff)
            animatorController.SetBool("Locked", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && triggerCollision && this.enabled && !animatorController.GetBool("Locked"))
        {
            animatorController.SetBool(boolName, true);
            doorSound.Play();
        }
        if (triggerCollision && boolName == "Final" && this.enabled)
        {
            doorSound.clip = finalLevel;
            doorSound.Play();
            triggerCollision = false;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && triggerCollision && this.enabled && !animatorController.GetBool("Locked"))
        {
            animatorController.SetBool(boolName, false);
        }
    }

    private void Update()
    {
        if (externalCollision)
        {
            animatorController.SetBool(boolName, false);
        }
        
    }
}
