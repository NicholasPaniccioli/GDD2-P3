using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    private float initializationTime;

    // Start is called before the first frame update
    void Start() {
        initializationTime = Time.timeSinceLevelLoad;
    }

    // Update is called once per frame
    void Update() {
        float timeAlive = Time.timeSinceLevelLoad - initializationTime;
        if(timeAlive > 0.7f) {
            Destroy(gameObject);
        }
    }
}
