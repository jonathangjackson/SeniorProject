using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AntDistance : MonoBehaviour
{
    public GameObject VRcamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Text>().text = ((int)Vector3.Distance(this.transform.position, VRcamera.transform.position)).ToString() + "m";
    }
}
