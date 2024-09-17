using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevel : MonoBehaviour
{
    public int sceneNumber;
    public GameObject Sides;
    public GameObject Sparks;
    public GameObject DarkSparks;

    private void OnTriggerEnter(Collider collision)
    {

        if (collision.CompareTag("Player"))
        {
            
            StartCoroutine(SceneLoadWithDelay(sceneNumber, 4));

        }
    }

    IEnumerator SceneLoadWithDelay(int sceneNum, int durationInSeconds)
    {
        // Set the initial and target scales
        Vector3 initialScaleSides = Sides.gameObject.transform.localScale;
        Vector3 targetScaleSides = initialScaleSides + new Vector3(0, 1, 0);

        Vector3 initialScaleSparks = Sparks.gameObject.transform.localScale;
        Vector3 targetScaleSparks = initialScaleSparks + new Vector3(0, 1, 0);

        Vector3 initialScaleDarkSparks = DarkSparks.gameObject.transform.localScale;
        Vector3 targetScaleDarkSparks = initialScaleDarkSparks + new Vector3(0, 1, 0);

        float elapsedTime = 0f;

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

        // Load the scene
        SceneManager.LoadScene(sceneNum);
    }

}
