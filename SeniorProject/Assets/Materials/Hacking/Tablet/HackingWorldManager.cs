using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackingWorldManager : MonoBehaviour
{
    public bool isHacked = false;
    public ButtonManager[] buttons = new ButtonManager[4];
    public GameObject hackingWorldObj;

    private Renderer rend;
    private Material dissolveMat;
    private int[] setSequence = new int[4];
    private int[] pressedSequence = new int[4];
    private int posInSequence = 0;
    private void Start()
    {
        rend = hackingWorldObj.GetComponent<Renderer>();
        dissolveMat = rend.material;
    }

    public void loadSequence(int[] sequence)
    {
        setSequence = sequence;
    }

    //checks the correct sequence against the entered sequence 
    public bool checkSequence()
    {
        for(int i = 0; i < 4; i++)
        {
            Debug.Log("Sequence: " + setSequence[i] + ", Pressed: " + pressedSequence[i]);
            if(setSequence[i] != pressedSequence[i])
            {
                resetSequencePress();
                return false;
            }
        }
        return true;
    }

    //resets the entred sequence to -1 representing no values 
    public void resetSequencePress()
    {
        for(int i = 0; i < 4; i++)
        {
            buttons[i].reset();
            pressedSequence[i] = -1;
            posInSequence = 0;
        }
    }

    public void buttonPressed(int button)
    {
        pressedSequence[posInSequence] = button;
        posInSequence++;
        if (posInSequence == 4)
        {
            if (checkSequence())
            {
                isHacked = true;
            }
            else
            {
                dissolveMat.SetColor("Color_A232AC94", Color.red);

            }
        }
    }
}
