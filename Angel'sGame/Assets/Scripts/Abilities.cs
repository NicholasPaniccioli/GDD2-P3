using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Health health;
    ControlMeter controlMeter;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        controlMeter = gameObject.GetComponent<ControlMeter>();
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
    }
}
