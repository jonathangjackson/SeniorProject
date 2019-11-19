using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class changeScene : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Cube")
        {
            SceneManager.LoadScene("VROculusMoveV2Demo Basic");
        }        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Cube")
        {
            Debug.Log("Cube collided with pressure plate");
            SceneManager.LoadScene("TutorialLevelPostHack");
        }
    }
}
