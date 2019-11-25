using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.AI;
using System.Collections.Generic;

[RequireComponent(typeof(VRMovementOculus))]
public class VROculusTeleport : MonoBehaviour
{

    [Header("-Teleport Controls-")]
    public OVRInput.Button TeleportButton = OVRInput.Button.One;     //Teleport Button
    public bool canTeleport = true;
    public enum TeleportType { NavMesh, TaggedPoint, AnyCollider, Tag }; //Mode fof Teleportation Baked Navmesh is required for NavMesh Teleportation
    [Header("-Telepor Modes-")]
    public TeleportType TeleportMode = TeleportType.AnyCollider;
    public bool useArcTeleporter = true;
    [Header("-Teleport Settings-")]
    public LayerMask myLayers = ~0;
    public bool fadeTeleport = true;
    public float TeleMinDstance = 4;          // Min TeleportDistance
    public float TeleMaxDistance = 500;       //Max Teleport Distance
    public string theTag;                    //Tag for Tag Teleport Type
    public float teleportTime = 0;            // Blink and Teleport Speed, 0 Is Instan
    [Header("-ArcTeleporter Settings-")]
    [Range(1, 50)]
    public float forwardForce = 10;
    public float gravityRatio = 1;
    [Range(1, 50)]
    public float lineResolution = 25;
    [Header("-LineArc Settings-")]
    public LineArcSystem teleportLine;       //Use Line Arch System for Teleporter To Disable Set to None
    public Color canTeleportColor = Color.green;
    public Color canNotTeleportColor = Color.red;
    [Header("-Required HookUps-")]
    public Transform teleportPoint;
    VRMovementOculus refSystem;
    float teleportGive = .2f;
    // Use this for initialization
    bool inBlink;
    Transform storedTransformBlink;
    bool inTeleport;
    Transform storedTransformTeleport;
    void Start()
    {
        refSystem = GetComponent<VRMovementOculus>();
        if (canTeleport)
        {
            if (!teleportLine)
            {
                Debug.Log("teleportBlinkLine was not assigned disabling");
                this.enabled = false;
                return;
            }
            if (!teleportPoint)
            {
                Debug.Log("teleportPoint was not assigned disabling");
                this.enabled = false;
                return;
            }
        }
    }

    //// Update is called once per frame
    void Update()
    {
        if (canTeleport)
        {
            TeleportInput();
        }

    }

