using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewPowerCell : MonoBehaviour
{
    public killPowerCell Powerbool;

    //public bool crateBool;

    public GameObject newPowerCell, doorLeft, doorRight;

    // Start is called before the first frame update
    void Start()
    {
        newPowerCell.transform.gameObject.SetActive(false);
        //newCube.GetComponent<MeshRenderer>().enabled = false;
        //crateBool = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Powerbool.hackingSuccess)
        {
            //crateBool = true;
            newPowerCell.transform.gameObject.SetActive(true);
            doorLeft.transform.gameObject.SetActive(false);
            doorRight.transform.gameObject.SetActive(false);
            //newCube.GetComponent<MeshRenderer>().enabled = true;

        }
    }
}