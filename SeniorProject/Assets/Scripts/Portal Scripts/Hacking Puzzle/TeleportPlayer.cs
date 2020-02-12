using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private IEnumerator coroutine;
    public Transform teleportTarget;
    public GameObject thePlayer;
    public GameObject VRMovement;
    public GameObject fadeObject1;
    public GameObject PostProcessingVolume;

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("test");
        if (other.tag == "Player")
        {
            VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = true;
            thePlayer.transform.position = teleportTarget.transform.position;
            Debug.Log("test");
            coroutine = WaitAndPrint(1.0f);
            StartCoroutine(coroutine);
            fadeObject1.SetActive(true);
            PostProcessingVolume.SetActive(false);
        }
    }

    private IEnumerator WaitAndPrint(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        VRMovement.GetComponent<VRMovementOculus>().minerSwitchOn = false;

    }
}