    /// <summary>
    ///  Teleport System Area ||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>
    void TeleportInput()
    {
        VRMovementOculus.InputData InputHolderDown = refSystem.InputReturnDown(TeleportButton);
        VRMovementOculus.InputData InputHolderUp = refSystem.InputReturnUp(TeleportButton);
        if (InputHolderDown.pressed)
        {
            inTeleport = true;
            storedTransformTeleport = InputHolderDown.selectedController;
            //Get Point

        }
        if (inTeleport)
        {
            GetTeleportPoint();
            //Forces Teleporter to Look Right At you
            teleportPoint.transform.DOLookAt(storedTransformTeleport.position, .1f, AxisConstraint.Y);
        }
        if (InputHolderUp.pressed)
        {
            if(!inTeleport)
            {
                return;
            }
            inTeleport = false;
            Teleport();
            if (teleportLine)
            {
                teleportLine.HideLine();
            }
            teleportPoint.gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// Teleportation Area |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>
    void Teleport()
    {
        if (!teleportPoint.gameObject.activeInHierarchy)
        {
            //OnlyTeleport if Active
            return;
        }
        //Instant if Zero
        if (fadeTeleport)
        {
            refSystem.myFade.StartFadeIn(refSystem.fadeTime);
            Vector3 holder = teleportPoint.position;
            holder.y += refSystem.GetHeight();
            refSystem.yourRig.transform.DOMove(holder,.1f);
            refSystem.yourRig.transform.position = holder;
            Invoke("BumpMe", Time.deltaTime);
            return;
        }
        if (teleportTime == 0)
        {
            Vector3 holder = teleportPoint.position;
            holder.y += refSystem.GetHeight();
            refSystem.yourRig.transform.DOMove(holder, .1f);
            refSystem.yourRig.transform.position = holder;
            Invoke("BumpMe", Time.deltaTime);
        }
        else
        {
            Vector3 holder = teleportPoint.position;
            holder.y += refSystem.GetHeight();
            refSystem.yourRig.transform.DOMove(holder, teleportTime);
            Invoke("BumpMe", teleportTime + Time.deltaTime);
        }
    }

    void BumpMe()
    {
        refSystem.yourRig.Move(Vector3.one * .01f);
    }
    /// <summary>
    /// Get Teleport Point |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>
    void GetTeleportPoint()
    {
        if (useArcTeleporter)
        {
            CastArc(storedTransformTeleport);
            return;
        }
        Ray ray = new Ray(storedTransformTeleport.position, storedTransformTeleport.forward);
        RaycastHit hit;
        bool foundPoint = false;
        //FireRayCast
        if (Physics.Raycast(ray, out hit, TeleMaxDistance, myLayers))
        {
            //Inviso ColliderFinder!
            // Debug.Log(hit.collider.name);
            if (Vector3.Distance(hit.point, refSystem.yourRig.transform.position) > TeleMinDstance)
            {
                //Only Show Teleport greater then Min
                switch (TeleportMode)
                {
                    case TeleportType.NavMesh:
                        foundPoint = NavMeshTeleport(hit);
                        break;
                    case TeleportType.TaggedPoint:
                        foundPoint = TagTeleport(hit);
                        break;
                    case TeleportType.AnyCollider:
                        foundPoint = ColliderTeleport(hit);
                        break;
                    case TeleportType.Tag:
                        foundPoint = TagTeleport(hit);
                        break;
                    default:
                        break;
                }
                if (foundPoint)
                {
                    teleportPoint.gameObject.SetActive(true);

                    if (TeleportMode == TeleportType.TaggedPoint)
                    {
                        teleportPoint.transform.DOMove(ray.GetPoint(hit.distance - teleportGive), 0);

                    }
                    else
                    {
                        teleportPoint.transform.DOMove(ray.GetPoint(hit.distance - teleportGive), .05f);
                    }
                    if (teleportLine)
                    {
                        if (TeleportMode == TeleportType.TaggedPoint)
                        {
                            teleportLine.CreateLine(storedTransformTeleport.position, teleportPoint.position, canTeleportColor);
                        }
                        else
                        {
                            teleportLine.CreateLine(storedTransformTeleport.position, teleportPoint.position, canTeleportColor);
                        }

                    }
                    //Smooths Teleporter there

                }
                else
                {
                    if (teleportLine)
                    {
                        teleportLine.CreateLine(storedTransformTeleport.position, hit.point, canNotTeleportColor);
                    }
                    if (TeleportMode == TeleportType.AnyCollider)
                    {
                        if (teleportLine)
                        {
                            teleportLine.CreateLine(storedTransformTeleport.position, hit.point, canNotTeleportColor);
                        }
                    }
                    //Did not Hit Correct Point
                    teleportPoint.gameObject.SetActive(false);
                }
            }
            else
            {
                if (teleportLine)
                {
                    teleportLine.HideLine();
                }
                //Not in Min Distance
                teleportPoint.gameObject.SetActive(false);
            }
        }
        else
        {
            if (teleportLine)
            {
                teleportLine.HideLine();
            }
            //Extra Else Just in case of Weirdness
            teleportPoint.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// Teleport Functions Seporated Cleaner Code |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// </summary>

    bool NavMeshTeleport(RaycastHit hit)
    {
        //Calcuate if Point is on NavMesh
        Vector3 holder = Vector3.zero;
        if (RandomPoint(hit.point, .01f, out holder))
        {
            return true;
        }
        else
        {
            //Not On NavMesh
            return false;
        }
    }


    bool TagTeleport(RaycastHit hit)
    {
        if (hit.collider.tag == theTag)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool ColliderTeleport(RaycastHit hit)
    {
        return true;
    }

    /// <summary>
    /// NavMesh Helper Teleportation |||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||||
    /// Ripped Straight out of Documentation - Finds the closest point to where you point on Navmesh.
    /// https://docs.unity3d.com/ScriptReference/NavMesh.SamplePosition.html
    /// </summary>
    /// 
    public void CastArc(Transform start)
    {
        List<Vector3> points = new List<Vector3>();

        Vector3 currentPos = start.position;
        Vector3 velocity = start.forward * forwardForce / lineResolution;
        Vector3 gravity = Physics.gravity * gravityRatio / (lineResolution * lineResolution);

        points.Add(currentPos);

        RaycastHit hit = new RaycastHit();

        bool impactFound = false;

        while (!impactFound)
        {
            Ray ray = new Ray(currentPos, velocity);
            float distance = velocity.magnitude;
            if (Physics.Raycast(ray, out hit, distance, myLayers))
            {
                impactFound = true;

                points.Add(hit.point);
            }
            else
            {
                currentPos += velocity;
                velocity += gravity;

                points.Add(currentPos);

                if (points.Count > 1000)
                {
                    break;
                }
            }


        }

        teleportLine.myLine.enabled = true;
#if UNITY_5_6_OR_NEWER
        teleportLine.myLine.positionCount = points.Count;
#else
            teleportLine.myLineSetVertexCount(points.Count);
#endif
        teleportLine.myLine.SetPositions(points.ToArray());

        //Only Show Teleport greater then Min
        switch (TeleportMode)
        {
            case TeleportType.NavMesh:
                impactFound = NavMeshTeleport(hit);
                break;
            case TeleportType.TaggedPoint:
                impactFound = TagTeleport(hit);
                break;
            case TeleportType.AnyCollider:
                impactFound = ColliderTeleport(hit);
                break;
            case TeleportType.Tag:
                impactFound = TagTeleport(hit);
                break;
            default:
                break;
        }

        teleportPoint.gameObject.SetActive(impactFound);
        if (impactFound)
        {
            teleportPoint.position = hit.point;
            teleportPoint.forward = hit.normal;
            //ArcImpact.position = hit.point;
            //ArcImpact.forward = hit.normal;
            teleportLine.SetColor(canTeleportColor);
        }
        else
        {
            teleportPoint.gameObject.SetActive(false);
            teleportLine.SetColor(canNotTeleportColor);
        }

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 holder = center + Random.insideUnitSphere * range;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(holder, out hit, 1.0f, 1 << NavMesh.GetAreaFromName("Walkable")))
        {
            result = hit.position;
            return true;
        }
        result = Vector3.zero;
        return false;
    }

}
