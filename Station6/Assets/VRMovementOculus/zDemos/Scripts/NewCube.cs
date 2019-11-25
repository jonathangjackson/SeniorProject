using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCube : MonoBehaviour
{
    public KillCube bS;

    public bool crateBool;

    public GameObject newCube;

    // Start is called before the first frame update
    void Start()
    {
        newCube.transform.gameObject.SetActive(false);
        //newCube.GetComponent<MeshRenderer>().enabled = false;
        crateBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bS.isOpen)
        {
            crateBool = true;
            newCube.transform.gameObject.SetActive(true);
            //newCube.GetComponent<MeshRenderer>().enabled = true;
            
        }
    }
}
