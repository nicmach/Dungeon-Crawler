using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*You could add [RequireComponent(typeof(BoxCollider2D))], so that 
 * if the object, to which this script is attached, does not have a BoxCollider2D
 * it will automaticaley get on assigned. */

public class CollideCharacters : MonoBehaviour
{
    public ContactFilter2D filter;

    private BoxCollider2D boxCollider; 

    private Collider2D[] hits = new Collider2D[10]; // Array that contains the Objects, that were hit within the frame (Collider2D[10] creates a cap of ten collissions at a time).

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    {
        boxCollider.OverlapCollider(filter, hits); // Looks for objects, that collide with the object and puts them into hits.
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]);

            hits[i] = null;
                
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
    }
}
