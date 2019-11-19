using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openLeft : MonoBehaviour
{
    private Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            anim.SetBool("open", true);
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            anim.SetBool("open", false);

    }

    // Update is called once per frame
    void Update()
    {      

    }

    public void Add()
    {
        
    }
}
