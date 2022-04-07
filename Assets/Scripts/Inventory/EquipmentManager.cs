using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance;

    private void Awake()
    {
        GameObject GameManager = GameObject.Find("GameManager");
        instance = GameManager.GetComponent<EquipmentManager>();

        //instance = this;
    }

    Equipment[] currentEquipment;

    public delegate void OnEquipmentChanged(Equipment newItem, Equipment oldItem);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    private void Start()
    {
        inventory = Inventory.instance;
        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    // Puts an equipment from the inventory onto the player
    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)newItem.equipmentSlot;
        Equipment oldItem = null;

        // If there already is a item in the slot we exchange it with the new one and put the old one back into the inventory
        if (currentEquipment[slotIndex] != null)
        {
            oldItem = currentEquipment[slotIndex]; 
            inventory.Add(oldItem);
        }

        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(newItem, oldItem);
        }

        currentEquipment[slotIndex] = newItem;
    }


    // Unequips a item on the player and puts it back into the inventory
    public void Unequip (int slotIndex)
    {
        if (currentEquipment[slotIndex] != null)
        {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, oldItem);
            }
        }
    }

    // Unequip all items
    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }
    }

    // Check if the button for unequiping everything is pressed
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
    }
}
