using System.Collections;
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
        //and if so the player goes to the next appropriate level
        if (stair.IsTouching(player))
        {
            Debug.Log("Hit the trigger");

            //checks the current level the player is on
            //Based on that loads the next scene
            if(CheckScene() == "PlayTest Level")
            {
                SceneManager.LoadScene("Level Two");
            }
            else if(CheckScene() == "Level Two")
            {
                SceneManager.LoadScene("Level Three");
            }
            else if(CheckScene() == "Level Three")
            {
                SceneManager.LoadScene("Level Four");
            }
            else if(CheckScene() == "Level Four")
            {
                SceneManager.LoadScene("Level Five");
            }
            else if(CheckScene() == "Level Five")
            {
                SceneManager.LoadScene("Level Six");
            }
        }
    }

    //Method checks the current scene the player is in
    //Returns the name of the scene
    public string CheckScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        return sceneName;
    }
}
