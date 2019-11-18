using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Vehicle
{
    public GameObject dresden;
    public float range;
    private Vector3 speed;
    [SerializeField]
    private float dragForce = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(dresden.transform.position, transform.position) < range)
        {
            CalculateSteeringForce();
        }
        else
        {
            //IDLE ANIMATION
        }
        vehiclePosition = transform.position;
    }

    public override void CalculateSteeringForce()
    {
        speed += Seek(dresden);
        speed *= dragForce;
        if (speed.magnitude <= 0.1)
            //if the velocity vector is to small to be noticable, stop moving
            speed = Vector3.zero;

        //max velocity
        speed = Vector3.ClampMagnitude(speed, maxSpeed);
        transform.position += speed * Time.deltaTime;
    }

}
