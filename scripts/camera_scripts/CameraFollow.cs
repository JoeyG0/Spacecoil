using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    GameObject player;
    private PlayerMove cl;
    private Vector3 lastPosition;
    private float shakeTimer = 0f;
    private float shakeAmount = 0f;
    Vector2 shakePosition;
    private float speedX {
        get {
            return cl.speedX;
        }
    }
    private float speedY {
        get {
            return cl.speedY;
        }
    }
    private Vector3 offset;
    void Start() {
        player = GameObject.Find("player");
        cl = GameObject.Find("player").GetComponent<PlayerMove>();
    }

    public void shakeCamera(float shakeAmount, float shakeTimer) {

        this.shakeAmount = shakeAmount;
        this.shakeTimer = shakeTimer;
    }
    // Update is called once per frame
    void LateUpdate() {
        if (shakeTimer >= 0) {
            shakePosition = Random.insideUnitCircle * shakeAmount;
            transform.position = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y, transform.position.z);
            shakeTimer -= Time.deltaTime;
        }
        else if(cl.boosting == true) {
            shakePosition = Random.insideUnitCircle * .8f;
            transform.position = new Vector3(transform.position.x + shakePosition.x, transform.position.y + shakePosition.y, transform.position.z);
        }
        //so the camera does not get stuck on walls
        if (player.transform.position != lastPosition && player.transform.parent == null) {
             transform.Translate(Vector3.right * Time.deltaTime * speedX);
             transform.Translate(Vector3.up * Time.deltaTime * speedY);
           // GetComponent<Transform>().parent = player.transform;
        }
        else {
            GetComponent<Transform>().parent = player.transform.parent.transform;
        }
        

        
        
        //lastPosition = player.transform.position;
    }
}