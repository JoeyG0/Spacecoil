using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class driftMenue : MonoBehaviour {
    public Material menueMaterial;
    public Vector2 speed = Vector2.zero;
    
    private Vector2 offset = Vector2.zero;
    //public Vector2 ofset = new Vector2(speed, 0);

	// Use this for initialization
	void Start () {
        offset = menueMaterial.GetTextureOffset("_MainTex");
	}
	
	// Update is called once per frame
	void Update () {
        offset += speed * Time.deltaTime;

        menueMaterial.SetTextureOffset("_MainTex", offset);
	}
}