using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    private bool debug;
    private GameObject staff;
    private Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        staff = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angleOfRotation = Mathf.Rad2Deg * Mathf.Atan2(mouseWorldPos.y - staff.transform.position.y, mouseWorldPos.x - staff.transform.position.x);
        staff.transform.rotation = Quaternion.Euler(0, 0, angleOfRotation);

        if (Input.GetKeyDown(KeyCode.P))
            debug = !debug;
        if (debug)
            drawDebug();
    }

    void drawDebug()
    {
        Debug.DrawLine(transform.position, transform.position + transform.up, Color.green);
        Debug.DrawLine(transform.position, transform.position + transform.right, Color.red);

    }
}
