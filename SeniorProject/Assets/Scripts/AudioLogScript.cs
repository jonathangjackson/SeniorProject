using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLogScript : MonoBehaviour
{

    AudioSource audioLog;

    bool playHit;

    // Start is called before the first frame update
    void Start()
    {
        audioLog = GetComponent<AudioSource>();
        playHit = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudioLog()
    {
        if(playHit == false)
        {
            audioLog.Play();
            playHit = true;
        }
        
    }

    public void PauseAudioLog()
    {
        if(playHit == true)
        {
            audioLog.Pause();
            playHit = false;

        }
    }

    public void RestAudioLog()
    {
        if(playHit == true)
        {
            audioLog.Stop();
            playHit = false;
        }
        
    }
}
