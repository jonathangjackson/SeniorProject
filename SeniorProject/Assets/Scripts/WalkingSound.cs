using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;
using OVR;
using UnityEngine.EventSystems;

public class WalkingSound : MonoBehaviour
{
    public AudioSource footsteps;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) >= 0.9f || OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) >= 0.9f)
        {
            footsteps.volume = 1.0f;
        }

        if (OVRInput.Get(OVRInput.Axis1D.SecondaryHandTrigger) == 0.0f && OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger) == 0.0f)
        {
            footsteps.volume = 0.0f;
        }
    }

}