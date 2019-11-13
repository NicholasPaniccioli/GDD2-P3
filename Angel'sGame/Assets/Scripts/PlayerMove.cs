using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 velocity;
    [SerializeField]
    private float dragForce = 0.8f, maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        //Move up or down
        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector3.up;
            transform.rotation = Quaternion.Euler(45, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector3.down;
            transform.rotation = Quaternion.Euler(-45, 180, 0);
        }
        //move right or left
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left;
            transform.rotation = Quaternion.Euler(0, 135, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right;
            transform.rotation = Quaternion.Euler(0, -45, 0);
        }
        //deceleration
        velocity *= dragForce;
        if (velocity.magnitude <= 0.1)
            //if the velocity vector is to small to be noticable, stop moving
            velocity = Vector3.zero;

        //max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
    }
}
