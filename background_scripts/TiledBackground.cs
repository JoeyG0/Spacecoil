using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiledBackground : MonoBehaviour {

    //take size of tile try to figure out how many tiles fit within the current screen width and height. take that value and change the materials texture to match that
    // Use this for initialization
    public int textureSize = 256;
    public bool scaleableHorisontal = true;
    public bool scaleableVertical = true;
    PixelPerfectCamera ppx;


    void Start() {
        ppx = Camera.main.GetComponent<PixelPerfectCamera>();
        //using Mathf.ceil ensures that we get the overage that we need so we dont get any blank spaces in the game. Mathf.Ceil rounds things up
        var newWidth = !scaleableHorisontal ? 1 : Mathf.Ceil(Screen.width / (textureSize * ppx.scale));
        var newHeight = !scaleableVertical ? 1 : Mathf.Ceil(Screen.height / (textureSize * ppx.scale));

        //set the scale
        transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);
        //tell the matarial that it has a new texture scale
        GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }
}

/*
* 
* 
* 
* 
*     public int textureSize = 256;
public bool scaleableHorisontal = true;
public bool scaleableVertical = true;


void Start () {
    //using Mathf.ceil ensures that we get the overage that we need so we dont get any blank spaces in the game. Mathf.Ceil rounds things up
    var newWidth = !scaleableHorisontal ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
    var newHeight = !scaleableVertical ? 1 : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));

    //set the scale
    transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);
    //tell the matarial that it has a new texture scale
    GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
}
    public int textureSize = 256;
public bool scaleableHorisontal = true;
public bool scaleableVertical = true;
private ChildManeger cm;

private GameObject[] children {
    get {
        return cm.children;
    }
}
private int count {
    get {
        return cm.transformCount;
    }
}

void Awake() {
    cm = gameObject.GetComponent<ChildManeger>();
}
void Start() {
    //using Mathf.ceil ensures that we get the overage that we need so we dont get any blank spaces in the game. Mathf.Ceil rounds things up
    var newWidth = !scaleableHorisontal ? 1 : Mathf.Ceil(Screen.width / (textureSize * PixelPerfectCamera.scale));
    var newHeight = !scaleableVertical ? 1 : Mathf.Ceil(Screen.height / (textureSize * PixelPerfectCamera.scale));
    for (int i = 0; i < count; i++){
        children[i].transform.localScale = new Vector3(newWidth * textureSize, newHeight * textureSize, 1);
        children[i].GetComponent<Renderer>().material.mainTextureScale = new Vector3(newWidth, newHeight, 1);
    }
    //set the scale

}
* 
* 
* 
* 
* 
* 
* 
* 
*/

