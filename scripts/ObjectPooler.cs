using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour {

    public static ObjectPooler current;
    public GameObject pooledObject;
    public int pooledAmount = 20;
    public bool willGrow = true;

    List<GameObject> pooledObjects;


    private void Awake() {

        current = this;
    }
    void Start () {

        pooledObjects = new List<GameObject>();
        for (int i = 0; i < pooledAmount; i++) {

            GameObject o = (GameObject)Instantiate(pooledObject);
            o.SetActive(false);
            pooledObjects.Add(o);
        }
    }

    public GameObject GetObject() {

        for (int i = 0; i < pooledAmount; i++) {

            if (!pooledObjects[i].activeInHierarchy) {

                return pooledObjects[i];
            }
        }

        if (willGrow) {

            GameObject o = (GameObject)Instantiate(pooledObject);
            pooledObjects.Add(o);
            return o;
        }
        else {
            return null;
        }
    }
	

}
