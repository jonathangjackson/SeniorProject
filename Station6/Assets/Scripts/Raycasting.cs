using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycasting : MonoBehaviour
{
    // Update is called once per frame
    //public Shader interactableShader;
    //Shader defaultShader;
    Material objectMaterial;
    public Material highlightMat;
    //Material defaultObjMat;
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
                if(hitObject != selectedObject)
                {
                    //Material highlight = new Material(hitObject.GetComponent<Renderer>().material);
                    //highlight.SetColor("_EmissionColor", Color.yellow);
                    //hitObject.GetComponent<Renderer>().material = highlight;
                    //Renderer objectRenderer = hitObject.GetComponent<Renderer>();
                    //objectRenderer.material.SetColor("_EmissionColor", Color.yellow);
                    objectMaterial = hitObject.GetComponent<Renderer>().material;
                    hitObject.GetComponent<Renderer>().material = highlightMat;
                    
                    //Debug.Log("Object " + hitObjectName + " was hit.");
                    selectedObject = hitObject;
                    Debug.Log("Object " + selectedObject.name + " was hit.");

                }

                //defaultObjMat = hitObject.GetComponent<MeshRenderer>().material;
                //objectMaterial = defaultObjMat;
                //objectMaterial.shader = interactableShader;

            }
            else
            {

                
            }

        }
        else
        {
            if(selectedObject != null)
            {
                Debug.Log("No Object being hit");
                selectedObject.GetComponent<Renderer>().material = objectMaterial;
                selectedObject = null;


            }
            //selectedObject.GetComponent<MeshRenderer>().material = 
            //selectedObject.GetComponent<MeshRenderer>().material = defaultObjMat;

        }

        Debug.DrawLine(oculusPos, rayEndPos);

    }
}
