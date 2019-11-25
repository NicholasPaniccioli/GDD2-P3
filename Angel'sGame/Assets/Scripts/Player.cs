using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //  Rotation
    private bool debug = false;
    private GameObject staff;

    //  Movement
    private Vector3 velocity;
    private GameObject dresden;
    [Header("Movement")]
    [SerializeField]
    private float dragForce = 0.8f, maxSpeed = 3,speedMod = 1;

    //  Stats
    [Header("Stats")]
    [SerializeField]
    private float maxHealth = 100, iFrameDuration = 1;
    [SerializeField]
    private GameObject healthBar;
    private Renderer dresdenRenderer;
    private float health;
    private float iFrameTimeStamp;
    public float IFrameTimeStamp { get { return iFrameTimeStamp; } }
    public float Health { get { return health; } }

    void Start() {
        //  Rotation
        staff = gameObject.transform.GetChild(1).gameObject;

        //  Movement
        velocity = Vector3.zero;
        dresden = gameObject.transform.GetChild(0).gameObject;

        //  Stats
        health = maxHealth;
        dresdenRenderer = gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        if (healthBar == null)
            throw new MissingReferenceException("healthBar object not assigned");
        iFrameTimeStamp = Time.time;
    }

    void Update() {
        Rotate();
        HandleMovement();
        UpdateHealth();
    }

    /// <summary>
    /// Rotate Dresden's staff
    /// </summary>
    private void Rotate() {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleOfRotation = Mathf.Rad2Deg * Mathf.Atan2(mouseWorldPos.y - staff.transform.position.y, mouseWorldPos.x - staff.transform.position.x);
        staff.transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        if (Input.GetKeyDown(KeyCode.P))
            debug = !debug;
        if (debug)
            DrawDebug();
    }

    /// <summary>
    /// Draw debug lines for Dresden
    /// </summary>
    private void DrawDebug() {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);
    }

    /// <summary>
    /// Take button input and adjust velocity accordingly
    /// </summary>
    private void HandleMovement() {
        //  Up/down
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
            velocity += Vector3.up * speedMod;
            dresden.transform.rotation = Quaternion.Euler(0, 0, 0);
        } else if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
            velocity += Vector3.down * speedMod;
            dresden.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        //  Left/right
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
            velocity += Vector3.left * speedMod;
            dresden.transform.rotation = Quaternion.Euler(0, 180, 0);
        } else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
            velocity += Vector3.right * speedMod;
            dresden.transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        //  Deceleration
        velocity *= dragForce;
        if (velocity.magnitude <= 0.1)
            velocity = Vector3.zero;

        //  Max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        transform.position += velocity * Time.deltaTime;
    }

    /// <summary>
    /// Health update
    /// </summary>
    private void UpdateHealth() {
        healthBar.transform.localScale = new Vector3
            (health / maxHealth, 
            healthBar.transform.localScale.y, 
            healthBar.transform.localScale.z);
        if (iFrameTimeStamp <= Time.time)
            dresdenRenderer.material.color = Color.white;
    }

    /// <summary>
    /// Subtract damage amount from total health
    /// </summary>
    /// <param name="damage">Damage to be dealt</param>
    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            Die();
        iFrameTimeStamp = Time.time + iFrameDuration;
    }

    /// <summary>
    /// 死ぬ
    /// </summary>
    private void Die() {
        Destroy(gameObject);
    }

    /// <summary>
    /// Heals for given amount
    /// </summary>
    /// <param name="healAmount">Amount to be healed</param>
    public void Heal(float healAmount) {
        health += healAmount;
        if (health > maxHealth)
            health = maxHealth;
    }
}
