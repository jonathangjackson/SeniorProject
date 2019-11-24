using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective1 : MonoBehaviour
{
    private ObjectiveManager objManager;

    // Start is called before the first frame update
    void Start()
    {
        objManager = GameObject.Find("PreparedOVRCameraRig").GetComponent<ObjectiveManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            Destroy(gameObject);
            objManager.objective1 = true;
            objManager.objective2 = false;
            objManager.objective3 = false;
        }
    }
}
