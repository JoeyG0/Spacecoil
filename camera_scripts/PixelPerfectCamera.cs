using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixelPerfectCamera : MonoBehaviour {
    public float pixelsToUnets = 1f;
    public float scale = 1f;

    //resolution of the gameboy advance 
    public Vector2 nativeRes = new Vector2(240,160);
	// awake is before start
	void Awake () {
        //get refrence to the camera
        var camera = GetComponent<Camera>();
        //make sure camera is in orthographic then scale  
        //if (camera.orthographic) {
            scale = Screen.height / nativeRes.y;
            pixelsToUnets *= scale;
            //we need half of the height for the scale because the 0 position is half of the screen in unity
            camera.orthographicSize = (Screen.height / 2.0f) / pixelsToUnets;
        //}
	}
	

}
