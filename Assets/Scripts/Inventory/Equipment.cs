using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName="New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipmentSlot;

    public float armor;
    public float armorModifier;
    public float damageModifier;
    public float damage;
    public float knockbackModifier;
    public float knockback;
    public float health;
    public float healthModifier;
    public float fireballModifier;
    public float speedModifier;

    public override void Use()
    {
        base.Use();
        EquipmentManager.instance.Equip(this);
        RemoveFromInventory();
    }
}

public enum EquipmentSlot {Head, Chest, Legs, Weapon, Shield, Feet, Miscellaneous_1, Miscellaneous_2}
