using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : CollideCharacters
{

    /**private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name != "Player_0")
        {
            Destroy(gameObject);
        }
    }**/

    public float damage;
    public float knockback;

    protected override void OnCollide(Collider2D coll)
    {

        GameObject thePlayer = GameObject.Find("Player_0");
        PlayerStats playerStats = thePlayer.GetComponent<PlayerStats>();
        if (coll.tag == "Fighter" && coll.name != "Player_0")
        {
            Damage dmg = new Damage
            {
                damageAmount = damage * playerStats.fireballModifier.GetValue(),
                origin = transform.position,
                knockback = knockback
            };

            coll.SendMessage("ReceiveDamage", dmg);

            Debug.Log(coll.name);

            Destroy(gameObject);
        }
        else if (coll.name != "Player_0")
        {
            Destroy(gameObject);
        }
    }
}
