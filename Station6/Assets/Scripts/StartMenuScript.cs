using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenuScript : MonoBehaviour
{
    public void PlayButtonHit()
    {
        SceneManager.LoadScene("TutorialLevel");
    }

    public void QuitButtonHit()
    {
        Application.Quit();
    }
}
