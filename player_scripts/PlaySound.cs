using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class PlaySound : MonoBehaviour {
    private PlayerMove cl;
    AudioSource[] sound;
    SpriteRenderer gun;

    public Sprite idle;
    public Sprite shoot;

    // Use this for initialization
    void Start () {
        
        cl = GameObject.Find("player").GetComponent<PlayerMove>();


        sound = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        if (cl.clicked == true)
        {
            cl.clicked = false;
            sound[0].PlayOneShot(sound[0].clip);
        }

        if(cl.boosting == true)
        {
            if(!sound[1].isPlaying){
                sound[1].Play();
            }
        }
        else
        {
            sound[1].Stop();
        }
    }
}
