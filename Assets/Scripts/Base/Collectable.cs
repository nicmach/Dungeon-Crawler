using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : CollideCharacters
{
    protected bool collected; // Protected means, that your childreen can access this data, while no one else can (protected is between private and public).

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player_0")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
