using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // Tracks if the game is paused or not
    public bool GameIsPaused = false;

    // The pause menu UI to show when the game is paused
    public GameObject pauseMenuUI;

    // The name of the main menu scene to load
    public string MenuLevel;

    // Update is called once per frame
    void Update()
    {
        // Check if the player presses the Escape key to toggle pause menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                // Resume the game if it is currently paused
                resume();
            }
            else
            {
                // Pause the game if it's currently running
                pause();
            }
        }
    }

    // Resumes the game by hiding the pause menu and setting the game time to normal speed
    public void resume()
    {
        pauseMenuUI.SetActive(false);      // Hide the pause menu
        Time.timeScale = 1f;               // Resume normal game time
        GameIsPaused = false;              // Set pause state to false

        // Re-lock the cursor for player movement in-game
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;            // Hide the cursor
    }

    // Pauses the game by showing the pause menu and stopping the game time
    void pause()
    {
        pauseMenuUI.SetActive(true);       // Show the pause menu
        Time.timeScale = 0f;               // Freeze game time
        GameIsPaused = true;               // Set pause state to true

        // Unlock the cursor so the player can interact with the menu
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;             // Show the cursor
    }

    // Loads the main menu scene
    public void LoadMenu()
    {
        Time.timeScale = 1f;               // Resume normal game time when leaving the current scene
        SceneManager.LoadScene(MenuLevel); // Load the main menu scene
    }

    // Quits the game application
    public void QuitGame()
    {
        Application.Quit();                // Exit the game
    }
}
