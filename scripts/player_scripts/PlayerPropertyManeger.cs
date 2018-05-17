using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPropertyManeger : MonoBehaviour {
    public int directionX = 0;
    public int directionY = 0;
    public float speedX = 0f;
    public float speedY = 0f;
    public bool clicked;
    private PlayerMove cl;
    public bool isGrounded;
    public int shootCount;
    public int deathCount;
    public Vector2 position;
    public int currentLevel;
    public static float time;
    public bool grounded;
    public bool boosting;
    private GameObject player;


    Animator anim;
    private TriggerBehaviors tb;

    void Start () {

        player = GameObject.Find("player");
        cl = player.GetComponent<PlayerMove>();
        anim = player.GetComponent<Animator>();
        tb = player.GetComponent<TriggerBehaviors>();
	}
	
	// Update is called once per frame
	void Update () {

        position = player.transform.position;
        isGrounded = cl.isGrounded;
        deathCount = TriggerBehaviors.deathCount;
        shootCount = cl.shootCount;
        directionX = cl.directionX;
        directionY = cl.directionY;
        speedX = cl.speedX;
        speedY = cl.speedY;
        grounded = cl.isGrounded;
        boosting = cl.boosting;

        
        anim.SetBool("isGrounded", grounded);
        anim.SetBool("boosting", boosting);

        anim.SetInteger("direction", directionX);
        anim.SetFloat("speed", Mathf.Abs(speedX));
        
    }
}
