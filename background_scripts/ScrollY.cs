using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollY : MonoBehaviour {

    public float backgroundSize {
        get {
            return layers[0].GetComponent<ScrollingBackground>().backgroundSizeY;
        }
    }

    public float paralaxSpeed;


    private GameObject[] layers {
        get {
            return cm.children;
        }
    }
    private int directionY {
        get {
            return pdm.directionY;
        }
    }
    private int size {
        get {
            return cm.counter;
        }
    }
    private ChildManeger cm;
    private PlayerPropertyManeger pdm;
    private Transform cameraTransform;
    public float veiwZone = 10;
    private int topIndex;
    private int bottomIndex;
    private float lastCameraY;
    private float deltaY;




    private void Start() {
        
        pdm = gameObject.GetComponent<PlayerPropertyManeger>();
        cm = gameObject.GetComponent<ChildManeger>();
        cameraTransform = Camera.main.transform;
        topIndex = 0;
        //rightIndex = size;
        lastCameraY = cameraTransform.position.y;
        //deltaX = cameraTransform.position.x - lastCameraX;
        //transform.position += Vector3.right * (deltaX * paralaxSpeed);
        //lastCameraX = cameraTransform.position.x;

    }
    private void Update() {
        deltaY = cameraTransform.position.y - lastCameraY;
        transform.position += Vector3.right * (deltaY * paralaxSpeed);
        lastCameraY = cameraTransform.position.y;
        if (cameraTransform.position.y < (layers[bottomIndex].transform.position.y) - veiwZone) {
            Debug.Log("1");
            scrollDown();
        }
        if (cameraTransform.position.y > (layers[topIndex].transform.position.y)) {
            Debug.Log("2");
            scrollUp();
        }
    }
    private void scrollDown() {
        
        //we have to do it this way so we don't mess with the y direction
        //layers[rightIndex].transform.position = Vector3.right * (layers[leftIndex].transform.position.x - backgroundSize);
        layers[topIndex].transform.position = new Vector3(layers[topIndex].transform.position.x, layers[bottomIndex].transform.position.y - backgroundSize);

        topIndex = bottomIndex;
        bottomIndex--;
        //so we dont get an index out of bouds exception
        if (bottomIndex < 0) {
            bottomIndex = layers.Length - 1;
        }
    }
    private void scrollUp() {
        
        //we have to do it this way so we don't mess with the y direction
        //layers[leftIndex].transform.position = Vector3.right * (layers[rightIndex].transform.position.x + backgroundSize);
        layers[bottomIndex].transform.position = new Vector3(layers[bottomIndex].transform.position.x, layers[topIndex].transform.position.y + backgroundSize);
        bottomIndex = topIndex;
        topIndex++;

        if (topIndex == layers.Length) {
            topIndex = 0;
        }
    }
}
