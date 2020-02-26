using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARWave : MonoBehaviour
{
    private IEnumerator coroutine;
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if(other.tag == "AR")
        {
            Debug.Log("HIT");
            other.transform.GetChild(0).gameObject.SetActive(true);
            coroutine = SetActiveStatus(1.0f, other.gameObject, false);
            StartCoroutine(coroutine);
        }
    }

    private IEnumerator SetActiveStatus(float waitTime, GameObject obj, bool active)
    {
        yield return new WaitForSeconds(waitTime);
        obj.transform.GetChild(0).gameObject.SetActive(active);
    }
}
