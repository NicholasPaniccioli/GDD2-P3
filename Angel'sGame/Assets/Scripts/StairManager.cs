﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StairManager : MonoBehaviour
{
    //Gets the p;ayer and stair game object colliders
    public Collider2D stair;
    public Collider2D player;

    // Start is called before the first frame update
    void Start()
    {

        stair = GameObject.Find("Staircase").GetComponent<Collider2D>();
        player = GameObject.Find("FeetCollider").GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if the colliders overlap 
        //and if so the player goes to the next level
        if (stair.IsTouching(player) && SceneManager.)
        {
            Debug.Log("Hit the trigger");
            SceneManager.LoadScene("Level Two");
        }
    }
}
