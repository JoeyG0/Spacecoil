using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pringHook : MonoBehaviour {

    Camera Cam;
    public bool isEnabled = false;
    RaycastHit2D hit;
    public GameObject anchor;
    public LayerMask canColide;
    SpringJoint2D joint;
    public Vector2 playerPosition = new Vector2();
    public Vector2 mouseDir = new Vector2();
    public Vector2 mousePosition = new Vector3();
    public bool didHit;
    public float ropeMaxShootDistance = 20f;

    public LineRenderer line;
    private Rigidbody2D anchorRB;
    private SpriteRenderer anchorSprite;
    private bool distanceSet;
    private int positions;
    private Vector2 dynamicPoint;
    Transform parent;


    // Use this for initialization
    void Start() {
        line = anchor.GetComponent<LineRenderer>();
        Cam = Camera.main;
        joint = GetComponent<SpringJoint2D>();
        joint.enabled = false;
        anchorRB = anchor.GetComponent<Rigidbody2D>();
        anchorSprite = anchor.GetComponent<SpriteRenderer>();
        line.enabled = false;

    }


    private void updateRope() {
        if (!didHit) {
            //the rope did not hit anything so stop here
            return;
        }
        else if (hit.collider.tag != "moveingFloor") {
            //if we dont expect the object to move we can just put the anchor on the sopt that was hit
            anchor.transform.position = hit.point;
        }
        else {
            if (parent != null) {

                if (hit.collider.GetComponent<MoveLeftAndRight>() != null) {
                    if (hit.collider.GetComponent<MoveLeftAndRight>().goingLeft) {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2((-1 * (hit.collider.GetComponent<MoveLeftAndRight>().speed * Time.deltaTime)), 0));
                    }
                    else {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2((hit.collider.GetComponent<MoveLeftAndRight>().speed * Time.deltaTime), 0));
                    }
                }

                //at this point this puts the anchor in the center
                var changeX = hit.collider.transform.position.x - parent.position.x;
                var changeY = hit.collider.transform.position.y - parent.position.y;
                anchor.transform.position = new Vector2((anchor.transform.position.x + changeX), (anchor.transform.position.y + changeY));
                parent = anchor.transform;
                //joint.distance = Vector2.Distance(playerPosition, anchor.transform.position);
            }
            else {

                anchor.transform.position = hit.point;
                parent = hit.collider.transform;
            }

        }




    }


    private void inputHandler(Vector2 aimDirection) {

        bool pauseState = GameObject.Find("Pause Menu").GetComponent<PauseMenu>().isPaused;

        if (Input.GetKeyDown(KeyCode.Space)) {

            if (didHit) {

                return;
            }
            //sends out our raycast with our specified range for the grappling hook
            hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxShootDistance, canColide);

            if (hit.collider != null) {

                if (hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {

                    didHit = true;
                    //ropePositions.Add(hit.point);
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
        didHit = false;
        //playerMovement.isSwinging = false; probably do not need this
        isEnabled = false;
        anchorSprite.enabled = false;
        joint.connectedAnchor = new Vector2(0, 0);


    }

    // Update is called once per frame
    void Update() {
        if (line.enabled == true) {

            line.SetPosition(0, transform.position);
            line.SetPosition(1, anchor.transform.position);
        }

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




        inputHandler(aimDirection);
        updateRope();


    }
}

