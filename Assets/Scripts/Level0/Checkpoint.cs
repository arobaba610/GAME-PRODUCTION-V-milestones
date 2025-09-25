using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                player.SetCheckpoint(transform.position);
                // Optionally, add visual/audio feedback to indicate the checkpoint was activated.
            }
        }
    }
}
