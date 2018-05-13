using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grappling_hook : MonoBehaviour {

    Camera Cam;
    public bool isEnabled = false;
    RaycastHit2D hit;
    public GameObject anchor;
    public LayerMask canColide;
    DistanceJoint2D joint;
    public Vector2 playerPosition = new Vector2();
    public Vector2 mouseDir = new Vector2();
    public Vector2 mousePosition = new Vector3();
    public bool didHit;
    public float ropeMaxShootDistance = 20f;
    public LineRenderer line;
    RaycastHit2D canGrapple;
    SpringJoint2D spring;

    PlayerMove pl;
    private Rigidbody2D anchorRB;
    private SpriteRenderer anchorSprite;
    private bool distanceSet;
    private int positions;
    private Vector2 dynamicPoint;
    Transform variablePosition;
    Transform playerTransform;
    CustomCursor cc;
    bool letGoOfMovingObject = false;
    Vector3 velocityOfGrappleX = new Vector3(0, 0, 0);
    Vector3 velocityOfGrappleY = new Vector3(0, 0, 0);

    // Use this for initialization
    void Start () {

        playerTransform = GameObject.Find("player").GetComponent<Transform>();
        pl = GetComponent<PlayerMove>();
        line = anchor.GetComponent<LineRenderer>();
        Cam = Camera.main;
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        anchorRB = anchor.GetComponent<Rigidbody2D>();
        anchorSprite = anchor.GetComponent<SpriteRenderer>();
        line.enabled = false;
        cc = Cam.GetComponent<CustomCursor>();

    }


    private void handleMovingObject() {

        if (variablePosition == null) {
            anchor.transform.position = hit.point;
            anchor.transform.SetParent(hit.collider.transform);
            variablePosition = hit.collider.transform;
            letGoOfMovingObject = true;
        }
    }

    private void updateRope() {

        if (!didHit) {
            //the rope did not hit anything so stop here
            return;
        }
        else if (hit.collider.tag != "movingFloor") {
            //if we dont expect the object to move we can just put the anchor on the sopt that was hit
            anchor.transform.position = hit.point;
        }
        else {
            if( hit.collider.GetComponent<MoveLeftAndRight>() != null) {
                velocityOfGrappleX = hit.collider.GetComponent<MoveLeftAndRight>().currentVelocity;
            }
            if (hit.collider.GetComponent<moveUpAndDown>()!= null) {
                velocityOfGrappleY = hit.collider.GetComponent<moveUpAndDown>().currentVelocity;
            }
            handleMovingObject();
        }

        


    }


    private void inputHandler(Vector2 aimDirection) {

        bool pauseState = GameObject.Find("Pause Menu").GetComponent<PauseMenu>().isPaused;

        if (Input.GetKeyDown(KeyCode.Space)){

            if (didHit){

                return;
            }
            //sends out our raycast with our specified range for the grappling hook
            hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxShootDistance, canColide);

            if (hit.collider != null) {

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {

                    didHit = true;
                    //turn on our distance joint
                    joint.distance = Vector2.Distance(playerPosition, hit.point);
                    joint.enabled = true;
                    anchorSprite.enabled = true;
                    isEnabled = true;
                    line.enabled = true;

                }
                else {
                    //we did not hit anything we wanted to hit, so turn everything off
                    didHit = false;
                    joint.enabled = false;
                    isEnabled = false;
                    line.enabled = false;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space) && !pauseState) {
            // if they stop holding the space bar down then the tope dissapears
            resetRope();
        }
    }

    public void resetRope() {

        joint.enabled = false;
        line.enabled = false;
        variablePosition = null;
        didHit = false;
        isEnabled = false;
        anchorSprite.enabled = false;
        joint.connectedAnchor = new Vector2(0, 0);
        anchor.transform.SetParent(playerTransform);

        if (letGoOfMovingObject) {

            Vector3 previousPlayerVelocity = GetComponent<Rigidbody2D>().velocity;

            Vector3 compare = new Vector3(0, 0, 0);
            if (velocityOfGrappleX != compare && velocityOfGrappleY != compare) {
                Vector3 letGoVelocity = new Vector3(velocityOfGrappleX.x, velocityOfGrappleY.y, 0);
                GetComponent<Rigidbody2D>().velocity = previousPlayerVelocity + letGoVelocity;
            }
            else if (velocityOfGrappleX == compare && velocityOfGrappleY != compare) {
                GetComponent<Rigidbody2D>().velocity = previousPlayerVelocity + velocityOfGrappleY;
            }
            else {
                GetComponent<Rigidbody2D>().velocity = previousPlayerVelocity + velocityOfGrappleX;
            }

            letGoOfMovingObject = false;
            velocityOfGrappleX = new Vector3(0, 0, 0);
            velocityOfGrappleY = new Vector3(0, 0, 0);
        }



    }

    // Update is called once per frame
    void Update () {
        if (line.enabled == true) {

            line.SetPosition(0, transform.position);
            line.SetPosition(1, anchor.transform.position);
        }

        //we don't want to have to call the SetCustomCursor every frame, so before wecheck the valwith the previous frame to see wif we need to call it
        bool previosFrameCanGrapple = cc.canGrapple;

        mousePosition = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

        //get player position
        playerPosition = transform.position;
        mouseDir = mousePosition - playerPosition;
        //as the name would suggest this gets the angle that the player is aiming at
        var aimAngle = Mathf.Atan2(mouseDir.y, mouseDir.x);
        //make sure we have a positive angle
        if (aimAngle < 0f) {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }
            
        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;

        //send out a raycast every frame to see if we need to change the cursor
        canGrapple = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxShootDistance, canColide);

        if (canGrapple.collider != null) {

            if (canGrapple.collider.gameObject.GetComponent<Rigidbody2D>() != null && !previosFrameCanGrapple) {
                cc.canGrapple = true;
                cc.SetCustomCursor();
            }
        }
   
        else if (canGrapple.collider == null && previosFrameCanGrapple) {                            
            cc.canGrapple = false; 
            cc.SetCustomCursor();            
        }
        
        

        inputHandler(aimDirection);
        updateRope();


    }
}
