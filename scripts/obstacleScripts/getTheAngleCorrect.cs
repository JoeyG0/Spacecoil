using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Broken, right now do it manually
public class getTheAngleCorrect : MonoBehaviour {


    private ChildManeger cm;

    private GameObject[] layers {
        get {
            return cm.children;
        }
    }
    GameObject topIndex {
        get {
           return layers[0];
        }
    }
    GameObject bottomIndex {
        get {
            return layers[1];
        }
    }
    private int size {
        get {
            return cm.counter;
        }
    }
    private void Awake() {
        cm = gameObject.GetComponent<ChildManeger>();
    }

    void Start () {
        //make sure that the first 2 children of the holder game object are the two indexes that you dont want the thing to go beyond
        Debug.Log(size);
        
       // GameObject target = layers[2];
       // var angle = target.GetComponent<Transform>().rotation;

        //topIndex.GetComponent<Transform>().rotation = angle;
       // bottomIndex.GetComponent<Transform>().rotation = angle;
    }


}
