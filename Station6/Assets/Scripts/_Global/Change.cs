using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    public GameObject minerva;
    public GameObject ant;
    public GameObject rig;

    bool minervaActive = true;
    bool antActive = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 MinervaPos = GameObject.Find("Minerva").transform.position;
        Vector3 AntPos = GameObject.Find("Ant").transform.position;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (antActive == true)
            {
                minervaActive = true;
                antActive = false;
                rig.GetComponent<CharacterController>().height = 4.4f;
                ant.transform.parent = null;
                rig.transform.position = MinervaPos;
                minerva.transform.parent = rig.transform;
            }
            else if (minervaActive == true)
            {
                minervaActive = false;
                antActive = true;
                rig.GetComponent<CharacterController>().height = 0.6f;
                minerva.transform.parent = null;
                rig.transform.position = AntPos;
                ant.transform.parent = rig.transform;

            }

        }
    }
}
