using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerBehaviors : MonoBehaviour
{
    BoxCollider2D playerCollider;
    private PlayerMove cl;
    public float slowDown = 0.7f;
    public float speedUp = 1.7f;
    private GameObject player;
    Vector3 originalPosition;
    Grappling_hook hook;
    public static int deathCount;
    public static float time;
   // public bool reverse = true;
    
    //public GameObject bounce;


    // Use this for initialization
    void Start()
    {

        hook = GetComponent<Grappling_hook>();
        //anim = bounce.GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        cl = GameObject.Find("player").GetComponent<PlayerMove>();
        player = GameObject.Find("player");
        //anim = reverser.GetComponent<Animator>();
        originalPosition = player.transform.position;

    }

    /*private void OnTriggerStay2D(Collider2D collision) {

        if( collision.gameObject.tag == "platform") {
            transform.parent = collision.transform;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bouncy")
        {

            playerCollider.sharedMaterial.bounciness = 1;
            playerCollider.sharedMaterial = playerCollider.sharedMaterial;
        }
        if (collision.gameObject.tag == "Slow")
        {
            cl.rb2D.velocity = new Vector2(cl.rb2D.velocity.x, cl.rb2D.velocity.y) * slowDown;
        }
        if (collision.gameObject.tag == "Speed")
        {
            cl.rb2D.velocity = new Vector2(cl.rb2D.velocity.x, cl.rb2D.velocity.y) * speedUp;
        }
        if (collision.gameObject.tag == "Death")
        {
            time += Time.timeSinceLevelLoad;
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            deathCount++;
            
        }
        if (collision.gameObject.tag == "QuickStop")
        {
            cl.rb2D.velocity = new Vector2(0, 0);
        }
        if (collision.gameObject.tag == "Finish")
        {
            SceneManager.LoadScene("NextLevel");
            time += Time.timeSinceLevelLoad;
            PlayerPropertyManeger.time = time;
            Debug.Log(PlayerPropertyManeger.time);
        }
        if (collision.gameObject.tag == "Reverse")
        {
            //Debug.Log();
            //Vector2 temp = new Vector2(cl.rb2D.velocity.x*-1,cl.rb2D.velocity.y*-1);
           // reverse = true;
            cl.rb2D.gravityScale = -17;
        }
        if (collision.gameObject.tag == "Fall")
        {
            //Debug.Log();
            //Vector2 temp = new Vector2(cl.rb2D.velocity.x*-1,cl.rb2D.velocity.y*-1);
            cl.rb2D.gravityScale = 17;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bouncy")
        {
            playerCollider.sharedMaterial.bounciness = 0;
            playerCollider.sharedMaterial = playerCollider.sharedMaterial;
        }
        if (collision.gameObject.tag == "Reverse")
        {
            //reverse = false;
            //Debug.Log();
            //Vector2 temp = new Vector2(cl.rb2D.velocity.x*-1,cl.rb2D.velocity.y*-1);
            cl.rb2D.gravityScale = 17;
        }

       // if (collision.gameObject.tag == "platform") {
         //   transform.parent = null;
       // }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Fall")
        {
            //Debug.Log();
            //Vector2 temp = new Vector2(cl.rb2D.velocity.x*-1,cl.rb2D.velocity.y*-1);
            cl.rb2D.gravityScale = 17;
        }
        if (collision.gameObject.tag == "Reverse")
        {
            //Debug.Log();
            //Vector2 temp = new Vector2(cl.rb2D.velocity.x*-1,cl.rb2D.velocity.y*-1);
            cl.rb2D.gravityScale = -17;
        }
    }


}
