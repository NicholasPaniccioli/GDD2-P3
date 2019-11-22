using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Health health;
    ControlMeter controlMeter;
    [SerializeField]
    private GameObject basicAttack, fireball, staff;

    // Cool Downs
    public float fireBallCoolDown;  // cooldown for fireball ability
    public float healCoolDown;      // cooldown for heal ability
    public float basicCoolDown;     // cooldown for basic attack
    public float AOECoolDown;       // cooldown for basic attack

    // Time Stamps
    private float fireTimeStamp;    // time stamp for fireball
    private float healTimeStamp;    // time stamp for heal
    private float basicTimeStamp;   // time stamp for basic attack
    private float AOETimeStamp;     // time stamp for AOE attack

    // Bars
    private GameObject healBar;
    private GameObject fireBar;
    private GameObject AOEBar;

    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        controlMeter = gameObject.GetComponent<ControlMeter>();

        fireTimeStamp = Time.time;
        healTimeStamp = Time.time;
        basicTimeStamp = Time.time;
        AOETimeStamp = Time.time;

        healBar = GameObject.Find("CoolDownH");
        fireBar = GameObject.Find("CoolDownF");
    }

    // Update is called once per frame
    void Update()
    {
        // left click
        if (Input.GetMouseButtonDown(0) && basicTimeStamp <= Time.time)//basic
        {
            Instantiate<GameObject>(basicAttack, gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.center + gameObject.transform.GetChild(1).right, Quaternion.identity);
            basicTimeStamp = Time.time + basicCoolDown;
        }
        
        // right click
        if (Input.GetMouseButtonDown(1) && fireTimeStamp <= Time.time)// fireball
        {
            Instantiate<GameObject>(fireball, gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.center, staff.transform.rotation).GetComponent<Fireball>().staff = staff;
            gameObject.GetComponent<ControlMeter>().IncreaseControl(5f);
            fireTimeStamp = Time.time + fireBallCoolDown;
        }

        // Q
        if (Input.GetKeyDown(KeyCode.Q) && healTimeStamp <= Time.time)    // heal
        {
            gameObject.GetComponent<Health>().Heal(10f);   // heal the player by 10 points;
            gameObject.GetComponent<ControlMeter>().IncreaseControl(5f);
            healTimeStamp = Time.time + healCoolDown;
        }

        // E
        if (Input.GetKeyDown(KeyCode.E) && AOETimeStamp <= Time.time)    // AOE
        {
            Debug.Log("AOE Used");
            // add AOE attack code here
        }

        // Bars Code
        if (healTimeStamp <= Time.time) // heal
        {
            healBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
        else
        { 
            healBar.transform.localScale = new Vector3((healTimeStamp - Time.time) / healCoolDown, 1.0f, 1.0f);
        }

        if (fireTimeStamp <= Time.time) // fireball
        {
            fireBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
        else
        {
            fireBar.transform.localScale = new Vector3((fireTimeStamp - Time.time) / fireBallCoolDown, 1.0f, 1.0f);
        }

        //if (AOETimeStamp <= Time.time)  // AOE
        //{
        //    AOEBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        //}
        //else
        //{
        //    AOEBar.transform.localScale = new Vector3((AOETimeStamp - Time.time) / AOECoolDown, 1.0f, 1.0f);
        //}
    }
}
