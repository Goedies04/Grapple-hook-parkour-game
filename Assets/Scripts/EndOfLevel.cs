using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    public int sceneNumber; // The scene number to load when the level ends
    public GameObject Sides; // Reference to the Sides GameObject for scaling effect
    public GameObject Sparks; // Reference to the Sparks GameObject for scaling effect
    public GameObject DarkSparks; // Reference to the DarkSparks GameObject for scaling effect

    private void OnTriggerEnter(Collider collision)
    {
        // Check if the object entering the trigger is the player
        if (collision.CompareTag("Player"))
        {
            // Start the coroutine to load the scene with a delay
            StartCoroutine(SceneLoadWithDelay(sceneNumber, 4));
        }
    }

    // Coroutine to load the scene after a delay with scaling effects
    IEnumerator SceneLoadWithDelay(int sceneNum, int durationInSeconds)
    {
        // Set the initial and target scales for the GameObjects
        Vector3 initialScaleSides = Sides.gameObject.transform.localScale;
        Vector3 targetScaleSides = initialScaleSides + new Vector3(0, 1, 0);

        Vector3 initialScaleSparks = Sparks.gameObject.transform.localScale;
        Vector3 targetScaleSparks = initialScaleSparks + new Vector3(0, 1, 0);

        Vector3 initialScaleDarkSparks = DarkSparks.gameObject.transform.localScale;
        Vector3 targetScaleDarkSparks = initialScaleDarkSparks + new Vector3(0, 1, 0);

        float elapsedTime = 0f; // Timer for scaling

        // Scale objects over the duration
        while (elapsedTime < durationInSeconds)
        {
            // Interpolate the scale between the initial and target scales
            Sides.gameObject.transform.localScale = Vector3.Lerp(initialScaleSides, targetScaleSides, elapsedTime / durationInSeconds);
            Sparks.gameObject.transform.localScale = Vector3.Lerp(initialScaleSparks, targetScaleSparks, elapsedTime / durationInSeconds);
            DarkSparks.gameObject.transform.localScale = Vector3.Lerp(initialScaleDarkSparks, targetScaleDarkSparks, elapsedTime / durationInSeconds);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure final scale is exactly the target scale after the loop ends
        Sides.gameObject.transform.localScale = targetScaleSides;
        Sparks.gameObject.transform.localScale = targetScaleSparks;
        DarkSparks.gameObject.transform.localScale = targetScaleDarkSparks;

        // If the scene number is 0, unlock the mouse cursor
        if (sceneNum == 0)
        {
            Cursor.lockState = CursorLockMode.None; // Unlocks the cursor
            Cursor.visible = true; // Makes the cursor visible
        }

        // Load the specified scene
        SceneManager.LoadScene(sceneNum);
    }
}
