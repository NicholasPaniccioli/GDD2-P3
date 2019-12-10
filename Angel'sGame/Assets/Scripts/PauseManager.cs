using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public bool pause;
    public GameObject pMenu;

    // Start is called before the first frame update
    void Start()
    {
        pause = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            pause = !pause;
        }

        if (pause)
        {
            Time.timeScale = 0.0f;
            pMenu.SetActive(true); //when Paused shows the pause menu
        }
        else
        {
            Time.timeScale = 1.0f;
            pMenu.SetActive(false); //when unpaused the pause menu disappears.
        }
    }

    //Method used for Resume button to play the game
    public void ResumeButton()
    {
        pause = !pause;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
