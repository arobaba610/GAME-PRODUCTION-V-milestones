using UnityEngine;

/// <summary>
/// A spinning pole trap that moves back and forth along the Z-axis relative to its starting position,
/// while continuously rotating using torque.
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class SpinningPoleTrap : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 torque = new Vector3(0f, 100f, 0f);

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float minOffsetZ = -5f;
    [SerializeField] private float maxOffsetZ = 5f;

    private float originalX;
    private float originalY;
    private float originalZ;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = 50f;

        // Cache the initial position
        Vector3 startPos = transform.position;
        originalX = startPos.x;
        originalY = startPos.y;
        originalZ = startPos.z;
    }

    private void FixedUpdate()
    {
        ApplyRotation();
    }

    private void Update()
    {
        MoveBackAndForth();
    }

    /// <summary>
    /// Applies continuous torque to rotate the trap.
    /// </summary>
    private void ApplyRotation()
    {
        rb.AddTorque(torque);
    }

    /// <summary>
    /// Moves the trap back and forth along the Z-axis relative to its starting position.
    /// </summary>
    private void MoveBackAndForth()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, maxOffsetZ - minOffsetZ) + minOffsetZ;
        float newZ = originalZ + offset;
        transform.position = new Vector3(originalX, originalY, newZ);
    }
}
