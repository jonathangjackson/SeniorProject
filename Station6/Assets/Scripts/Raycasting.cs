using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    // Update is called once per frame
    Material objectMaterial;
    void Update()
    {
        RaycastFromOculus();
    }

    void RaycastFromOculus()
    {

        float rayLength = 5.0f;

        Vector3 oculusPos = transform.position;
        Vector3 rayDirection = transform.forward;
        //Debug.Log(transform.forward);
        Ray rayscastRay = new Ray(oculusPos, rayDirection);
        RaycastHit raycastHit;

        Vector3 rayEndPos = oculusPos + rayDirection * rayLength;

        bool objectHit = Physics.Raycast(rayscastRay, out raycastHit, rayLength);

        if (objectHit)
        {
            GameObject hitObject = raycastHit.transform.gameObject;
            string hitObjectName = hitObject.name;

            if (hitObject.tag == "Interactable")
            {
                objectMaterial = hitObject.GetComponent<MeshRenderer>().material;
                Debug.Log("Object " + hitObjectName + " was hit.");

            }

        }

        Debug.DrawLine(oculusPos, rayEndPos);

    }
}
