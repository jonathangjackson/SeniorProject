using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseMove : MonoBehaviour
{
    public float scaleFactor = 1.0f;
    public float force = 100.0f;
    public float speed = 10.0f;
    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(speed * Vector3.right * Time.deltaTime);
        this.transform.localScale += new Vector3(0, scaleFactor * Time.deltaTime, scaleFactor * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hit" ||  other.GetComponent<Rigidbody>() == null)
            return;

        coroutine = ClearTag(0.2f, other.gameObject);
        StartCoroutine(coroutine);
        other.tag = "hit";
        Vector3 dir = other.gameObject.transform.position - transform.position;
        dir = dir.normalized;
        other.gameObject.GetComponent<Rigidbody>().AddForce(dir * force);
    }

    private IEnumerator ClearTag(float waitTime, GameObject other)
    {
        yield return new WaitForSeconds(waitTime);
        other.tag = "Untagged";
    }
}
