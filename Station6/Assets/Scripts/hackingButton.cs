using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class hackingButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PreparedOVRCameraRig" || other.name == "HackingButton")
        {
            Debug.Log("entered");
            SceneManager.LoadScene("HackingScene");
        }
    }
}
