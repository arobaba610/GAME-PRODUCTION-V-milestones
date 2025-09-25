using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float horizontalJumpMultiplier = 1f;
    private float currentMoveSpeed;

    private Vector3 checkpointPosition;
    public float deathYThreshold = -10f;
    public TextMeshProUGUI winMessage;

    private Rigidbody rb;
    private bool jumpRequested = false;
    private bool isGrounded = false;
    private Vector3 jumpDirection;

    public Transform[] teleportPositions = new Transform[10];
    public CameraFollow cameraFollow;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentMoveSpeed = moveSpeed;

       // rb.constraints = RigidbodyConstraints.FreezeRotation;


        if (cameraFollow != null)
        {
            cameraFollow.target = transform;
        }

        checkpointPosition = new Vector3(transform.position.x, transform.position.y, 0f);
    }

    private void Update()
    {
        if (transform.position.y < deathYThreshold)
        {
            Respawn();
        }

        // Diagonal jump input
        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Z)) // 
            {
                jumpDirection = new Vector3(-1f, 1f, 0f).normalized;
                jumpRequested = true;
            }
            else if (Input.GetKeyDown(KeyCode.C)) // 
            {
                jumpDirection = new Vector3(1f, 1f, 0f).normalized;
                jumpRequested = true;
            }
            else if (Input.GetKeyDown(KeyCode.Space)) // vertical or directional jump
            {
                float horizontalInput = Input.GetAxisRaw("Horizontal");
                jumpDirection = new Vector3(horizontalInput * horizontalJumpMultiplier, 1f, 0f).normalized;
                jumpRequested = true;
            }
        }

        // Teleportation keys 1–9 and 0
        for (int i = 0; i < teleportPositions.Length; i++)
        {
            KeyCode key = i < 9 ? KeyCode.Alpha1 + i : KeyCode.Alpha0;
            if (Input.GetKeyDown(key) && teleportPositions[i] != null)
            {
                Vector3 tp = teleportPositions[i].position;
                transform.position = new Vector3(tp.x, tp.y, 0f);
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private void FixedUpdate()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        Vector3 newVelocity = rb.velocity;
        newVelocity.x = horizontalInput * currentMoveSpeed;
        rb.velocity = new Vector3(newVelocity.x, rb.velocity.y, 0f); // Lock Z

        if (jumpRequested)
        {
            rb.AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
            jumpRequested = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            RestartGame(); // veya Respawn();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("DeadZone"))
        {
            Respawn();
        }

        SpeedZone sz = other.GetComponent<SpeedZone>();
        if (sz != null)
        {
            currentMoveSpeed = moveSpeed * sz.speedMultiplier;
        }

        if (other.gameObject.CompareTag("Finish"))
        {
            WinGame();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        SpeedZone sz = other.GetComponent<SpeedZone>();
        if (sz != null)
        {
            currentMoveSpeed = moveSpeed;
        }
    }

    public void SetCheckpoint(Vector3 newCheckpoint)
    {
        checkpointPosition = new Vector3(newCheckpoint.x, newCheckpoint.y, 0f);
    }

    private void Respawn()
    {
        transform.position = new Vector3(checkpointPosition.x, checkpointPosition.y, 0f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    private void WinGame()
    {
        Debug.Log("You reached the goal! You win!");
        if (winMessage != null)
        {
            winMessage.gameObject.SetActive(true);
            winMessage.text = "You Win!";
        }
        Invoke("RestartGame", 3f);
    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
