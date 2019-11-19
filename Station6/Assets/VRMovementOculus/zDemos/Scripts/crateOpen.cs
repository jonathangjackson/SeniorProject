using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crateOpen : MonoBehaviour
{
    public NewCube bSS;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bSS.crateBool)
            GetComponent<Animator>().SetBool("open", bSS);
    }
}
