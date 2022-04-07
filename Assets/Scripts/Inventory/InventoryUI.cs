using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform ItemsParent;
    public GameObject inventoryUI;

    Inventory inventory;
    InventorySlot[] slots;

    private void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI; //We trigger the event onItemChangedCallback when a new item has been added. Here we declare, that the UpdateUI method should be called in the case of the event.

        slots = ItemsParent.GetComponentsInChildren<InventorySlot>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
    private void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
