using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
    private List<Vector2> ropePositions = new List<Vector2>();
    public LineRenderer line;
    private Rigidbody2D anchorRB;
    private SpriteRenderer anchorSprite;
    private bool distanceSet;
    private int positions;
    

    // Use this for initialization
    void Start () {
        line = anchor.GetComponent<LineRenderer>();
        Cam = Camera.main;
        joint = GetComponent<DistanceJoint2D>();
        joint.enabled = false;
        anchorRB = anchor.GetComponent<Rigidbody2D>();
        anchorSprite = anchor.GetComponent<SpriteRenderer>();
        line.enabled = false;
    }

    //set rhe rope vertex positions
    private void updateRopePositions() {

        if (!didHit) {
            //the rope did not hit anything so stop here
            return;
        }

        positions = ropePositions.Count + 1;

        for (int i = positions - 1; i >= 0; i--) {
            if (i != positions - 1) {


                //configure the joint distance to the distance between the player and where they want to grapple to

            if (i == ropePositions.Count - 1 || ropePositions.Count == 1) {
                    //we move the anchor to where the player wants to grapple to
                    Vector2 newPosition = ropePositions[ropePositions.Count - 1];
                    if (ropePositions.Count == 1) {
                        anchorRB.transform.position = newPosition;

                        if (!distanceSet) {

                            joint.distance = Vector2.Distance(transform.position, newPosition);
                            distanceSet = true;
                        }
                        else {

                            anchorRB.transform.position = newPosition;

                            if (!distanceSet) {
                                joint.distance = Vector2.Distance(transform.position, newPosition);
                                distanceSet = true;
                            }
                        }
                    }
                    //the point at which the rope connects to the object 
                    else if (i - 1 == ropePositions.IndexOf(ropePositions.Last())) {

                        var lastPosition = ropePositions.Last();
                        anchorRB.transform.position = lastPosition;
                        if (!distanceSet) {

                            joint.distance = Vector2.Distance(transform.position, lastPosition);
                            distanceSet = true;
                        }
                    }

                    }
                }
            }




        }
    

    private void inputHandler(Vector2 aimDirection) {
        bool pauseState = GameObject.Find("Pause Menu").GetComponent<PauseMenu>().isPaused;
        if (Input.GetKeyDown(KeyCode.Space)){

            if (didHit) return;
            //sends out our raycast with our specified range for the grappling hook
            var hit = Physics2D.Raycast(playerPosition, aimDirection, ropeMaxShootDistance, canColide);

            if (hit.collider != null) {

                didHit = true;

                if (!ropePositions.Contains(hit.point)){

                    ropePositions.Add(hit.point);
                    //turn on our distance joint
                    joint.distance = Vector2.Distance(playerPosition, hit.point);
                    joint.enabled = true;
                    anchorSprite.enabled = true;
                    isEnabled = true;
                    line.enabled = true;

                }
                else {
                    //we did not hit turn everything off
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
        ropePositions.Clear();
        anchorSprite.enabled = false;
    }

    // Update is called once per frame
    void Update () {
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


        //do a raycast to the object we want to see if we can connect to
        //hit = Physics2D.Raycast(transform.position, mouseDir, distance, canColide);

        /*if (hit.collider != null && hit.collider.gameObject.GetComponent<Rigidbody2D>() != null) {
            didHit = true;
            joint.enabled = true;
            joint.connectedBody = hit.collider.gameObject.GetComponent<Rigidbody2D>();
            //make the joint where the ray cast hit
            joint.connectedAnchor = hit.point - new Vector2(hit.collider.transform.position.x, hit.collider.transform.position.y);
            //get distance between player and the thing that they hooked on to
            joint.distance = Vector2.Distance(transform.position, hit.point);

        }*/

        inputHandler(aimDirection);
        updateRopePositions();


    }
}
