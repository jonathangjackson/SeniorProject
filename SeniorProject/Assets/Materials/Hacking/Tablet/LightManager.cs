using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    public Material lightEmissionMat;
    public ParticleSystem binaryParticles;
    //public Light spotLight;

    public void lightOff()
    {
        lightEmissionMat.DisableKeyword("_EMISSION");
        binaryParticles.Clear();
        binaryParticles.Stop();
        //spotLight.enabled = false;
    }

    public void lightOn()
    {
        lightEmissionMat.EnableKeyword("_EMISSION");
        binaryParticles.Play(true);
        //spotLight.enabled = true;
    }
}
