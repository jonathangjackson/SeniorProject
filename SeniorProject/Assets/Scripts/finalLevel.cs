using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalLevel : MonoBehaviour
{
    int grabTimes = 0;
    public OVRGrabbable grabbedObject;
    public GeneratorButton2 lab2;
    public GeneratorButton3 lab3;
    public GameObject hackingWorldObj;
    public GameObject doorLocked;
    public GameObject avium;
    public bool aviumAcquired;
    public GameObject debris;
    public AudioSource Audio;
    public GameObject Pod;
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        Audio.GetComponent<AudioClip>();
        aviumAcquired = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (grabbedObject.isGrabbed == true)
        {
            if(grabTimes == 0)
            {
                lab2.reset();
                lab3.reset();
                doorLocked.GetComponent<Animator>().SetBool("Locked", true);
                Audio.Play();
                grabTimes = 1;
                hackingWorldObj.SetActive(true);
                debris.SetActive(true);
                avium.SetActive(false);
                aviumAcquired = true;
                Pod.GetComponent<ControlAnimations>().enabled = true;
                Player.transform.parent = Pod.transform;
            }
        }
    }
}
