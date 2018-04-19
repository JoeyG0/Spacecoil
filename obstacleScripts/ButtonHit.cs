using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHit : MonoBehaviour
{
    public bool wasHit;
    SpriteRenderer spritRenderer;
    public Sprite unHitButton;
    public Sprite hitButton;
    // Use this for initialization
    void Start()
    {
        
        spritRenderer = GetComponent<SpriteRenderer>();
        wasHit = false;


    }

    // Update is called once per frame
    void Update()
    {
        if(wasHit == true)
        {
            //spritRenderer.color = Color.green;
            spritRenderer.sprite = hitButton;
        }
        else {
            spritRenderer.sprite = unHitButton;
        }

    }
}
