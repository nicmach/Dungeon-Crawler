using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Stat 
{
    [SerializeField]
    private float baseValue;

    // Equipment Modifiers
    #region Equipment
    private List<float> modifiers = new List<float>();

    // Easy fixes: list for each modifier. Then add up the effects and change the variables.


    public void AddModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Add(modifier);
        }
    }

    public void RemoveModifier(float modifier)
    {
        if (modifier != 0)
        {
            modifiers.Remove(modifier);
        }
    }


    #endregion
    public float GetValue()
    {
        float finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x); //for loop for each elemnt in the modifiers list and add each element (here denoted by x) to our finalValue
        return finalValue;
    }
 }
