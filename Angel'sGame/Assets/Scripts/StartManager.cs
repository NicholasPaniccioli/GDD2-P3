using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //When play button is pressed should load the game
    public void PlayButton()
    {
        SceneManager.LoadScene("PlayTest Level");
    }
    
    //Button to be used to load an insturctions menu
    public void InstructionsButton()
    {
        //SceneManager.LoadScene("Instructions Level");
    }
    
    //button to allow player the option to return to menu screen
    public void StartMenu()
    {
        SceneManager.LoadScene("StartScreen");
    }


    //For button press to quit the enitre application
    public void QuitButton()
    {
        Application.Quit();
    }
}
