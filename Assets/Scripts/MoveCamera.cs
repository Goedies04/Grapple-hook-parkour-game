using UnityEngine;

public class MoveCamera : MonoBehaviour {

    public Transform player;

    void Update() {

        if (Time.timeScale == 1)
        {
            transform.position = player.transform.position;
        }
    }
}
