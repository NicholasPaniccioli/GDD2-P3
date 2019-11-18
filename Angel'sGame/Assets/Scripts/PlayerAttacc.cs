using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttacc : MonoBehaviour {

    [SerializeField]
    //  Explosion animation prefab
    private GameObject explosionInstance;
    private GameObject staff;
    [SerializeField]
    private float coolDown = 1;

    //  Used for cooldowns
    private float timeStamp;
    //  By what magnitude away from dresden should it be spawned?
    private float distanceFromDresden = 1;

    // Start is called before the first frame update
    void Start() {
        //  This NEEDS an explosion reference
        if(explosionInstance == null) {
            throw new UnassignedReferenceException("explosionInstance not assigned");
        }
        //  Grab the staff's visual location
        staff = gameObject.transform.GetChild(0).gameObject;
        //  Cooldowns, YEET
        timeStamp = Time.time;
    }

    // Update is called once per frame
    void Update() {
        //  If off cooldown and mouse button pressed, fire
        if(Input.GetMouseButtonDown(0) && timeStamp <= Time.time) {
            //  Reset cooldown
            timeStamp = Time.time + coolDown;

            //  Grab mouse pos and find where it should spawn
            Vector3 explosionPos = staff.GetComponent<Renderer>().bounds.center + staff.transform.right * distanceFromDresden;

            //  Spawn the EKSUPLOSION
            Instantiate(explosionInstance, explosionPos, Quaternion.identity);
        }
    }
}
