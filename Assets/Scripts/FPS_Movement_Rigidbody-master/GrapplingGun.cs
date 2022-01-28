using UnityEngine;

public class GrapplingGun : MonoBehaviour {

    //blueGun
    private LineRenderer lr;
    private Vector3 grapplePoint;
    public LayerMask whatIsGrappleable;
    public Transform gunTip, camera, player;
    private float maxDistance = 100f;
    private SpringJoint joint;

    //mine
    public GameObject grapplingPS;
    public float ropeReducePower = 4;
    public float impulsePower = 50;


    //redGun
    public GameObject impulsePS;
    public Rigidbody playerRb;


    void Awake() {
        lr = GetComponent<LineRenderer>();
    }

    void Update() 
    {
        if (GameManager.instance.currentGun == "blue")
        {
            if (Input.GetMouseButtonDown(0))
            {
                EffectsON();
                StartGrapple();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                EffectsOFF();
                StopGrapple();
            }
            ReduceRopeLenght();
        }
        else if(GameManager.instance.currentGun == "red")
        {
            if (Input.GetMouseButtonDown(0))
            {
                StopGrapple();

                makeInpulse();
                activateParticleSystemImpulse();
            }
            else if (Input.GetMouseButtonUp(0))
            {
                
            }
            
        }
    }

    //Called after Update
    void LateUpdate() 
    {
        DrawRope();
    }

    /// <summary>
    /// Call whenever we want to start a grapple
    /// </summary>
    void StartGrapple() {
        RaycastHit hit;
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable)) {
            grapplePoint = hit.point;
            joint = player.gameObject.AddComponent<SpringJoint>();
            joint.autoConfigureConnectedAnchor = false;
            joint.connectedAnchor = grapplePoint;

            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            //The distance grapple will try to keep from grapple point. 
            joint.maxDistance = distanceFromPoint * 0.8f;
            joint.minDistance = distanceFromPoint * 0.25f;

            //Adjust these values to fit your game.
            joint.spring = 4.5f;
            joint.damper = 7f;
            joint.massScale = 4.5f;

            lr.positionCount = 2;
            currentGrapplePosition = gunTip.position;
        }
    }


    /// <summary>
    /// Call whenever we want to stop a grapple
    /// </summary>
    void StopGrapple() {
        lr.positionCount = 0;
        Destroy(joint);
    }

    private Vector3 currentGrapplePosition;
    
    void DrawRope() {
        //If not grappling, don't draw rope
        if (!joint) return;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 8f);
        
        lr.SetPosition(0, gunTip.position);
        lr.SetPosition(1, currentGrapplePosition);
    }

    void EffectsON()
    {
        grapplingPS.SetActive(true);
    }

    void EffectsOFF()
    {
        grapplingPS.SetActive(false);
    }

    void ReduceRopeLenght()
    {
        if (!joint) return;
        joint.maxDistance -= Time.deltaTime*ropeReducePower;
    }

    public bool IsGrappling() {
        return joint != null;
    }

    public Vector3 GetGrapplePoint() {
        return grapplePoint;
    }

    // para la gun roja
    void makeInpulse()
    {
        playerRb.AddForce(gunTip.forward*impulsePower, ForceMode.Impulse );
    }

    void activateParticleSystemImpulse()
    {
        impulsePS.GetComponent<ParticleSystem>().Play();
    }
}
