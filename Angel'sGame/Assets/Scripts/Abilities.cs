using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    Health health;
    ControlMeter controlMeter;
    [SerializeField]
    private GameObject basicAttack, fireball;
    // Start is called before the first frame update
    void Start()
    {
        health = gameObject.GetComponent<Health>();
        controlMeter = gameObject.GetComponent<ControlMeter>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))//basic
        {
            Instantiate<GameObject>(basicAttack, gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.center + gameObject.transform.GetChild(1).right, Quaternion.identity);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))// fireball
        {
            Instantiate<GameObject>(fireball, gameObject.transform.GetChild(1).GetComponent<Renderer>().bounds.center, Quaternion.identity);
            gameObject.GetComponent<ControlMeter>().IncreaseControl(5f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))    // if the player presses the '4' key above the letters (not the numpad), heal
        {
            gameObject.GetComponent<Health>().Heal(10f);   // heal the player by 10 points;
            gameObject.GetComponent<ControlMeter>().IncreaseControl(5f);
        }
    }
}
