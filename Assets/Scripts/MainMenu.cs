using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Method to start the game
    public void PlayGame()
    {
        // Loads the next scene in the build order
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    // Method to quit the game
    public void QuitGame()
    {
        // Log a message to the console to indicate the quit action (useful for debugging in the editor)
        Debug.Log("QUIT!");

        // Exit the application when in a built version of the game
        Application.Quit();
    }
}
