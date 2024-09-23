using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingRope : MonoBehaviour
{

    // Simulates the spring effect for the rope
    private Spring spring;

    // LineRenderer component used to draw the rope
    private LineRenderer lr;

    // Position of the current grapple point
    private Vector3 currentGrapplePosition;

    // Reference to the GrapplingGun script to check grappling status and get necessary positions
    public GrapplingGun grapplingGun;

    // Rope parameters for controlling the look and feel of the grappling rope
    public int quality;               // Number of points on the rope (the higher, the smoother)
    public float damper;              // How much the spring will dampen (reduce) oscillation
    public float strength;            // Strength of the spring for pulling effect
    public float velocity;            // Initial velocity for the spring movement
    public float waveCount;           // Number of waves along the rope
    public float waveHeight;          // Height of the wave pattern in the rope
    public AnimationCurve affectCurve;// Animation curve to influence how the rope behaves along its length

    // Called when the script instance is being loaded
    void Awake()
    {
        lr = GetComponent<LineRenderer>();  // Get the LineRenderer component to draw the rope
        spring = new Spring();              // Initialize the spring
        spring.SetTarget(0);                // Set the spring's target value to 0 (initial state)
    }

    // Called after all Update methods have been called
    void LateUpdate()
    {
        DrawRope(); // Call DrawRope to render the rope if needed
    }

    // Draws the rope using the LineRenderer
    void DrawRope()
    {
        // If the grappling hook is not active, don't draw the rope
        if (!grapplingGun.IsGrappling())
        {
            // Reset the grapple position to the gun's tip and reset spring force
            currentGrapplePosition = grapplingGun.gunTip.position;
            spring.Reset();

            // Clear any previously drawn rope by setting position count to 0
            if (lr.positionCount > 0)
                lr.positionCount = 0;
            return;
        }

        // If first time drawing the rope, initialize the spring and set position count
        if (lr.positionCount == 0)
        {
            spring.SetVelocity(velocity);       // Set the spring's initial velocity
            lr.positionCount = quality + 1;     // Set the number of points to draw the rope smoothly
        }

        // Set the spring's damper and strength values
        spring.SetDamper(damper);
        spring.SetStrength(strength);

        // Update the spring's value based on time
        spring.Update(Time.deltaTime);

        // Get the positions for the grapple point and the gun's tip
        var grapplePoint = grapplingGun.GetGrapplePoint();
        var gunTipPosition = grapplingGun.gunTip.position;

        // Calculate an upward vector relative to the grapple direction for wave offset
        var up = Quaternion.LookRotation((grapplePoint - gunTipPosition).normalized) * Vector3.up;

        // Smoothly interpolate the current grapple position towards the target grapple point
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, grapplePoint, Time.deltaTime * 12f);

        // Loop to set the position of each point along the rope
        for (var i = 0; i < quality + 1; i++)
        {
            var delta = i / (float)quality;  // Normalized value between 0 and 1 for each point
            var offset = up * waveHeight * Mathf.Sin(delta * waveCount * Mathf.PI) * spring.Value *
                         affectCurve.Evaluate(delta);  // Calculate wave effect along the rope

            // Set the position of each point on the rope, adding the wave offset for a wavy effect
            lr.SetPosition(i, Vector3.Lerp(gunTipPosition, currentGrapplePosition, delta) + offset);
        }
    }
}
