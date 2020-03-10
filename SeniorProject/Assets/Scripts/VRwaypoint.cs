using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class VRwaypoint : MonoBehaviour
{
    public Camera VRcamera;
    public Image image;
    public Transform target;
    public Text meter;
    public Canvas canvas;

    public Vector3 ScaleFactor;
    public Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        meter.text = ((int)Vector3.Distance(target.position, VRcamera.transform.position)).ToString() + "m";
        canvas.transform.LookAt(VRcamera.transform.position + VRcamera.transform.rotation * Vector3.forward, VRcamera.transform.rotation * Vector3.up);
        canvas.transform.Rotate(0, 180, 0);

        canvas.transform.localScale = Vector3.Distance(VRcamera.transform.position, canvas.transform.position) * ScaleFactor;

        canvas.transform.position = (target.position + offset);

    }
}
