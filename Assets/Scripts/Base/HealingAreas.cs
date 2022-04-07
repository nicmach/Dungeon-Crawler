using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingAreas : CollideCharacters
{
    public int healing = 1;

    private float healCooldown = 1.0f;
    private float lastHeal;

    protected override void OnCollide(Collider2D coll)
    {
        if (Time.time - lastHeal > healCooldown && coll.name == "Player_0")
        {
            lastHeal = Time.time;
            GameManager.instance.Player.Heal(healing);
        }
    }
}
