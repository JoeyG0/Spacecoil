using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    //the name Destroy may be a tad misleading, because we use object pooling instead of destroying them 

    private void OnEnable() {
        Invoke("deactivate", 2f);
        GetComponent<BulletBehavior>().hit = false;
    }

    public void deactivate() {
        gameObject.SetActive(false);
    }

    private void OnDisable() {
        CancelInvoke();
    }


}
