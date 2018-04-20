using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildManeger : MonoBehaviour {

    public GameObject[] children;
    public int transformCount;
    public int counter;
	// Use this for initialization
	void Start () {
        transformCount = transform.childCount;
        children = new GameObject[transformCount];
        counter = 0;
        foreach(Transform i in gameObject.transform) {
            children[counter] = i.gameObject;
            counter++;
        }
        

	}
	
	
}
