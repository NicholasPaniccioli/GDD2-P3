using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vampire : Enemy
{
    private GameObject dresden;
    public float range;
    [SerializeField]
    private float dragForce = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        dresden = GameObject.Find("Dresden");
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(dresden.transform.position, transform.position) < range && !intersecting)
        {
            CalculateSteeringForce();
        }
        else
        {
            //IDLE ANIMATION
        }
        EnemyPosition = transform.position;
    }

    public override void CalculateSteeringForce()
    {
        velocity += Seek(dresden);
        velocity *= dragForce;
        if (velocity.magnitude <= 0.1)
            //if the velocity vector is to small to be noticable, stop moving
            velocity = Vector3.zero;

        //max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
    }

}
