using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    // Update is called once per frame
    public Shader interactableShader;
    Shader defaultShader;
    Material objectMaterial;
    Material defaultObjMat;
    GameObject selectedObject;
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
                defaultObjMat = hitObject.GetComponent<MeshRenderer>().material;
                selectedObject = hitObject;
                objectMaterial = defaultObjMat;
                objectMaterial.shader = interactableShader;
                Debug.Log("Object " + hitObjectName + " was hit.");

            }
            else
            {

                
            }

        }
        else
        {
            //selectedObject.GetComponent<MeshRenderer>().material = 
            selectedObject.GetComponent<MeshRenderer>().material = defaultObjMat;

        }

        Debug.DrawLine(oculusPos, rayEndPos);

    }
}
