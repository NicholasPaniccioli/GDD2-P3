﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Started by Kyle Mulvey
/// Controls the amount of health that the player and enemies have.
/// Controls taking damage and healing, as well.
/// </summary>
public class Health : MonoBehaviour
{
    public float maxHealth; // the maximum amount of health an entity can have
    public float health;  // the amount of health an entity has
    public GameObject healthBar;
    public float iFrameTimeStamp;
    // Start is called before the first frame update
    void Start()
    {
        //maxHealth = 100f;   // health starts out at 100
        health = maxHealth; // health starts at the max health
        healthBar = GameObject.Find("Bar");
        iFrameTimeStamp = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthBar != null)//error check to prevent errors in the test scene that doesn't have a health bar
        {
            healthBar.transform.localScale = new Vector3(health / maxHealth, healthBar.transform.localScale.y, healthBar.transform.localScale.z);
        }
        if(iFrameTimeStamp <= Time.time)
        {
            gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.white;
        }

    }

    // Subtract damage from health
    // If health goes below zero, die
    public void TakeDamage(float damage)
    {
        health -= damage;
        gameObject.transform.GetChild(0).gameObject.GetComponent<Renderer>().material.color = Color.red;
        // if health goes below zero, die
        if (health <= 0)
        {
            Die();
        }
        else
        {
            
        }
        Debug.Log("Health: " + health);
    }

    // Play death animation and remove entity from screen
    void Die()
    {
        // play death animation
        // set active to false
        Debug.Log("You died!");
        gameObject.GetComponent<Collider>().enabled = false;
        gameObject.SetActive(false);
    }

    // Heal the entity
    public void Heal(float hp)
    {
        health += hp;   // add hp to health
        
        // if health is greater than maxHealth
        if(health > maxHealth)
        {
            health = maxHealth; // set health equal to maxHealth, so it doesn't go past maxHealth
        }

        Debug.Log("Health: " + health);
    }

}
