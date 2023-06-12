using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{
    //public variables
    public bool GameOver = false;
    public bool GamePaused = false;
    public GameObject GameOverUI;

    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        //if game over triggered
        if (GameOver == true)
        {
            GameOverScreen();
        }
    }

    //display game over screen
    public void GameOverScreen()
    {
        //display game over screen
        GameOverUI.SetActive(true);
    }

    //start new game
    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //close game window
    public void QuitGame()
    {
        Application.Quit();
    }

    //pause/unpause game
    public void PauseGame(bool state)
    {
        if (state == true)
        {
            //set global variable game is paused
            GamePaused = true;

            //freeze game time
            Time.timeScale = 0;
        }
        else
        {
            //reset global variable game is paused
            GamePaused = false;

            //resume game time
            Time.timeScale = 1;
        }
        
    }
}
