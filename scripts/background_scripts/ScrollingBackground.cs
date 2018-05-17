using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {


    public float backgroundSize {
        get {
            return layers[0].transform.localScale.x;
        }
    }
    public float backgroundSizeY {
        get {
            return layers[0].transform.localScale.y;
        }
    }

    public float paralaxSpeed;

    
    private GameObject[] layers {
        get {
            return cm.children;
        }
    }
    private int directionX {
        get {
            return pdm.directionX;
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
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;
    private float deltaX;




    private void Start() {
        
        pdm = gameObject.GetComponent<PlayerPropertyManeger>();
        cm = gameObject.GetComponent<ChildManeger>();
        cameraTransform = Camera.main.transform;
        leftIndex = 0;
        //rightIndex = size;
        lastCameraX = cameraTransform.position.x;
        //deltaX = cameraTransform.position.x - lastCameraX;
        //transform.position += Vector3.right * (deltaX * paralaxSpeed);
        //lastCameraX = cameraTransform.position.x;

    }
    private void Update() {
        deltaX = cameraTransform.position.x - lastCameraX;
        transform.position += Vector3.right * (deltaX * paralaxSpeed);
        lastCameraX = cameraTransform.position.x;
        if(cameraTransform.position.x < (layers[leftIndex].transform.position.x + veiwZone)) {
            scrollLeft();
        }
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - veiwZone)) {
            scrollRight();
        }
    }
    private void scrollLeft() {
        
        //we have to do it this way so we don't mess with the y direction
        //layers[rightIndex].transform.position = Vector3.right * (layers[leftIndex].transform.position.x - backgroundSize);
        layers[leftIndex].transform.position = new Vector3(((layers[leftIndex].transform.position.x - backgroundSize)), layers[leftIndex].transform.position.y);

        leftIndex = rightIndex;
        rightIndex--;
        //so we dont get an index out of bouds exception
        if(rightIndex < 0) {
            rightIndex = layers.Length - 1;
        }
    }
    private void scrollRight() {
        
        //we have to do it this way so we don't mess with the y direction
        //layers[leftIndex].transform.position = Vector3.right * (layers[rightIndex].transform.position.x + backgroundSize);
        layers[leftIndex].transform.position = new Vector3( ((layers[rightIndex].transform.position.x + backgroundSize)), layers[leftIndex].transform.position.y);
        rightIndex = leftIndex;
        leftIndex++;
        
        if (leftIndex == layers.Length) {
            leftIndex = 0;
        }
    }
}
