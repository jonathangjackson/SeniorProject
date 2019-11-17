using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using OVR;

public class ToggleArmMenu : MonoBehaviour
{

    public GameObject armMenu;
    bool menuOn;
    // Start is called before the first frame update
    void Start()
    {
        menuOn = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (OVRInput.GetDown(OVRInput.Button.Three)){
            menuOn = !menuOn;

            armMenu.SetActive(menuOn);
        }

    }
}
