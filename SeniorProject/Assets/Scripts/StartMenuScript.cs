using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void PlayButtonHit()
    {
        SceneManager.LoadScene("Station6");
    }

    public void QuitButtonHit()
    {
        Application.Quit();
    }
}
