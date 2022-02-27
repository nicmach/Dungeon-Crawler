using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Moving
{
    public int xpValue = 5;

    public float triggerRange = 2;
    public float chaseRange = 5;
    private bool chasing;
    private bool collidingPlayer;
    private Transform playerTransform;
    private Vector3 startPosition;

    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];      // New implementation of collision because Enemy can not inheritate from CollideCharacters


    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.Find("Player_0").transform;
        startPosition = transform.position;
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>(); // Takes the BoxCollider2D Component from the first child of the Object it is attached to (in this case this would be the Collider of AttackArea)
    }

    private void FixedUpdate()
    {
       // Check if the player is in the triggerRange
       if (Vector3.Distance(playerTransform.position, startPosition) < chaseRange)
       {
            if (Vector3.Distance(playerTransform.position, startPosition) < triggerRange)
                chasing = true;

            if (chasing)
            {
                if (!collidingPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(startPosition - transform.position);
            }
       }
       else
        {
            UpdateMotor(startPosition - transform.position);
            chasing = false;
        }

        // Check for collision with the player

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
