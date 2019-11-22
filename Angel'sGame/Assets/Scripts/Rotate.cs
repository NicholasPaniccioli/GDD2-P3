using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private bool debug = false, flag;
    private GameObject staff;
    private float demonAngle, angleOfRotation, perlinY;

    // Start is called before the first frame update
    void Start()
    {
        staff = gameObject.transform.GetChild(1).gameObject;
        demonAngle = 0;
        flag = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (true/*player.control < 75*/) {
            flag = false;
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            angleOfRotation = Mathf.Rad2Deg * Mathf.Atan2(mouseWorldPos.y - staff.transform.position.y, mouseWorldPos.x - staff.transform.position.x);
            staff.transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

            if (Input.GetKeyDown(KeyCode.P))
                debug = !debug;
            if (debug)
                drawDebug();
        }
        else{
            if (!flag){
                demonAngle = 0;
                flag = true;
                perlinY = Random.Range(0.0f, 100.0f);
            }
            float newAngle = (Mathf.PerlinNoise(Time.time * 1.0f,perlinY) * 5) - 2.5f;
            demonAngle += newAngle;
            staff.transform.rotation = Quaternion.Euler(0, 0, demonAngle);
        }
    }

    void drawDebug()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);

    }
}
