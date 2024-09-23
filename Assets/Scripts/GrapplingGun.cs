using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
    // The point in space the grappling hook will attach to
    private Vector3 grapplePoint;

    // LayerMask to define what can be grappled
    public LayerMask whatIsGrappleable;

    // Transforms for gun tip, camera, and player to control the grappling behavior
    public Transform gunTip, camera, player;

    // Maximum distance for grappling
    private float maxDistance = 100f;

    // SpringJoint to create a spring-like connection to the grapple point
    private SpringJoint joint;

    [Header("OdmGear")]
    public Transform orientation; // Orientation reference for the grappling gear
    public Rigidbody rb;         // Rigidbody for applying physics
    public float extendCableSpeed; // Speed for extending the cable

    void Update()
    {
        // Check for mouse input to start or stop grappling
        if (Input.GetMouseButtonDown(0))
        {
            StartGrapple(); // Start the grappling action
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopGrapple(); // Stop the grappling action
        }

        // If a joint exists, handle OdmGear movement
        if (joint != null) OdmGearMovement();
    }

    /// <summary>
    /// Starts the grapple when called.
    /// </summary>
    void StartGrapple()
    {
        RaycastHit hit; // Variable to store raycast hit information

        // Perform a raycast from the camera to find a valid grapple point
        if (Physics.Raycast(camera.position, camera.forward, out hit, maxDistance, whatIsGrappleable))
        {
            grapplePoint = hit.point; // Set the grapple point to the hit point
            joint = player.gameObject.AddComponent<SpringJoint>(); // Add a SpringJoint component to the player
            joint.autoConfigureConnectedAnchor = false; // Prevent automatic anchor configuration
            joint.connectedAnchor = grapplePoint; // Set the anchor point to the grapple point

            // Calculate distance from the player to the grapple point
            float distanceFromPoint = Vector3.Distance(player.position, grapplePoint);

            // Configure the joint's distance parameters
            joint.maxDistance = distanceFromPoint * 0.8f; // Maximum distance the joint can stretch
            joint.minDistance = distanceFromPoint * 0.25f; // Minimum distance the joint can contract

            // Joint physics properties
            joint.spring = 4.5f; // Spring strength
            joint.damper = 7f;   // Damping to reduce oscillations
            joint.massScale = 4.5f; // Mass scale for the spring effect
        }
    }

    /// <summary>
    /// Stops the grapple when called.
    /// </summary>
    void StopGrapple()
    {
        Destroy(joint); // Remove the SpringJoint component to stop grappling
    }

    /// <summary>
    /// Checks if the player is currently grappling.
    /// </summary>
    public bool IsGrappling()
    {
        return joint != null; // Return true if the joint exists
    }

    /// <summary>
    /// Returns the current grapple point.
    /// </summary>
    public Vector3 GetGrapplePoint()
    {
        return grapplePoint; // Return the grapple point's position
    }

    /// <summary>
    /// Handles movement while grappling.
    /// </summary>
    private void OdmGearMovement()
    {
        // Shorten the cable if the space key is held down
        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 directionToPoint = grapplePoint - transform.position; // Calculate direction to grapple point
            rb.AddForce(directionToPoint.normalized * Time.deltaTime); // Apply force towards the grapple point

            // Update joint distance parameters
            float distanceFromPoint = Vector3.Distance(transform.position, grapplePoint);
            joint.maxDistance = distanceFromPoint * 0.8f; // Update maximum distance
            joint.minDistance = distanceFromPoint * 0.25f; // Update minimum distance
        }

        // Extend the cable if the left shift key is held down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            float extendedDistanceFromPoint = Vector3.Distance(transform.position, grapplePoint) + extendCableSpeed;

            // Update joint distance parameters for extended cable
            joint.maxDistance = extendedDistanceFromPoint * 0.8f;
            joint.minDistance = extendedDistanceFromPoint * 0.25f;
        }
    }
}
