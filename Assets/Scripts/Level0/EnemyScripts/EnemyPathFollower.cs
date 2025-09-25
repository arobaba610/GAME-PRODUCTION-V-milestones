using UnityEngine;

public class EnemyPathFollower : MonoBehaviour
{
    public float moveSpeed = 2f;
    public int maxHealth = 100;

    private int currentHealth;
    private int currentWaypointIndex = 0;
    private WaypointPath path;
    private Rigidbody rb;

    void Start()
    {
        path = GameObject.FindObjectOfType<WaypointPath>();
        rb = GetComponent<Rigidbody>();
        currentHealth = maxHealth;

        if (path == null)
        {
            Debug.LogError("WaypointPath bulunamadı!");
        }
        else
        {
            Debug.Log("Enemy başladı. Waypoint sayısı: " + path.WaypointCount);
        }
    }

    void Update()
    {
        MoveAlongPath();
    }

    void MoveAlongPath()
    {
        if (path == null || currentWaypointIndex >= path.WaypointCount)
        {
            ReachGate();
            return;
        }

        Transform target = path.GetWaypoint(currentWaypointIndex);

        // Sadece X-Z düzleminde hareket (Y'yi sabit tut)
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        Vector3 direction = (targetPosition - transform.position).normalized;

        rb.velocity = new Vector3(direction.x * moveSpeed, rb.velocity.y, direction.z * moveSpeed);

        Debug.Log("Waypoint " + currentWaypointIndex + " yönüne gidiyor");

        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            currentWaypointIndex++;
            Debug.Log("Waypoint " + currentWaypointIndex + " noktasına geçti");
        }
    }

    void ReachGate()
    {
        Debug.Log("Kapıya ulaştı!");
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
