using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Vector3 ropePoint1;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100;
    private SpringJoint joint;


    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple();
        }
    }

    private void LateUpdate()
    {
        DrawRope();
    }

    void StartGrapple()
    {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            ropePoint1 = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = ropePoint1;

            float distanceFromPoint = Vector3.Distance(player.position, ropePoint1);

        }
    }

    void DrawRope()
    {
        if (!joint) return;

        lineRenderer.SetPosition(0, gunTip.transform.position);
        lineRenderer.SetPosition(1, ropePoint1);

    }

    void StopGrapple()
    {
        lineRenderer.positionCount = 0;
        Destroy(joint);
    }
}
