using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReloadScene : MonoBehaviour
{
    // This method is called when another object enters the trigger collider attached to the object this script is attached to
    private void OnTriggerEnter(Collider col)
    {
        // Check if the object that entered the trigger has the "Player" tag
        if (col.CompareTag("Player"))
        {
            // Reload the current active scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
