using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Moving : Fighter // abstract means, that the class can only be used by inheriting from this class
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.85f;
    public float xSpeed = 1.1f;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        moveDelta = input;

        /* Checking in which direction the character is going.
         * Then changing the direction of the character sprite to the corresponding direction (i.e. right or left)*/

        if (moveDelta.x > 0)
            transform.localScale = Vector3.one;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);

        // Knockback-Function 

        moveDelta += pushDirection;

        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed); // This is used to prevent the enemy beeing knockbacked for eternity

        /* Checking if objects are in the way.
         * Move if there is no collision. */

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime * ySpeed), LayerMask.GetMask("Creatures", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(0, moveDelta.y * Time.deltaTime * ySpeed, 0); // X = 0 and Z = 0, because we only check collision for the y direction.
        }

        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime * xSpeed), LayerMask.GetMask("Creatures", "Blocking"));
        if (hit.collider == null)
        {
            transform.Translate(moveDelta.x * Time.deltaTime * xSpeed, 0, 0); // Y = 0 and Z = 0, because we only check collision for the x direction.
        }
    }
}
