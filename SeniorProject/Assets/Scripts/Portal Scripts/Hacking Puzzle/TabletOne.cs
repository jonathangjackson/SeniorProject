using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabletOne : MonoBehaviour
{
    public GameObject Tablet1, Tablet2, checkMark, redX;
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
            Tablet1.SetActive(false);
            Tablet2.SetActive(true);
            checkMark.SetActive(true);
            redX.SetActive(true);

        }
    }
}
