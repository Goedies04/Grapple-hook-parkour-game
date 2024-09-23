using UnityEngine;

public class RotateGun : MonoBehaviour
{

    // Reference to the GrapplingGun script
    public GrapplingGun grappling;

    // The desired rotation the gun should have
    private Quaternion desiredRotation;

    // Speed at which the gun rotates towards the desired direction
    private float rotationSpeed = 5f;

    void Update()
    {
        // If the grappling hook is not in use
        if (!grappling.IsGrappling())
        {
            // Set the desired rotation to match the rotation of the gun's parent object (probably the player or camera)
            desiredRotation = transform.parent.rotation;
        }
        else
        {
            // If the grappling hook is in use, set the desired rotation towards the grapple point
            desiredRotation = Quaternion.LookRotation(grappling.GetGrapplePoint() - transform.position);
        }

        // Smoothly interpolate the gun's rotation towards the desired rotation over time
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
    }
}
