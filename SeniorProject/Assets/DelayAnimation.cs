using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayAnimation : MonoBehaviour
{
    public Animator animtest;

    // Start is called before the first frame update
    void Start()
    {
        animtest.gameObject.GetComponent<Animator>().enabled = false;
        StartCoroutine(PlayEndCredits());
        //animtest = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator PlayEndCredits()
    {
        yield return new WaitForSeconds(5);
        animtest.gameObject.GetComponent<Animator>().enabled = true;
    }
}
