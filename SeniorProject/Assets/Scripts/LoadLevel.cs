using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public float delay;

    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(LoadLevelAfterDelay(delay));
    }

    void Update()
    {
        delay -= Time.deltaTime;
        if (delay < 0)
        {
            SceneManager.LoadScene("Station6");
        }
    }

}






