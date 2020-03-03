using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public List<GameObject> tutorialObjects;
    public AudioSource audio;
    private bool clipPlay = false;
    
    void Start()
    {
        playAudioClip(audioClips[0]);
        clipPlay = true;
    }

    void Update()
    {
        if (!audio.isPlaying && clipPlay)
        {
            onClipEnd();
        }
    }

    void playAudioClip(AudioClip ac)
    {
        audio.Stop();
        audio.clip = ac;
        audio.Play();
        clipPlay = true;
    }

    public void triggerNext()
    {
        if (audioClips.Count > 0)
        {
            tutorialObjects[0].SetActive(false);
            tutorialObjects.RemoveAt(0);
            audioClips.RemoveAt(0);
            playAudioClip(audioClips[0]);
            
        }
        else
        {
            Debug.Log("Tutorial Done");
        }
    }

    public void onClipEnd()
    {
        tutorialObjects[0].SetActive(true);
        clipPlay = false;
    }
}
