using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Health health;
    ControlMeter controlMeter;
    [SerializeField]
    private GameObject basicAttack, fireball, staff;

    public float fireBallCoolDown;  // cooldown for fireball ability
    public float healCoolDown;      // cooldown for heal ability
    private float fireTimeStamp;    // time stamp for fireball
    private float healTimeStamp;    // time stamp for heal
    private GameObject healBar;
    private GameObject fireBar;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        controlMeter = gameObject.GetComponent<ControlMeter>();
        fireTimeStamp = Time.time;
        healTimeStamp = Time.time;
        healBar = GameObject.Find("CoolDownH");
        fireBar = GameObject.Find("CoolDownF");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//basic
        {
            Instantiate<GameObject>(basicAttack, gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.center + gameObject.transform.GetChild(1).right, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && fireTimeStamp <= Time.time)// fireball
        {
            Instantiate<GameObject>(fireball, gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.center, Quaternion.identity).GetComponent<Fireball>().staff = staff;
            gameObject.GetComponent<ControlMeter>().IncreaseControl(5f);
            fireTimeStamp = Time.time + fireBallCoolDown;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && healTimeStamp <= Time.time)    // if the player presses the '4' key above the letters (not the numpad), heal
        {
            gameObject.GetComponent<Health>().Heal(10f);   // heal the player by 10 points;
            gameObject.GetComponent<ControlMeter>().IncreaseControl(5f);
            healTimeStamp = Time.time + healCoolDown;
        }

        if(healTimeStamp <= Time.time)
        {
            healBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
        else
        { 
            healBar.transform.localScale = new Vector3((healTimeStamp - Time.time) / healCoolDown, 1.0f, 1.0f);
        }

        if (fireTimeStamp <= Time.time)
        {
            fireBar.transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
        }
        else
        {
            fireBar.transform.localScale = new Vector3((fireTimeStamp - Time.time) / fireBallCoolDown, 1.0f, 1.0f);
        }
    }
}
