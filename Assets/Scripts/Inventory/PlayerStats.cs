using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    // Start is called before the first frame update
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment newItem, Equipment oldItem)
    {
        if (newItem != null)
        {
            armor.AddModifier(newItem.armor);
            armorModifier.AddModifier(newItem.armorModifier);
            damageModifier.AddModifier(newItem.damageModifier);
            damage.AddModifier(newItem.damage);
            knockbackModifier.AddModifier(newItem.knockbackModifier);
            knockback.AddModifier(newItem.knockback);
            health.AddModifier(newItem.health);
            healthModifier.AddModifier(newItem.healthModifier);
            fireballModifier.AddModifier(newItem.fireballModifier);
            speedModifier.AddModifier(newItem.speedModifier);
        }

        if (oldItem != null)
        {
            armor.RemoveModifier(oldItem.armor);
            armorModifier.RemoveModifier(oldItem.armorModifier);
            damageModifier.RemoveModifier(oldItem.damageModifier);
            damage.RemoveModifier(oldItem.damage);
            knockbackModifier.RemoveModifier(oldItem.knockbackModifier);
            knockback.RemoveModifier(oldItem.knockback);
            health.RemoveModifier(oldItem.health);
            healthModifier.RemoveModifier(oldItem.healthModifier);
            fireballModifier.RemoveModifier(oldItem.fireballModifier);
            speedModifier.RemoveModifier(oldItem.speedModifier);
        }

    }
}
