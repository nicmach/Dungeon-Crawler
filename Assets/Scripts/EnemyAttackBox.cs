using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackBox : CollideCharacters
{
    public int damage;
    public float knockback;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter" && coll.name == "Player_0")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                knockback = knockback
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log(coll.name);
        }
    }
}
