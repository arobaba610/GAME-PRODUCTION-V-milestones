using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;      // How fast the platform moves.
    public float distance = 3f;   // Maximum distance from the start position along the x-axis.

    private Vector3 startPosition;
    private Rigidbody rb;

    private void Start()
    {
        startPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        float movement = Mathf.PingPong(Time.time * speed, distance);
        Vector3 newPosition = new Vector3(startPosition.x + movement, startPosition.y, startPosition.z);
        rb.MovePosition(newPosition);
    }
}
