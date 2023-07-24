using UnityEngine;

public class AngleLimit : MonoBehaviour
{
    public float lowerAngleLimit = -20f; // The lower angle limit in degrees
    public float upperAngleLimit = 20f;  // The upper angle limit in degrees

    private SpringJoint2D springJoint;
    private float initialRotation;

    private void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        initialRotation = springJoint.connectedBody.rotation - transform.rotation.eulerAngles.z;
    }

    private void Update()
    {
        // Calculate the current angle between the connected objects
        float currentAngle = springJoint.connectedBody.rotation - transform.rotation.eulerAngles.z - initialRotation;

        // Check if the current angle exceeds the limits
        if (currentAngle < lowerAngleLimit)
        {
            // Limit the lower angle
            float targetAngle = springJoint.connectedBody.rotation - lowerAngleLimit - initialRotation;
            springJoint.connectedBody.MoveRotation(targetAngle);
        }
        else if (currentAngle > upperAngleLimit)
        {
            // Limit the upper angle
            float targetAngle = springJoint.connectedBody.rotation - upperAngleLimit - initialRotation;
            springJoint.connectedBody.MoveRotation(targetAngle);
        }
    }
}
