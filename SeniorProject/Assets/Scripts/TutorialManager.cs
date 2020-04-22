using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<TutorialController> tutorialControllers;
    public AudioSource audio;
    private bool clipPlay = false;
    private int currentPosition = 0;
    public bool skipTutorial = false;
    public GameObject OVRRig;
    public VRMovementOculus movement;
    public GameObject Pod;
    public Animator unlockDoor;
    
    void Start()
    {

        if (skipTutorial)
            skipTutorialClips();
        else
        {
            playAudioClip(tutorialControllers[currentPosition].getAudioClip());
            clipPlay = true;
            OVRRig.transform.parent = Pod.transform;
        }
    }

    void Update()
    {
        if(movement.canMove && currentPosition < 2)
            movement.canMove = false;
        if (!audio.isPlaying && clipPlay)
        {
            onClipEnd();
        }
        if (tutorialControllers[0].triggerActivated)
        {
            currentPosition++;
            tutorialControllers[0].destroyTutorial();
            tutorialControllers.RemoveAt(0);
            //END of Tutorial
            if(tutorialControllers.Count == 1)
            {
                unlockDoor.SetBool("Locked", false);
            }
            if (tutorialControllers.Count == 0)
            {

                //Debug.Log("END OF Tutorial");
                Destroy(this);
            }
            else
            {
                tutorialControllers[0].gameObject.SetActive(true);
                playAudioClip(tutorialControllers[0].getAudioClip());

                if (currentPosition == 2)
                {
                    OVRRig.transform.parent = null;
                    Pod.GetComponent<Animator>().SetBool("Play", true);
                }
            }
        }
    }

    public void replayAudioClip()
    {
        playAudioClip(tutorialControllers[0].getAudioClip());
    }

    void playAudioClip(AudioClip ac)
    {
        audio.Stop();
        audio.clip = ac;
        audio.Play();
        clipPlay = true;
    }

    public void onClipEnd()
    {
        //currentPosition++;
        clipPlay = false;
        tutorialControllers[0].sequentialTriggers[0].setObjectActive(true);
        //tutorialControllers[0].setChildActive(0);
        if (tutorialControllers[0].hasHighlights)
        {
            tutorialControllers[0].activateHighlights();
        }
        if(currentPosition == 2)
        {
            movement.canMove = true;
        }
        /*
        if(tutorialControllers[0].triggerType.CompareTo(TutorialController.TriggerType.Animation) == 0)
        {
            Debug.Log("anim");
            tutorialControllers[0].startAnimation();
        }*/
    }

    private void skipTutorialClips()
    {
        OVRRig.transform.parent = null;
        movement.canMove = true;
        tutorialControllers[0].gameObject.SetActive(false);
        //Play Animation and let the player move 
        Destroy(this);
    }
}
