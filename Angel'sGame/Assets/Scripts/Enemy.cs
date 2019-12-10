using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {
    protected Vector3 velocity, EnemyPosition;//these needs to be accessed by its children
    [SerializeField]
    protected float maxSpeed, maxHealth=30,iFrameDuration=1;
    [SerializeField]
    private CombatManager combatManager;
    protected float health;
    protected float iFrameTimeStamp;
    public float IFrameTimeStamp { get { return iFrameTimeStamp; } }
    protected bool intersecting;

    public bool Intersecting
    {
        get { return intersecting; }
        set { intersecting = value; }
    }

    // Use this for initialization
    protected void Start() {
        intersecting = false;
        health = maxHealth;
        iFrameTimeStamp = Time.time;
        if (combatManager == null)
            combatManager = GameObject.Find("Combat Manager").GetComponent<CombatManager>() ;
    }

    // Update is called once per frame
    protected void Update() {
        //  Clear iFrame display if duration ends
        if(iFrameTimeStamp <= Time.time) {
            gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
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

    public void RandomPosition()
    {
        EnemyPosition = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), 0));
        EnemyPosition.z = 0;
    }

    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Die();
        }
        
        iFrameTimeStamp = Time.time + iFrameDuration;
        gameObject.transform.GetChild(1).gameObject.GetComponent<Renderer>().material.color = Color.red;    // flash red when hit
    }

    /// <summary>
    /// Maybe increase score later on or some death animation
    /// </summary>
    protected void Die() {
        combatManager.removeEnemy(this);
        Destroy(gameObject);
    }

    protected void Heal(float amount) {
        health += amount;
        if (health > maxHealth)
            health = maxHealth;
    }

    public abstract void CalculateSteeringForce();
}