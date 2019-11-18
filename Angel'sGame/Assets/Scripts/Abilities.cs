using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Health health;
    ControlMeter controlMeter;
    private GameObject staff;
    public Vector3 fireBallVelocity;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        controlMeter = gameObject.GetComponent<ControlMeter>();
        staff = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        // Heal Ability
        if(Input.GetKeyDown(KeyCode.Alpha4))    // if the player presses the '4' key above the letters (not the numpad)
        {
            health.Heal(10f);   // heal the player by 10 points;
            controlMeter.IncreaseControl(5f);   
        }

        //if (Input.GetKeyDown(KeyCode.Alpha3))
        //{
        //    ShootFireball();
        //}
    }

    void ShootFireball(GameObject fireball)
    {
        GameObject newFireball = Instantiate(fireball, new Vector3(staff.GetComponent<Renderer>().bounds.center.x, staff.GetComponent<Renderer>().bounds.center.y), Quaternion.identity);
        newFireball.transform.position += fireBallVelocity * Time.deltaTime;
    }
}
