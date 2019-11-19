using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public Vector3 EnemyPosition;
    public Vector3 velocity;
    public Vector3 acceleration;
    public Vector3 direction;

    public float mass;

    public float friction;
    public float maxSpeed;
    public float lookAhead;

    public Color gizmoColor;
    protected Bounds bounds;

    protected Vector3 forward;
    protected Vector3 right;

    private BoundingSphere personalSpace;
    private BoundingSphere futureSpace;
    //public Vector3 gravity;
    //public Vector3 wind;

    //public bool isFriction;
    //public bool isGravity;
    //public bool isWind;

    // Use this for initialization
    protected void Start()
    {
        lookAhead = 5;
        bounds = GetComponent<BoxCollider>().bounds;
        EnemyPosition = transform.position;
        gizmoColor = Color.white;
        SetBoundingSpheres();
    }

    // Update is called once per frame
    protected void Update()
    {
        //if (isFriction)
        //{
        //    ApplyFriction(friction);
        //}
        /*if (isGravity)
        {
            ApplyGravity(gravity);
        }
        if (isWind)
        {
            ApplyForce(wind);
        }*/
        CalculateSteeringForce();

        velocity += acceleration * Time.deltaTime;
        EnemyPosition += velocity * Time.deltaTime;

        direction = velocity.normalized;
        acceleration = Vector3.zero;


        EnemyPosition.y = gameObject.GetComponentInChildren<Transform>().position.y; //Every Enemy has an empty component at their base to keep them bound to the ground

        transform.position = EnemyPosition;
        transform.rotation = Quaternion.Euler(transform.rotation.x, -Mathf.Rad2Deg * Mathf.Atan2(direction.z, direction.x), transform.rotation.z);
        bounds = GetComponent<BoxCollider>().bounds;

        forward = transform.forward;
        right = transform.right;
        SetBoundingSpheres();
    }

    /// <summary>
    /// sets the acceleration equal to the given force, divided by the objects mass
    /// </summary>
    /// <param name="force"></param>
    public void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    /// <summary>
    /// adjusts the acceleration to account for gravity, since gravity isn't affected by mass.
    /// </summary>
    /// <param name="force"></param>
    public void ApplyGravity(Vector3 force)
    {
        acceleration += force;
    }

    /// <summary>
    /// adjusts the acceleration to account for friction, since forces can't be easy and all use one method
    /// </summary>
    /// <param name="force"></param>
    public void ApplyFriction(float coeff)
    {
        Vector3 friction = velocity * -1;
        friction.Normalize();
        friction *= coeff;
        acceleration += friction;
    }

    /// <summary>
    /// seeks the specified position
    /// </summary>
    /// <param name="targetPos"></param>
    public Vector3 Seek(Vector3 targetPos)
    {
        Vector3 desiredVelocity = targetPos - EnemyPosition;

        desiredVelocity = desiredVelocity.normalized * maxSpeed;

        Vector3 seekingForce = desiredVelocity - velocity;

        return seekingForce;
    }

    /// <summary>
    /// seeks the specified object
    /// </summary>
    /// <param name="target"></param>
    public Vector3 Seek(GameObject target)
    {
        return Seek(target.transform.position);
    }

    /// <summary>
    /// applies force from mouse
    /// </summary>
    public Vector3 Flee(Vector3 targetPos)
    {
        Vector3 desiredVelocity = EnemyPosition - targetPos;
        desiredVelocity = desiredVelocity.normalized * maxSpeed;
        Vector3 seekingForce = desiredVelocity - velocity;
        return seekingForce;
    }

    /// <summary>
    /// applies force from mouse
    /// </summary>
    public Vector3 Flee(GameObject target)
    {
        return Flee(target.transform.position);
    }

    /// <summary>
    /// causes Enemy to rebound
    /// </summary>
    public void ReturnToBounds(Bounds bounds)
    {
        ApplyForce(Seek(bounds.center) * 2);
    }

    /// <summary>
    /// Checks if the Enemy is out of bounds
    /// </summary>
    public bool InBounds(Bounds bounds)
    {
        if (EnemyPosition.x > bounds.max.x || EnemyPosition.x < bounds.min.x || EnemyPosition.y > bounds.max.y || EnemyPosition.y < bounds.min.y)
        {
            return true;
        }
        return true;
    }

    public void RandomPosition()
    {
        EnemyPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0));
        EnemyPosition.z = 0;
    }

    public bool AABB(Bounds otherBounds)
    {
        bool collision = (bounds.max.x < otherBounds.min.x) || (otherBounds.max.x < bounds.min.x) || (bounds.max.z < otherBounds.min.z) || (otherBounds.max.z < bounds.min.z);
        return !collision;
    }

    public abstract void CalculateSteeringForce();

    public void OnDrawGizmos()
    {
        Gizmos.color = gizmoColor;
        Gizmos.DrawLine(bounds.center, bounds.center + Vector3.up);
        Gizmos.color = new Color(255, 69, 0);
        Gizmos.DrawLine(bounds.center, bounds.center + forward);
        Gizmos.color = new Color(128, 0, 128);
        Gizmos.DrawLine(bounds.center, bounds.center + right);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(personalSpace.position, personalSpace.radius);
        Gizmos.DrawWireSphere(futureSpace.position, futureSpace.radius);
    }

    /// <summary>
    /// sets the bounding sphere for obstacle avoidance.  The sphere is centered around the ground prefab in the Enemy, 
    /// with a radius equal to the max of the bounding box
    /// </summary>
    private void SetBoundingSpheres()
    {
        //sets the first sphere on the object
        Vector3 center = new Vector3(transform.position.x, 0, transform.position.z);
        float radius = Vector3.Distance(transform.position, bounds.max);
        personalSpace = new BoundingSphere(center, radius);

        //sets a second sphere 3 seconds ahead
        center = center + (direction * lookAhead);
        center.y = 0;
        futureSpace = new BoundingSphere(center, radius);
    }


    public Vector3 ObstacleAvoidance(List<GameObject> obstacles)
    {
        for (int x = 0; x < obstacles.Count; x++)
        {
            Vector3 distance = transform.position - obstacles[x].transform.position;
            float dot = Vector3.Dot(right, distance);
            if (Mathf.Abs(dot) < distance.magnitude)
            {
                if (dot > 0)
                {
                    Vector3 returnVector = right * maxSpeed;

                    return returnVector * (lookAhead / distance.magnitude);
                }
                else
                {
                    Vector3 returnVector = -right * maxSpeed;

                    return returnVector * (lookAhead / distance.magnitude);
                }
            }
        }
        return Vector3.zero;
    }
}