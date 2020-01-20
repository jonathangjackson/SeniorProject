using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killPowerCell : MonoBehaviour
{
    public bool hackingSuccess;

    // Start is called before the first frame update
    void Start()
    {
        hackingSuccess = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "PowerSlot")
        {
            Destroy(gameObject);
            hackingSuccess = true;
        }
    }
}