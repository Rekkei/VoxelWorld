using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using System;

public class ActionBar : MonoBehaviour
{
    public List<GameObject> itemSlots;
    public List<GameObject> blockPrefabs;

    [SerializeField] private PlayerActions playerActions;

    private int selectedIndex = 0;

    void Start()
    {
        playerActions.selectedBlock = blockPrefabs[selectedIndex];
        itemSlots[selectedIndex].GetComponent<Image>().color = Color.yellow;
        UpdateItemSlotTexts();
    }

    void Update()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (Input.GetKeyDown((KeyCode)((int)KeyCode.Alpha1 + i)))
            {
                SelectItem(i);
            }
        }
    }

    private void SelectItem(int index)
    {
        if (selectedIndex >= 0)
        {
            itemSlots[selectedIndex].GetComponent<Image>().color = Color.white;
        }

        selectedIndex = index;

        playerActions.selectedBlock = blockPrefabs[index];
        Debug.Log(playerActions.selectedBlock.name);

        itemSlots[selectedIndex].GetComponent<Image>().color = Color.yellow;

        UpdateItemSlotTexts();
    }

    private void UpdateItemSlotTexts()
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            TextMeshProUGUI textMeshPro = itemSlots[i].GetComponentInChildren<TextMeshProUGUI>();

            if (textMeshPro != null && i < blockPrefabs.Count)
            {
                textMeshPro.text = blockPrefabs[i].name;
            }
        }
    }
}
