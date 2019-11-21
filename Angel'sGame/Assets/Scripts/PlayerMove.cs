using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 demonMove;
    private float demonAngle;
    private GameObject dresden;
    private CircleCollider2D wallCollider;
    [SerializeField]
    private float dragForce = 0.8f, maxSpeed;

    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        dresden = gameObject.transform.GetChild(0).gameObject;
        wallCollider = GetComponentInChildren<CircleCollider2D>();
        demonMove = Vector3.zero;
        demonAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Move up or down
        if (Input.GetKey(KeyCode.W))
        {
            velocity += Vector3.up;
            dresden.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocity += Vector3.down;
            dresden.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //move right or left
        if (Input.GetKey(KeyCode.A))
        {
            velocity += Vector3.left;
            dresden.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            velocity += Vector3.right;
            dresden.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (false/*player.control > 75*/){//Once Jacob is done with making a player script, check the control variable.  Until then, this will never trigger
            if (demonMove == Vector3.zero){
                demonMove = velocity;
            }
            else{
                int newAngle = (int)Random.Range(demonAngle - 5, demonAngle + 5);
                demonMove = Quaternion.AngleAxis(newAngle, Vector3.forward) * demonMove;
                demonAngle = newAngle;
                velocity += demonMove;
            }
        }
        //deceleration
        velocity *= dragForce;
        if (velocity.magnitude <= 0.1)
            //if the velocity vector is to small to be noticable, stop moving
            velocity = Vector3.zero;

        //max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        wallCollider.transform.position += velocity * Time.deltaTime;
        transform.position = wallCollider.transform.position;
        wallCollider.transform.position = transform.position;
    }
}
