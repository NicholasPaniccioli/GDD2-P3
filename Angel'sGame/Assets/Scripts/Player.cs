using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //  Animations
    private Animator animator;
    private bool isRunning;

    //  Rotation
    private bool debug = false;
    private GameObject staff;

    //  Movement
    private Vector3 velocity;
    private GameObject dresden;
    private CircleCollider2D wallCollider;
    [Header("Movement")]
    [SerializeField]
    private float dragForce = 0.8f;
    [SerializeField]
    private float maxSpeed = 3,speedMod = 1;

    //Demon movement control
    private Vector3 demonMove;
    private float demonAngleMove;

    //demon staff control
    private float demonAngleStaff, perlinY;
    private bool flag;//flag variable to determine if demonAngleStaff needs to be set

    //  Stats
    [Header("Stats")]
    [SerializeField]
    private float maxHealth = 100;
    [SerializeField]
    private float maxControl = 100, bufferPoint = 25, controlTimer, iFrameDuration = 1;
    private float control;
    [SerializeField]
    private GameObject healthBar, controlBar;
    private Renderer dresdenRenderer;
    private float health;
    private float iFrameTimeStamp;
    public float IFrameTimeStamp { get { return iFrameTimeStamp; } }
    public float Health { get { return health; } }

    void Start() {
        //  Animations
        animator = gameObject.GetComponent<Animator>();
        isRunning = false;

        //  Rotation
        staff = gameObject.transform.Find("Staff").gameObject;

        //  Movement
        velocity = Vector3.zero;
        wallCollider = GetComponentInChildren<CircleCollider2D>();

        //Demon Control
        demonMove = Vector3.zero;
        demonAngleMove = 0;

        //  Stats
        health = maxHealth;
        dresdenRenderer = gameObject.transform.Find("dresden").gameObject.GetComponent<Renderer>();
        if (healthBar == null)
            throw new MissingReferenceException("healthBar object not assigned");
        iFrameTimeStamp = Time.time;
    }

    void Update() {
        Rotate();
        HandleMovement();
        UpdateHealth();
        UpdateControl();
    }

    /// <summary>
    /// Rotate Dresden's staff
    /// </summary>
    private void Rotate() {
        if (control < 75)
        {
            flag = false;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float angleOfRotation = Mathf.Rad2Deg * Mathf.Atan2(mouseWorldPos.y - staff.transform.position.y, mouseWorldPos.x - staff.transform.position.x);
            staff.transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

            if (Input.GetKeyDown(KeyCode.P))
                debug = !debug;
            if (debug)
                DrawDebug();
        }
        else
        {
            if (!flag)
            {
                demonAngleStaff = staff.transform.rotation.z;//sets the starting angle of the staff to the current angle
                flag = true;
                perlinY = Random.Range(0.0f, 100.0f);//gets a random start so the perlin noise is different each time
            }
            float newAngle = (Mathf.PerlinNoise(Time.time * 1.0f, perlinY) * 5) - 2.5f;
            demonAngleStaff += newAngle;
            staff.transform.rotation = Quaternion.Euler(0, 0, demonAngleStaff);
            flag = true;
        }
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
    private void HandleMovement()
    {
        //  Up/down
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            velocity += Vector3.up * speedMod;
            transform.rotation = Quaternion.Euler(-15, 0, 0);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            velocity += Vector3.down * speedMod;
            transform.rotation = Quaternion.Euler(15, 180, 0);
        }
        //  Left/right
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            velocity += Vector3.left * speedMod;
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            velocity += Vector3.right * speedMod;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        if (control >= 25)
        {
            if (demonMove == Vector3.zero)
            {
                if (velocity == Vector3.zero)//makes sure the demon takes control, even if the player isn't moving
                    demonMove = Vector3.right;
                else
                    demonMove = velocity;
            }
            else
            {
                int newAngle = (int)Random.Range(demonAngleMove - 5, demonAngleMove + 5);
                demonMove = Quaternion.AngleAxis(newAngle, Vector3.forward) * demonMove;
                demonAngleMove = newAngle;
                velocity += demonMove;
            }
        }
        //  Deceleration
        velocity *= dragForce;
        if (velocity.magnitude <= 0.1)
            velocity = Vector3.zero;

        //  Max velocity
        velocity = Vector3.ClampMagnitude(velocity, maxSpeed);
        wallCollider.transform.position += velocity * Time.deltaTime;
        transform.position = wallCollider.transform.position;
        wallCollider.transform.position = transform.position;


        if (velocity.magnitude > 0)
        {
            if (!isRunning)
            {
                animator.SetBool("isRunning", true);
                isRunning = true;
            }
        }
        else
        {
            if (isRunning)
            {
                animator.SetBool("isRunning", false);
                isRunning = false;
            }
        }

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
    /// Control update
    /// </summary>
    private void UpdateControl() {
        controlBar.transform.localScale = new Vector3
            (control / maxControl, 
            controlBar.transform.localScale.y,
            controlBar.transform.localScale.z);
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
        dresdenRenderer.material.color = Color.red;    // flash red when hit
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

    // Increase the amount of Control
    public void IncreaseControl(float amount)
    {
        if (control + amount > maxControl)
        {
            control = maxControl;  // prevent Control from going below 0
            // Game Over because the player lost control
        }
        else
        {
            control += amount;    // decrease Control by amount
        }
        Debug.Log("Control Amount: " + control);
    }

    // Second Option to Decrease Control (probably the better version)
    public void DecreseControl2(float amount)
    {
        if (control % bufferPoint > 0)
        {
            control--;
        }

        Debug.Log("Control Amount: " + control);
    }
}
