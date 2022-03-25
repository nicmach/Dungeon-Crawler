using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageProjectile : CollideCharacters
{
    public int damage;
    public float knockback;

    protected override void OnCollide(Collider2D coll)
    {

        if (coll.tag == "Fighter")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                knockback = knockback
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log(coll.name);

            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
