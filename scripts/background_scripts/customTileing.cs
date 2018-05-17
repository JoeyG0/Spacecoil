using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customTileing : MonoBehaviour {

    public float x = 1f;
    public float y = 1f;
    Renderer ren;
    //Material mat;

	// Use this for initialization
	void Start () {
        ren = GetComponent<Renderer>();
        ren.material.mainTextureScale = new Vector2(x, y);
        //mat = GetComponent<Material>();
    }
	
	// Update is called once per frame
	//void Update () {
      //  ren.material.mainTextureScale = new Vector2(x, y);
        //mat.mainTextureScale = new Vector2(x, y);
	//}
}
