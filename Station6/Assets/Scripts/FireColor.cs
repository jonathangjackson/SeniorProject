using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireColor : MonoBehaviour
{
    public Color Red;
    public Color Blue;
    public Color BaseFireColor;
    public ParticleSystem Fire;
    private float colorF;
    private float hot;
    private float cold;
    private float enterDistance;
    // Start is called before the first frame update
    void Start()
    {
        enterDistance = 0.0f;
        colorF = 1.0f;
        hot = 1.0f;
        cold = 1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("d"))
        {
            this.transform.position = new Vector3(this.GetComponent<Transform>().position.x + 100 * Time.deltaTime, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);
        }
        if (Input.GetKeyDown("a"))
        {
            this.transform.position = new Vector3(this.GetComponent<Transform>().position.x - 100 * Time.deltaTime, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z);
        }
        if (Input.GetKeyDown("w"))
        {
            this.transform.position = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z + 100 * Time.deltaTime);
        }
        if (Input.GetKeyDown("s"))
        {
            this.transform.position = new Vector3(this.GetComponent<Transform>().position.x, this.GetComponent<Transform>().position.y, this.GetComponent<Transform>().position.z - 100 * Time.deltaTime);
        }
        Fire.startColor = new Color((colorF * BaseFireColor.r) + ((1.0f - hot) * Red.r) + ((1.0f - cold) * Blue.r), (colorF * BaseFireColor.g) + ((1.0f - hot) * Red.g) + ((1.0f - cold) * Blue.g), (colorF * BaseFireColor.b) + ((1.0f - hot) * Red.b) + ((1.0f - cold) * Blue.b));
    }

    private void OnTriggerEnter(Collider other)
    {
        Vector3 distance = this.GetComponent<Transform>().position - other.GetComponent<Transform>().position;
        float hyp = Mathf.Sqrt(Mathf.Pow(distance.x, 2.0f) + Mathf.Pow(distance.z, 2.0f));
        enterDistance = hyp;
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 distance = this.GetComponent<Transform>().position - other.GetComponent<Transform>().position;
        float hyp = Mathf.Sqrt(Mathf.Pow(distance.x, 2.0f) + Mathf.Pow(distance.z, 2.0f));
        if(other.gameObject.tag.CompareTo("Hot") == 0)
        {
            Debug.Log("HOT");
            hot = hyp / enterDistance;
        }
        if(other.gameObject.tag.CompareTo("Cold") == 0)
        {
            Debug.Log("COLD");
            cold = hyp / enterDistance;
        }
        colorF = hyp / enterDistance;
    }
}