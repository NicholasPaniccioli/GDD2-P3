using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera cam; //Cam that follows the player around
    public GameObject obj; //Object that the camera follows 
    private Vector3 temp; //Temp vector to help with transform


    // Start is called before the first frame update
    void Start()
    {
        temp = cam.transform.position;

        //Sets starting position of camera on player
        temp.x = obj.transform.position.x;
        temp.y = obj.transform.position.y;
        temp.z = -10;
        cam.transform.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        //Updates the position consistently
        temp.x = obj.transform.position.x;
        temp.y = obj.transform.position.y;
        cam.transform.position = temp;
    }
}
