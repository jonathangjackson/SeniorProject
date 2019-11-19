using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FallDown : MonoBehaviour
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
        if (other.name == "PreparedOVRCameraRig" || other.name == "Fall")
        {
            Debug.Log("entered");
            SceneManager.LoadScene("Portal2");
        }
    }
}
