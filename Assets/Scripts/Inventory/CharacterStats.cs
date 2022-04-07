using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    // use public int (or whatever datatype) nameOfVariable {get; private set;} in order for every other class to access it but only beeing able to set it within here
    public Stat armor;
    public Stat armorModifier;
    public Stat damageModifier;
    public Stat damage;
    public Stat knockbackModifier;
    public Stat knockback;
    public Stat health;
    public Stat healthModifier;
    public Stat fireballModifier;
    public Stat speedModifier;


}
