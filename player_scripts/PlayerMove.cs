using System.Collections;
using System.Diagnostics;
using System.Collections.Generic;
using UnityEngine;
using System;
using Debug = UnityEngine.Debug;


public class PlayerMove : MonoBehaviour {
    public float shake = 3f;
    public float shakeTime = .2f;
    CameraFollow camMove;
    GameObject gun;
    gunRenderBehavior gunRender;
    public GameObject Gunbolt;
    Grappling_hook gh;
    float angle;
    public int directionX = 0;
    public float speedX = 0f;
    public int directionY = 0;
    public float speedY = 0f;
    bool overheat = false;
    public bool clicked = false;
    public int shootCount = 0;
    /// <summary>
    /// above added by Joey
    /// </summary>
    public float Force = 1200.0f;
    public float maxSpeed = 50.0f;
    public Vector2 mousePosition = new Vector2();
    public Rigidbody2D rb2D;
    public Vector2 TestPosition;
    public Vector2 playerPosition = new Vector2();
    public Vector2 mouseDir = new Vector2();
    public bool boosting;
    private GameObject button;
    public float thrustForce = 50f;
    public bool isGrounded = false;
    Camera Cam;
    public double threshhold = 200;
    public float timeToClick = 450;
    public int overheatLevel = 2;
    public double heatLevel;
    Stopwatch sw = new Stopwatch();

    // Use this for initialization
    void Start() {
        gun = GameObject.Find("gunRender");
        gunRender = gun.GetComponent<gunRenderBehavior>();
        heatLevel = 1;
        gh = GetComponent<Grappling_hook>();
        rb2D = GetComponent<Rigidbody2D>();
        Cam = Camera.main;
        camMove = Cam.GetComponent<CameraFollow>();
    }

    // Update is called once per frame
    void Update() {
        bool pauseState = GameObject.Find("Pause Menu").GetComponent<PauseMenu>().isPaused;
        
        if (Input.GetMouseButtonDown(0) && !pauseState) {
            gunRender.shootAnimationController(true, false);
            shoot();
            gunRender.shootAnimationController(false,true);
        }
    }


    void shoot() {
        
        angle = gunRender.gunAngle;

       // gunRender.shootAnimationController(true);
        shootCount++;

        RaycastHit2D hit = Physics2D.Raycast(rb2D.position, mouseDir);
        Effect();
        if (hit.collider != null) {
            
            if (hit.collider.gameObject.tag == "Button") {
            String nameHit = hit.collider.gameObject.name;
            button = GameObject.Find(nameHit);
            ButtonHit boolchange = button.GetComponent<ButtonHit>();
            boolchange.wasHit = !boolchange.wasHit;
                
            }
        }
        if (heatLevel < threshhold) {

            heatLevel += overheatLevel;
            rb2D.AddForce((-1 * mouseDir) * (int)(Force * (8 / heatLevel) * 1.5));

        }
        else {
            rb2D.AddForce(-1 * mouseDir * 12);
        }
                
        


        Vector2 temp;
        if (Math.Abs(rb2D.velocity.x) > maxSpeed && Math.Abs(rb2D.velocity.y) > maxSpeed) {

            temp = new Vector2(rb2D.velocity.x * 0.85f, rb2D.velocity.y * 0.85f);
            rb2D.velocity = temp;
        }
        else if (Math.Abs(rb2D.velocity.x) > maxSpeed) {

            temp = new Vector2(rb2D.velocity.x * 0.85f, rb2D.velocity.y);
            rb2D.velocity = temp;
        }
        else if (Math.Abs(rb2D.velocity.y) > maxSpeed) {

            temp = new Vector2(rb2D.velocity.x, rb2D.velocity.y * 0.85f);
            rb2D.velocity = temp;
        }

    }

    public void thrust() {

        if (Input.GetMouseButton(1) && isGrounded == true || Input.GetMouseButton(1) && gh.isEnabled == true) {
            boosting = true;
            rb2D.AddForce((-1 * mouseDir) * thrustForce);

        }
        else {
            boosting = false;
        }
    }
    private void Effect() {
        //float angle = Mathf.Atan2(mousePosition.y, mousePosition.x) * Mathf.Rad2Deg;
        //float angle = gunRender.gunAngle;

        Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        //Done
        Vector2 pos = new Vector2(gunRender.gunPosition.x + .8f, gunRender.gunPosition.y - .8f);
        //GameObject bullet = Instantiate(Gunbolt, rb2D.position, rotation);
        //GameObject bullet = Instantiate(Gunbolt, gunRender.gunPosition, rotation);
        GameObject bullet = Instantiate(Gunbolt, pos, rotation);
        Rigidbody2D bulletMove = bullet.GetComponent<Rigidbody2D>();
        if (heatLevel < threshhold) {
            //cameraTransform.localPosition = cameraTransform.localPosition + UnityEngine.Random.insideUnitSphere * shakeAmount;
            camMove.shakeCamera(shake, shakeTime);
            heatLevel += overheatLevel;
            bulletMove.AddForce((mouseDir) * (int)(Force * (8 / heatLevel) * 1000));
        }
        else
            bulletMove.AddForce((mouseDir) * (int)(Force * (8 / heatLevel) * 1000));


        Destroy(bullet, 3.0f);


    }

    private void OnCollisionEnter2D(Collision2D collision) {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "moveingFloor") {

            isGrounded = true;
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {

        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "moveingFloor") {

            isGrounded = false;
        }
    }

    private void FixedUpdate() {
        thrust();
        if (heatLevel > 1 && gh.isEnabled != true) {
            heatLevel -= 0.1;
        }
        if (heatLevel < 1)
            heatLevel = 1;
        if (isGrounded == true) {
            if (heatLevel > 1) {
                heatLevel = 1;
            }
            if (heatLevel < 1)
                heatLevel = 1;
        }
        speedX = rb2D.velocity.x;
        speedY = rb2D.velocity.y;
        if (speedX < 0) {

            directionX = -1;
            //we're changing the direction of the player sprite
            /*if(flip.flipX != true) {
                GetComponent<SpriteRenderer>().flipX = true;
            }*/
        }
        else if (speedX > 0) {
            /* if (flip.flipX != false) {
                 GetComponent<SpriteRenderer>().flipX = false;
             }*/
            directionX = 1;
        }
        else {
            directionX = 0;
        }
        if (speedY < 0) {
            directionY = -1;
        }
        else if (speedY > 0) {
            directionY = 1;
        }
        else {
            directionY = 0;
        }



        mousePosition = Cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        //TestPosition = gunRender.transform.position;
        playerPosition = rb2D.position;
        mouseDir = mousePosition - playerPosition;
        mouseDir = mouseDir.normalized;


    }

}
