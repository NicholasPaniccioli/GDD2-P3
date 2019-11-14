using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seekingAI : Vehicle
{
    public GameObject dresden;
    public float range;
    public Vector3 velocity;
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
    }

    public override void CalculateSteeringForce()
    {

    }

}
