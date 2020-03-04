using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedX : MonoBehaviour
{
    public GameObject Tablet1, Tablet2, redCross, checkMark;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Tablet1.SetActive(true);
            Tablet2.SetActive(false);
            checkMark.SetActive(false);
            redCross.SetActive(false);

        }
    }
}
