using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : Enemy
{

    public GameObject dresden;
    public GameObject[] path;
    int wavepoint;
    private Vector3 desiredVel;
    bool isOn;
    public float range;
    public float dragForce;

    // Use this for initialization
    void Start()
    {
        EnemyPosition = transform.position;
        wavepoint = 0;
        isOn = true;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateSteeringForce();
        EnemyPosition = transform.position;
    }

    /// <summary>
    /// calculates the movement of the follower
    /// </summary>
    public override void CalculateSteeringForce()
    {
        if (!intersecting)
        {
            if (Target(dresden))
            {
                velocity += Seek(dresden);
            }
            else
            {
                velocity += PathFollow(wavepoint);
            }
        }
        velocity *= dragForce;
        if (velocity.magnitude <= 0.1)
            //if the velocity vector is to small to be noticable, stop moving
            velocity = Vector3.zero;

        //max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
    }

    public Vector3 PathFollow(int currentPoint)
    {
        if (Vector3.Distance(transform.position, path[currentPoint].transform.position) < 5)
        {
            if (currentPoint != path.Length - 1)
            {
                wavepoint++;
            }
            else
            {
                wavepoint = 0;
            }
        }

        return Seek(path[currentPoint]);
    }

    public bool Target(GameObject dresden)
    {
        if (Vector3.Distance(dresden.transform.position, transform.position) < range)
        {
            return true;
        }
        return false;
    }

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    if (isOn)
    //    {
    //        for (int x = 0; x < path.Length - 1; x++)
    //        {
    //            Gizmos.DrawRay(path[x].transform.position, path[x + 1].transform.position - path[x].transform.position);
    //        }
    //        Gizmos.DrawRay(path[path.Length - 1].transform.position, path[0].transform.position - path[path.Length - 1].transform.position);
    //    }
    //}
}
