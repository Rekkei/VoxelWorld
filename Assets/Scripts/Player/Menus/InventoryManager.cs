using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private KeyCode openInventoryKey = KeyCode.E;
    [SerializeField] private KeyCode closeInventoryKey = KeyCode.E;
    [SerializeField] private GameObject inventoryPanel;

    private bool isInventoryOpen = false;

    private void Start()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(openInventoryKey) && !isInventoryOpen)
        {
            OpenInventory();
        }
        else if (Input.GetKeyDown(closeInventoryKey) && isInventoryOpen)
        {
            CloseInventory();
        }
    }

    private void OpenInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isInventoryOpen = true;

            PlayerActions.EnableActions(false);
            PlayerMovement.EnableMovement(false);
            PlayerLook.EnableLook(false);
        }
    }

    private void CloseInventory()
    {
        if (inventoryPanel != null)
        {
            inventoryPanel.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            isInventoryOpen = false;

            PlayerActions.EnableActions(true);
            PlayerMovement.EnableMovement(true);
            PlayerLook.EnableLook(true);
        }
    }
}
