using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public static Inventory Singleton;
    public static InventoryItem carriedItem;

    [SerializeField] InventorySlot[] inventorySlots;
    [SerializeField] InventorySlot[] hotbarSlots;
    public bool isHotBar = false;

    // 0=Head, 1=Chest, 2=Legs, 3=Feet
    [SerializeField] InventorySlot[] equipmentSlots;

    [SerializeField] Transform draggablesTransform;
    [SerializeField] InventoryItem itemPrefab;

    [Header("Item List")]
    [SerializeField] Item[] items;

    [Header("Debug")]
    [SerializeField] Button giveItemBtn;

    [Header("Hotbar Selection")]
    [SerializeField] private PlayerActions playerActions;
    private int selectedIndex = 0;

    void Awake()
    {
        Singleton = this;
        giveItemBtn.onClick.AddListener(delegate { SpawnInventoryItem(); });
    }

    void Start()
    {
        if (isHotBar)
        {
            SelectHotbarItem(0);
        }
    }

    void Update()
    {
        if (carriedItem != null)
        {
            carriedItem.transform.position = Input.mousePosition;
        }

        HandleHotbarSelection();
    }

    private void HandleHotbarSelection()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha1 + i)))
            {
                if (isHotBar)
                {
                    SelectHotbarItem(i);
                }
            }
        }
    }

    private void SelectHotbarItem(int index)
    {
        if (selectedIndex >= 0)
        {
            hotbarSlots[selectedIndex].GetComponent<Image>().color = Color.white;
        }

        selectedIndex = index;

        if (hotbarSlots[selectedIndex].myItem != null)
        {
            playerActions.selectedBlock = hotbarSlots[selectedIndex].myItem.myItem.prefab;
        }
        else
        {
            playerActions.selectedBlock = null;
        }

        hotbarSlots[selectedIndex].GetComponent<Image>().color = Color.yellow;
        UpdateHotbarSlotTexts();
    }

    private void UpdateHotbarSlotTexts()
    {
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            TextMeshProUGUI textMeshPro = hotbarSlots[i].GetComponentInChildren<TextMeshProUGUI>();

            if (textMeshPro != null && hotbarSlots[i].myItem != null)
            {
                textMeshPro.text = hotbarSlots[i].myItem.myItem.name;
            }
        }
    }

    public void SetCarriedItem(InventoryItem item)
    {
        if (carriedItem != null)
        {
            if (item.activeSlot.myTag != SlotTag.None && item.activeSlot.myTag != carriedItem.myItem.itemTag) return;
            item.activeSlot.SetItem(carriedItem);
        }

        if (item.activeSlot.myTag != SlotTag.None)
        {
            EquipEquipment(item.activeSlot.myTag, null);
        }

        carriedItem = item;
        carriedItem.canvasGroup.blocksRaycasts = false;
        item.transform.SetParent(draggablesTransform);
    }

    public void EquipEquipment(SlotTag tag, InventoryItem item = null)
    {
        switch (tag)
        {
            case SlotTag.Head:
                if (item == null)
                {
                    // Destroy item.equipmentPrefab on the Player Object;
                    Debug.Log("Unequipped helmet on " + tag);
                }
                else
                {
                    // Instantiate item.equipmentPrefab on the Player Object;
                    Debug.Log("Equipped " + item.myItem.name + " on " + tag);
                }
                break;
            case SlotTag.Chest:
                break;
            case SlotTag.Legs:
                break;
            case SlotTag.Feet:
                break;
        }
    }

    public void SpawnInventoryItem(Item item = null)
    {
        Item _item = item;
        if (_item == null)
        {
            _item = PickRandomItem();
        }

        for (int i = 0; i < inventorySlots.Length; i++)
        {
            // Check if the slot is empty
            if (inventorySlots[i].myItem == null)
            {
                Instantiate(itemPrefab, inventorySlots[i].transform).Initialize(_item, inventorySlots[i]);
                break;
            }
        }
    }

    Item PickRandomItem()
    {
        int random = Random.Range(0, items.Length);
        return items[random];
    }
}
