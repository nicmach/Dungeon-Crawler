using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class Enemy_AI_Path : Fighter_AI_Path
{
    public int xpValue = 5;
    private bool collidingPlayer;
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];      // New implementation of collision because Enemy can not inheritate from CollideCharacters
    protected BoxCollider2D boxCollider;
    public AIPath aiPath;
    protected Vector3 moveDelta;

    private void FixedUpdate()
    {
        if(aiPath.desiredVelocity.x >= 0.01f) //Moving to the right
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f) //Moving to the left
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }

        // FOR THE KNOCKBACK YOU COULD TRY TO TRANSFORM aiPath.desiredVelcoity 

        // Check for collision with the player
        boxCollider = GetComponent<BoxCollider2D>();
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
        collidingPlayer = false;

        boxCollider.OverlapCollider(filter, hits); // Looks for objects, that collide with the object and puts them into hits.
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if (hits[i].name == "Player_0")
            {
                collidingPlayer = true;
            }

            hits[i] = null;

        }
    }


    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);
        GameManager.instance.ShowText(xpValue + "xp", 25, Color.green, transform.position, Vector3.up * 40, 1.5f);
    }
}
