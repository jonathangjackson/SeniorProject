using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportThrower : MonoBehaviour {

    [HideInInspector]
    public bool isSelected;

    [Header("-Throw Settings Settings-")]
    [Range(.5f, 2.5f)]
    public float addedThrowForce = 1;

    public bool stayKinematic;
    public bool TeleportOnContact;
    public bool sticksToObjects;
    string LookTag = "";
    private int MAX_COUNT = 10;
    Rigidbody yourBody;
    [HideInInspector]
    public VROculusThrowTeleporter myTeleporter;
    private List<Vector3> positions = new List<Vector3>();

    bool hasHelper;
    void Start()
    {
        yourBody = GetComponent<Rigidbody>();
        PickUp();
        if(sticksToObjects && TeleportOnContact)
        {
            Debug.Log("Sticky and Teleport on contact will not function correctly");
        }
    }
    void FixedUpdate()
    {
        //TrackVel only if Selected
        if (isSelected)
        {
            TrackVelocity();
        }

    }
    //Track Vel
    private void TrackVelocity()
    {
        positions.Add(transform.position);
        if (positions.Count >= MAX_COUNT)
            positions.RemoveAt(0);
    }
    //Unlock for Deselecting
    public void UnlockObject()
    {
        isSelected = false;
        yourBody.velocity = Vector3.zero;
        yourBody.angularVelocity = Vector3.zero;
    }
    //Pick Up Function
    public void PickUp()
    {
        isSelected = true;
        if (yourBody)
        {
            yourBody.isKinematic = true;
        }
    }

    //Throwing
    public void Throw()
    {
        AddForce();
    }
    //Add Force to Throw
    public void AddForce()
    {
        if (yourBody != null)
        {
            Vector3 vec = GetWorldVelocity();
            if (addedThrowForce != 0)
            {
                vec *= addedThrowForce;
            }
            yourBody.AddForce((vec / Time.fixedDeltaTime), ForceMode.Impulse);
        }
    }
    //Drop If Burst use Impluse Throw
    public void Drop(bool isImpulse = false)
    {
        transform.parent = null;
        isSelected = false;
        if (yourBody)
        {
            if (!stayKinematic)
            {
                yourBody.isKinematic = false;
            }
            Throw();
            if (isImpulse)
            {
                Invoke("ImpluseThrow", Time.deltaTime);
            }
            else
            {
                Invoke("Throw", Time.deltaTime);
            }
        }
    }

    public void DeleteThrower()
    {
        if(hasHelper)
        {
            Destroy(transform.parent.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    //Calcuate World Velocity
    public Vector3 GetWorldVelocity()
    {
        Vector3 velocity = Vector3.zero;

        for (int i = 1; i < positions.Count; i++)
        {
            velocity += positions[i] - positions[i - 1];
        }
        velocity /= (MAX_COUNT - 1);
        return velocity;
    }

    void OnTriggerEnter(Collider Col)
    {
        if (LookTag == "")
        {
            if (Col.transform != transform.parent)
            {
                DoBehavior(Col.transform);
            }
        }
        else
        {
            if (Col.transform.tag == LookTag)
            {
                DoBehavior(Col.transform);
            }
        }
    }

    void OnCollisionEnter(Collision Col)
    {
            if (LookTag == "")
            {
                if (Col.transform != transform.parent)
                {
                DoBehavior(Col.transform);
            }
            }
            else
            {
                if (Col.transform.tag == LookTag)
                {
                    DoBehavior(Col.transform);
                }
            }
     }
    private Vector3 curpos;
    void DoBehavior(Transform theParent)
    {
        if(TeleportOnContact)
        {
            myTeleporter.TeleportMe(transform);
            Destroy(gameObject);
        }
        if(sticksToObjects)
        {
            yourBody.isKinematic = true;


            var emptyObject = new GameObject("TeleportHelper");
            transform.parent = emptyObject.transform;
            emptyObject.transform.parent = theParent;

            hasHelper = true;
        }
    }
}
