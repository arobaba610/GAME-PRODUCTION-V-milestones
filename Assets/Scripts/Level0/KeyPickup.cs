using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    public string keyID = "Key1";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Key touched by player");
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();
            if (inventory != null)
            {
                inventory.CollectKey(keyID);
                Debug.Log($"Key '{keyID}' collected!");
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("No PlayerInventory found on player.");
            }
        }
    }
}
