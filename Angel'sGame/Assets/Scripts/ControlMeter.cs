using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlMeter : MonoBehaviour
{
    public float controlAmount;    // the current amount of Control
    public float minControl;       // the minimum amount of Control
    public float maxControl;       // the maximum amount of Control possible
    public float bufferPoint;      // intervals of point of no return
    private GameObject controlBar;

    // Start is called before the first frame update
    void Start()
    {
        controlAmount = 0f; // Control starts at 0
        maxControl = 100f;  // Max Control is 100
        controlBar = GameObject.Find("BarBar");
    }

    // Update is called once per frame
    void Update()
    {
        controlBar.transform.localScale = new Vector3(controlAmount / maxControl , controlBar.transform.localScale.y, controlBar.transform.localScale.z);
    }

    // Increase the amount of Control
    public void IncreaseControl(float amount)
    {
        if (controlAmount + amount > maxControl)
        {
            controlAmount = maxControl;  // prevent Control from going below 0
            // Game Over because the player lost control
        }
        else
        {
            controlAmount += amount;    // decrease Control by amount
        }
        Debug.Log("Control Amount: " + controlAmount);
    }

    // Decrease the amount of Control
    void DecreaseControl(float amount)
    {
        if(controlAmount - amount < minControl)
        {
            controlAmount = minControl;  // prevent Control from going below 0
        }
        else
        {
            controlAmount -= amount;    // decrease Control by amount
        }

        Debug.Log("Control Amount: " + controlAmount);
    }

    // Second Option to Decrease Control (probably the better version)
    void DecreseControl2(float amount)
    {
        if(controlAmount % bufferPoint > 0)
        {
            controlAmount--;
        }

        Debug.Log("Control Amount: " + controlAmount);
    }
}
