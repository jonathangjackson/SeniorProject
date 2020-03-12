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
    private float maximumScaleX = 0.0005857561f;
    private float maximumScaleY = 0.0003514537f;
    private float maximumScaleZ = 0.0003514537f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

        meter.text = ((int)Vector3.Distance(target.position, VRcamera.transform.position)).ToString() + "m";
        canvas.transform.LookAt(VRcamera.transform.position + VRcamera.transform.rotation * Vector3.back, VRcamera.transform.rotation * Vector3.up);
        canvas.transform.Rotate(0, 180, 0);

        canvas.transform.localScale = Vector3.Distance(VRcamera.transform.position, canvas.transform.position) * ScaleFactor;

        if ((canvas.transform.localScale.x <= maximumScaleX) && (canvas.transform.localScale.y <= 0.0003514537) && (canvas.transform.localScale.z <= 0.0003514537))
        {
            canvas.transform.localScale = new Vector3(maximumScaleX, maximumScaleY, maximumScaleZ);
        }

        canvas.transform.position = (target.position + offset);

    }
}
