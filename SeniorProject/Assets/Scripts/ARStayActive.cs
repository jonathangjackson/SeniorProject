using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARStayActive : MonoBehaviour
{
    private IEnumerator coroutine;
    private bool playerTrigger = false;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "AR" && !playerTrigger)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            coroutine = SetActiveStatus(1.0f, false);
            StartCoroutine(coroutine);
        }
        if(other.tag == "Player")
        {
            playerTrigger = true;
            this.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Player")
        {
            playerTrigger = false;
            this.transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    private IEnumerator SetActiveStatus(float waitTime, bool active)
    {
        yield return new WaitForSeconds(waitTime);
        if(!playerTrigger)
            this.transform.GetChild(0).gameObject.SetActive(active);
        
    }
}
