using UnityEngine;
using TMPro;

public class DoorInteraction : MonoBehaviour
{
    public string requiredKeyID = "Key1";
    public GameObject doorObject;
    public TextMeshProUGUI uiMessage;

    private bool isPlayerNear = false;

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E near the door.");
            TryOpenDoor();
        }
    }

    private void TryOpenDoor()
    {
        Debug.Log("Trying to open door...");
        PlayerInventory inventory = FindObjectOfType<PlayerInventory>();

        if (inventory != null && inventory.HasKey(requiredKeyID))
        {
            Debug.Log("Key match! Opening door.");
            Rigidbody rb = doorObject.GetComponent<Rigidbody>();
            rb.isKinematic = false;
        }
        else
        {
            Debug.LogWarning("Missing key: " + requiredKeyID);
            ShowMessage("You need a key to open this door!");
        }
    }

    private void ShowMessage(string message)
    {
        if (uiMessage != null)
        {
            uiMessage.gameObject.SetActive(true);
            uiMessage.text = message;
            uiMessage.color = new Color(1f, 1f, 1f, 1f); // ensure visibility
            CancelInvoke("ClearMessage");
            Invoke("ClearMessage", 2f);
        }
    }

    private void ClearMessage()
    {
        if (uiMessage != null)
        {
            uiMessage.text = "";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            Debug.Log("Player entered door trigger.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            Debug.Log("Player exited door trigger.");
        }
    }
}
