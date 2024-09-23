using UnityEngine;

public class MoveCamera : MonoBehaviour
{

    // Reference to the player's Transform component to track their position
    public Transform player;

    // Update is called once per frame
    void Update()
    {

        // Only move the camera if the game is not paused (Time.timeScale == 1)
        if (Time.timeScale == 1)
        {
            // Set the camera's position to the player's position, so it follows the player
            transform.position = player.transform.position;
        }
    }
}
