using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public GameObject pulseObj;
    public float destroyTime = 1.0f;
    public bool isActive = false;
    public Vector3 rotate = new Vector3(0,0,0);
    public ParticleSystem fire;
    private IEnumerator coroutine;
    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Two) && isActive)
        {
            this.transform.parent.GetComponent<Animator>().SetBool("Play", true);
            fire.Play();
            GameObject pulseClone = Instantiate(pulseObj, this.transform.position, this.transform.localRotation);//Quaternion.Euler(this.transform.localEulerAngles.x, this.transform.localEulerAngles.x + 90, this.transform.localEulerAngles.z)
            pulseClone.transform.parent = gameObject.transform;
            pulseClone.transform.localEulerAngles = rotate;
            pulseClone.transform.parent = null;
            OVRInput.SetControllerVibration(0.5f, 0.5f, OVRInput.Controller.RTouch);
            coroutine = StopParticle(0.5f);
            StartCoroutine(coroutine);
            //pulseClone.GetComponent<ConstantForce>().force = new Vector3(speed, 0, 0);
            Destroy(pulseClone, destroyTime);
        }
    }

    private IEnumerator StopParticle(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        this.transform.parent.GetComponent<Animator>().SetBool("Play", false);
        fire.Stop();
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
    }
}
